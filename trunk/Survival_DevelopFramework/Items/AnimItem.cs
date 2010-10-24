using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Survival_DevelopFramework.GraphicSystem;
using Survival_DevelopFramework.InputSystem;
using Survival_DevelopFramework.Helpers;
using Survival_DevelopFramework.PhysicsSystem;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Controllers;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using Survival_DevelopFramework.GameManager;
using Survival_DevelopFramework.SceneManager;

namespace Survival_DevelopFramework.Items
{
    /// <summary>
    /// 动画Item
    /// </summary>
    class AnimItem : ItemBase
    {
        #region Constructor
        /// <summary>
        /// AnimItem
        /// 预设动画帧参数
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="frameWidth"></param>
        /// <param name="frameHeight"></param>
        /// <param name="frameNumber"></param>
        public AnimItem(Texture2D texture,int frameWidth,int frameHeight,int frameNumber):base(texture)
        {
           this.texture = texture;
           this.frameHeight = frameHeight;
           this.frameWidth = frameWidth;
           this.frameNumber = frameNumber;
        }
        /// <summary>
        /// AnimItem
        /// 给不启用动画的子类使用
        /// </summary>
        /// <param name="tex"></param>
        public AnimItem(Texture2D tex):base(tex)
        {
            this.texture = texture;
        }
        #endregion

        #region Variables
        private bool isIdle = true;
        protected int frameWidth;
        protected int frameHeight;
        protected int frameNumber;
        private int currentFrameId;

        /// <summary>
        /// 播放总时间
        /// </summary>
        private int baseTimeMS = 0;

        /// <summary>
        /// 角色动作序列
        /// </summary>
        public class AnimSequence
        {
            #region Constructor
            public AnimSequence(String setName, int setStartFrameId, int setActionLength, int setFrameRate, bool enableDistrub, bool looping)
            {
                name = setName;
                startFrameId = setStartFrameId;
                actionLength = setActionLength;
                frameRate = setFrameRate;
                this.enableDistrub = enableDistrub;
                this.looping = looping;
            }
            #endregion

            #region Variable
            /// <summary>
            /// Action 名称
            /// </summary>
            public String name;
            /// <summary>
            /// 开始帧号
            /// </summary>
            public int startFrameId;
            /// <summary>
            /// 帧长度
            /// </summary>
            public int actionLength;
            /// <summary>
            /// 帧率
            /// </summary>
            public int frameRate;
            /// <summary>
            /// 允许打断
            /// </summary>
            public bool enableDistrub;
            /// <summary>
            /// 循环
            /// </summary>
            public bool looping;
            #endregion

            #region Properties
            public int EndFrameId
            {
                get
                {
                    return startFrameId + actionLength - 1;
                }
            }
            /// <summary>
            /// 每一帧占用的毫秒数
            /// </summary>
            public int MsPerFrame
            {
                get
                {
                    return 1000 / frameRate;
                }
            }
            public int MsAllFrames
            {
                get
                {
                    return MsPerFrame * actionLength;
                }
            }
            #endregion
        }
        public List<AnimSequence> animSeqList = new List<AnimSequence>();
        /// <summary>
        ///  当前动作序列
        /// </summary>
        protected AnimSequence currentSeq;

        #endregion

        #region Properties
        public int FrameWidth
        {
            get
            {
                return frameWidth;
            }
        }
        public int FrameHeight
        {
            get
            {
                return frameHeight;
            }
        }
        public int ColumnCount
        {
            get
            {
                return texture.Width / frameWidth;
            }
        }
        public int RowCount
        {
            get
            {
                return texture.Height / frameHeight;
            }
        }
        #endregion

        #region Draw
        /// <summary>
        /// Draw
        /// </summary>
        public override void Draw()
        {
            // 如果启用动画则绘制帧
            // 否则使用基类的绘制方式
            if (animSeqList.Count != 0)
            {
                // 计算帧对应的矩形区域
                Rectangle pixelRect;
                int posX, poxY;
                int rowId, columnId;
                rowId = currentFrameId / ColumnCount;
                columnId = currentFrameId % ColumnCount;
                posX = columnId * frameWidth;
                poxY = rowId * frameHeight;
                pixelRect = new Rectangle(posX, poxY, frameWidth, frameHeight);
                Vector2 origion = new Vector2(frameHeight, frameWidth / 2);
                Painter.DrawT(texture, pixelRect, Position, origion, rotation, scale);
            }
            else
            {
                base.Draw();
            }
        }
        #endregion

