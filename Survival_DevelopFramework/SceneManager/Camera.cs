using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Survival_DevelopFramework.InputSystem;
using Microsoft.Xna.Framework.Input;

namespace Survival_DevelopFramework.SceneManager
{
    public class Camera
    {
        #region Variables

        /// <summary>
        /// 焦点
        /// </summary>
        public Vector2 Focus;
        /// <summary>
        /// 左上角
        /// </summary>
        public Vector2 UpLeft
        {
            get
            {
                return new Vector2(Focus.X - BaseGame.Width / 2, Focus.Y - BaseGame.Height / 2);
            }
        }

        /// <summary>
        /// 移动速度
        /// </summary>
        public int Speed;

        public enum CameraState
        {
            LockOnPlayer,
            Free,
        }
        /// <summary>
        /// 摄像机状态
        /// </summary>
        public CameraState cameraState;
        #endregion

        #region Constructor
        public Camera(int cameraSpeed)
        {
            this.Speed = cameraSpeed;
        }
        #endregion

        #region Update
        internal void Update()
        {
            // 自由摄像机
            if (cameraState == CameraState.Free)
            {
                // 自由移动
                float elapsedTime = BaseGame.ElapsedTimeThisFrameInMilliseconds / 1000;
                if (InputKeyboards.isKeyPress(Keys.W))
                {
                    Focus.Y -= Speed * elapsedTime;
                }
                if (InputKeyboards.isKeyPress(Keys.S))
                {
                    Focus.Y += Speed * elapsedTime;
                }
                if (InputKeyboards.isKeyPress(Keys.A))
                {
                    Focus.X -= Speed * elapsedTime;
                }
                if (InputKeyboards.isKeyPress(Keys.D))
                {
                    Focus.X += Speed * elapsedTime;
                }
            }
        }
        #endregion

        #region Control
        /// <summary>
        /// 设置绝对聚焦点
        /// </summary>
        public void SetAbsFocus(Vector2 focus)
        {
            Focus = focus;
        }
        #endregion
    }
}
