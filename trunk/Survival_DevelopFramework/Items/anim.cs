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

namespace Survival_DevelopFramework.Items
{
    class anim : ItemBase
    {
        #region Constructor
        public anim()
        {
        }
        #endregion

        #region Variables
        private Texture2D texture;
        private int X;
        private int Y;
        private bool isIdle;
        protected int frameWidth;
        protected int frameHeight;
        protected int frameNumber;
        private int curFrameId;

        /// <summary>
        /// 当前帧号
        /// </summary>
        protected int currentFrame = 0;
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

        #region DrawSelf
        /// <summary>
        /// DrawSelf
        /// </summary>
        void ItemBase.DrawSelf()
        {
            // 计算帧对应的矩形区域
            Rectangle pixelRect;
            int posX, poxY;
            int rowId, columnId;
            rowId = curFrameId / ColumnCount;
            columnId = curFrameId % ColumnCount;
            posX = columnId * frameWidth;
            poxY = rowId * frameHeight;
            pixelRect = new Rectangle(posX, poxY, frameWidth, frameHeight);

            Painter.Instance.DrawT(texture, pixelRect, new Vector2(X, Y), 0, 1);
        }
        #endregion

        #region UpdateSelf
        void ItemBase.UpdateSelf()
        {
            if (!isIdle)
            {
                if (currentFrame >= currentSeq.EndFrameId)
                {
                    if (currentSeq.looping)
                    {
                        // 最后一帧也需要一段显示（静止）时间...
                        if (GameMgr.gameTimeInMs - baseTimeMS > currentSeq.MsPerFrame + currentSeq.MsAllFrames)
                        {
                            currentFrame = currentSeq.startFrameId;
                        }
                    }
                    else
                    {
                        isIdle = true;
                        currentFrame = 0; // 默认0号是Free
                        return;
                    }
                }
                int timeInCircle = (int)(GameMgr.gameTimeInMs - baseTimeMS) % currentSeq.MsAllFrames;
                curFrameId = timeInCircle / currentSeq.MsPerFrame + currentSeq.startFrameId;
            }
            //nowF += 0.3f;
            //if (nowF > 6) nowF -= 6;
        }
        #endregion

        #region ContentLoad
        void ItemBase.ContentLoad()
        {
            texture = LoadHelper.Content.Load<Texture2D>("zhuzhen");
        }
        #endregion 

        #region InitSelf
        void ItemBase.InitSelf()
        { 
            X = 700;
            Y = 200;

            frameWidth = 174;
            frameHeight = 125;
            animSeqList.Add(new AnimSequence("Default", 0, 6, 4, false, true));
            isIdle = true;
            PlaySeq("Default");
        }
        #endregion 

        #region PlaySeq
        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="actionName">动画名称</param>
        public void PlaySeq(String seqName)
        {
            if (currentSeq == null || isIdle || currentSeq.enableDistrub)
            {
                foreach (AnimSequence animSeq in animSeqList)
                {
                    if (seqName == animSeq.name)
                    {
                        isIdle = false;
                        currentSeq = animSeq;
                        currentFrame = currentSeq.startFrameId;
                        // 重置播放时间
                        baseTimeMS = (int)GameMgr.gameTimeInMs;
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
