using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Survival_DevelopFramework;

namespace Survival_DevelopFramework.InputSystem
{
    static class InputMouse
    {
        #region Variables
        /// <summary>
        /// 前一次键盘状态
        /// </summary>
        private static MouseState mJustMouseState;

        /// <summary>
        /// 当前键盘状态
        /// </summary>
        private static MouseState mCurrentMouseState;
        #endregion

        #region Properties
        static public int posX
        {
            get
            {
                return mCurrentMouseState.X;
            }
        }
        static public int posY
        {
            get
            {
                return mCurrentMouseState.Y;
            }
        }
        #endregion

        #region Update
        static public void Update()
        {
            // 保存当前的鼠标状态
            mJustMouseState = mCurrentMouseState;
            // 保存当前的鼠标状态
            mCurrentMouseState = Mouse.GetState();
        }
        #endregion

        #region 鼠标处理方法组
        //左键一直按、释放、单击
        static public bool isLeftMousePress()
        {
            if (mCurrentMouseState.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }
        static public bool isLeftMouseRelease()
        {
            if (mCurrentMouseState.LeftButton == ButtonState.Released)
                return true;
            return false;
        }
        static public bool isLeftMouseClick()
        {
            if (mCurrentMouseState.LeftButton == ButtonState.Pressed &&
                mJustMouseState.LeftButton == ButtonState.Released)
                return true;
            return false;
        }

        //右键一直按、释放、单击
        static public bool isRightMousePress()
        {
            if (mCurrentMouseState.RightButton == ButtonState.Pressed)
                return true;
            return false;
        }
        static public bool isRightMouseRelease()
        {
            if (mCurrentMouseState.RightButton == ButtonState.Released)
                return true;
            return false;
        }
        static public bool isRightMouseClick()
        {
            if (mCurrentMouseState.RightButton == ButtonState.Pressed &&
                 mJustMouseState .RightButton ==ButtonState.Released )
                return true;
            return false;
        }
        #endregion
    }
}