        #region Update
        public override void Update()
        {
            base.Update();
            // 如果启用动画，则更新帧
            if (animSeqList.Count != 0)
            {
                if (!isIdle)
                {
                    if (currentFrameId >= currentSeq.EndFrameId) // 考虑单帧动画序列，使用“>”而不是“>=”
                    {
                        // 最后一帧也需要一段显示（静止）时间
                        if (BaseGame.TotalTimeMilliseconds - baseTimeMS > currentSeq.MsPerFrame + currentSeq.MsAllFrames)
                        {
                            if (currentSeq.looping)
                            {
                                currentFrameId = currentSeq.startFrameId;
                            }
                            else
                            {
                                isIdle = false;
                                currentSeq = animSeqList[1]; // 切换回第一个动画序列
                                currentFrameId = currentSeq.startFrameId; // 默认0号是Free
                                return;
                            }
                        }
                    }
                    else
                    {
                        int timeInCircle = (int)(BaseGame.TotalTimeMilliseconds - baseTimeMS) % currentSeq.MsAllFrames;
                        currentFrameId = timeInCircle / currentSeq.MsPerFrame + currentSeq.startFrameId;
                    }
                }
            }
        }
        #endregion

        #region PlaySeq
        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="actionName">动画名称</param>
        public void PlaySeq(String seqName)
        {
            if (animSeqList.Count == 0)
            {
                throw new Exception("该Item不包含任何动画序列数据");
            }
            if (currentSeq == null || isIdle || currentSeq.enableDistrub)
            {
                foreach (AnimSequence animSeq in animSeqList)
                {
                    if (seqName == animSeq.name)
                    {
                        isIdle = false;
                        currentSeq = animSeq;
                        currentFrameId = currentSeq.startFrameId;
                        // 重置播放时间
                        baseTimeMS = (int)BaseGame.TotalTimeMilliseconds;
                        break;
                    }
                }
            }
        }
        #endregion

        #region UnitTest
        public static void UnitTest()
        {
            AnimItem a = null;

            TestGame.StartTest("动画测试-1~5切换动画",
                null,
                delegate
                {
                    a = new AnimItem(LoadHelper.LoadTexture2D("soldier"), 32, 48, 17);
                    a.scale = 1.5f;
                    a.position = new Vector2(500, 400);
                    a.animSeqList.Add(new AnimSequence("Free", 40, 6, 8, true, true));
                    a.animSeqList.Add(new AnimSequence("Running", 0, 8, 8, true, true));
                    a.animSeqList.Add(new AnimSequence("Jumping", 8, 9, 8, true, false));
                    a.animSeqList.Add(new AnimSequence("UsingItem", 17, 8, 7, true, false));
                    a.animSeqList.Add(new AnimSequence("UsingGun", 24, 13, 12, true, false));
                    a.animSeqList.Add(new AnimSequence("UsingHook", 37, 1, 8, true, false));

                    a.PlaySeq("UsingGun");

                    SceneMgr.Instance.AddItem(a);
                },
                delegate
                {
                    // 通过数字键控制角色动画
                    if (InputKeyboards.isKeyJustPress(Keys.D1))
                    {
                        a.PlaySeq("Free");
                    }
                    if (InputKeyboards.isKeyJustPress(Keys.D2))
                    {
                        a.PlaySeq("Running");
                    }
                    if (InputKeyboards.isKeyJustPress(Keys.D3))
                    {
                        a.PlaySeq("Jumping");
                    }
                    if (InputKeyboards.isKeyJustPress(Keys.D4))
                    {
                        a.PlaySeq("UsingItem");
                    }
                    if (InputKeyboards.isKeyJustPress(Keys.D5))
                    {
                        a.PlaySeq("UsingGun");
                    }
                    if (InputKeyboards.isKeyJustPress(Keys.D6))
                    {
                        a.PlaySeq("UsingHook");
                    }
                    GameMgr.Instance.Update();
                },
                delegate
                {
                    GameMgr.Instance.Draw();
                }
                );


        }
        #endregion
    }
}
