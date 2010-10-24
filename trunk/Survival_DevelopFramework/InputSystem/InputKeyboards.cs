using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Survival_DevelopFramework.InputSystem
{
    static class InputKeyboards
    {
        #region Variables
        /// <summary>
        /// 前一次键盘状态
        /// </summary>
        private static KeyboardState mLastKeyState;

        /// <summary>
        /// 当前键盘状态
        /// </summary>
        private static KeyboardState mCurrentKeyState;
        #endregion

        #region Update
        /// <summary>
        /// 更新键盘状态
        /// </summary>
        static public void Update()
        {
            // 保存上次键盘状态
            mLastKeyState = mCurrentKeyState;

            mCurrentKeyState = Keyboard.GetState();
        }
        #endregion

        #region 键盘处理方法组
        /// <summary>
        /// KeyPressed
        /// </summary>
        /// <param name="k">a key</param>
        /// <returns></returns>
        static public bool isKeyPress(Keys k)
        {
            return mCurrentKeyState.IsKeyDown(k);
        }

        /// <summary>
        /// KeyJustPressed
        /// </summary>
        /// <param name="k">a key</param>
        /// <returns></returns>
        static public bool isKeyJustPress(Keys k)
        {
            return mCurrentKeyState.IsKeyDown(k) && !mLastKeyState.IsKeyDown(k);
        }
        #endregion
    }
}
