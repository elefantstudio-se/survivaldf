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
    [Serializable]
    public class AnimItem : ItemBase
    {
        #region Variables
        /// <summary>
        /// 启用动画
        /// </summary>
        public bool EnableAnim = false;
        /// <summary>
        /// 未播放动画
        /// </summary>
        private bool isIdle = true;
        /// <summary>
        /// 帧宽度
        /// </summary>
        protected int frameWidth;
        /// <summary>
        /// 帧高度
        /// </summary>
        protected int frameHeight;
        /// <summary>
        /// 帧数量
        /// </summary>
        protected int frameNumber;
        /// <summary>
        /// 当前帧号
        /// </summary>
        protected int currentFrameId;

        /// <summary>
        /// 播放总时间
        /// </summary>
        private int baseTimeMS = 0;

        /// <summary>
        /// 角色动作序列
        /// </summary>
        #region AnimSequence Class
        [Serializable]
        public class AnimSequence
        {
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
            /// <summary>
            /// 支持序列化
            /// </summary>
            public AnimSequence()
            {
            }
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
        #endregion
        public List<AnimSequence> animSeqList = new List<AnimSequence>();

        /// <summary>
        ///  当前动作序列
        /// </summary>
        protected AnimSequence currentSeq;
        #endregion

        #region Constructor
        /// <summary>
        /// AnimItem
        /// 预设动画帧参数
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="frameWidth"></param>
        /// <param name="frameHeight"></param>
        /// <param name="frameNumber"></param>
        public AnimItem(String texturePath,int frameWidth,int frameHeight,int frameNumber):base(texturePath)
        {
           EnableAnim = true;

           this.frameHeight = frameHeight;
           this.frameWidth = frameWidth;
           this.frameNumber = frameNumber;
        }
        /// <summary>
        /// AnimItem
        /// 给不启用动画的子类使用
        /// </summary>
        /// <param name="tex"></param>
        public AnimItem(String texturePath)
            : base(texturePath)
        {
            EnableAnim = false;

            frameWidth = texture.Width;
            frameHeight = texture.Height;
            SetSize(new Vector2(frameWidth,frameHeight));
        }
        /// <summary>
        /// 空的构造函数
        /// 限制使用于：
        /// 1.编辑器
        /// 2.序列化支持
        /// </summary>
        public AnimItem()
            : base()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// 帧宽度
        /// </summary>
        public int FrameWidth
        {
            get
            {
                return frameWidth;
            }
        }

        /// <summary>
        /// 帧高度
        /// </summary>
        public int FrameHeight
        {
            get
            {
                return frameHeight;
            }
        }

        /// <summary>
        /// 帧列数
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return texture.Width / frameWidth;
            }
        }

        /// <summary>
        /// 帧行数
        /// </summary>
        public int RowCount
        {
            get
            {
                return texture.Height / frameHeight;
            }
        }

        /// <summary>
        /// 绝对尺寸
        /// 如果启用动画   -- 返回帧尺寸和scale的乘积
        /// 如果不启用动画 -- 返回TexSize和scale的乘积
        /// </summary>
        public new Vector2 Size
        {
            get
            {
                if(EnableAnim)
                {
                    return new Vector2(frameWidth * scale.X, frameHeight * scale.Y);
                }
                else
                {
                    return new Vector2(TexSize.X * scale.X, TexSize.Y * scale.Y);
                }
            }
        }
        #endregion

        #region Advanced Field Setting
        /// <summary>
        /// SetSize
        /// 如果不启用动画 -- 设置Texture整体scale
        /// 如果启用动画   -- 计算帧scale
        /// </summary>
        /// <param name="size"></param>
        public override void SetSize(Vector2 size)
        {
            if (EnableAnim)
            {
                body = BodyFactory.Instance.CreateRectangleBody(size.X, size.Y, 10);

                scale.X = size.X / frameWidth;
                scale.Y = size.Y / frameHeight;
            }
            else
            {
                base.SetSize(size);
            }
        }
        #endregion

        #region Draw
        /// <summary>
        /// Draw
        /// 功能：
        /// 如果启用动画   -- 则绘制帧
        /// 如果不启用动画 -- 则绘制整个Texture
        /// </summary>
        public override void Draw()
        {
            if (Visible)
            {
                // 如果启用动画则绘制帧
                // 否则使用基类的绘制方式
                if (EnableAnim)
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
                    Painter.DrawT(texture, pixelRect, origin, Position, scale, Rotation);

                    // 显示包围盒
                    DrawBound();
                }
                else
                {
                    base.Draw();
                }
            }
        }
        /// <summary>
        /// 绘制包围正交矩形
        /// 供Draw调用
        /// </summary>
        protected override void DrawBound()
        {
            if (GameMgr.Instance.showBound)
            {
                Painter.DrawBound(GetOrthoBound());
            }
        }
        #endregion

        #region Collation Detection
        /// <summary>
        /// 返回和坐标轴正交的矩形包围盒
        /// 用于：场景编辑器、碰撞检测优化
        /// 功能：
        /// 如果Item未旋转 -- 则返回它本身的矩形区域
        /// 如果Item旋转   -- 则返回它的界限矩形
        /// 
        /// AnimItem重载，根据frameWidth和frameHeight计算
        /// </summary>
        /// <returns></returns>
        public override Rectangle GetOrthoBound()
        {
            // Texture矩形
            Rectangle orgRect;
            if(EnableAnim)
            {
                orgRect = new Rectangle(0, 0, frameWidth, frameHeight);
            }
            else
            {
                orgRect = new Rectangle(0, 0, (int)TexSize.X, (int)TexSize.Y);
            }

            // 变换矩阵
            Matrix transMat = Matrix.Identity;
            // Item空间变换
            transMat *= Matrix.CreateTranslation(-origin.X, -origin.Y, 0); // Texture空间位置
            // 世界空间变换
            transMat *= Matrix.CreateScale(scale.X, scale.Y, 0); // Texture 尺寸映射到世界坐标尺寸
            transMat *= Matrix.CreateRotationZ(Rotation); // 世界空间旋转
            transMat *= Matrix.CreateTranslation(Position.X, Position.Y, 0); // Texture位置映射到世界位置
            // 摄像机空间变换 ...

            // 获取包围区域最值
            Vector2 leftTop = new Vector2(orgRect.Left, orgRect.Top);
            Vector2 rightTop = new Vector2(orgRect.Right, orgRect.Top);
            Vector2 leftBottom = new Vector2(orgRect.Left, orgRect.Bottom);
            Vector2 rightBottom = new Vector2(orgRect.Right, orgRect.Bottom);

            Vector2.Transform(ref leftTop, ref transMat, out leftTop);
            Vector2.Transform(ref rightTop, ref transMat, out rightTop);
            Vector2.Transform(ref leftBottom, ref transMat, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transMat, out rightBottom);

            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                    Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                    Vector2.Max(leftBottom, rightBottom));

            Rectangle r = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
            // 返回包围矩形
            return r;
        }
        #endregion

        #region Update
        /// <summary>
        /// 更新
        /// 如果启用动画 -- 则更新帧
        /// </summary>
        public override void Update()
        {
            base.Update();
            // 如果启用动画，则更新帧
            if (EnableAnim)
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
            if (!EnableAnim)
            {
                throw new Exception("该Item未启用动画");
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
        public static new void UnitTest()
        {
            AnimItem a = null;

            TestGame.StartTest("动画测试-1~5切换动画",
                null,
                delegate
                {
                    a = new AnimItem("Roles/soldier", 32, 48, 17);
                    a.SetSize(new Vector2(48,64));
                    a.Position = new Vector2(500, 400);
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
                },
                delegate
                {
                }
                );
        }
        #endregion
    }
}
