using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Survival_DevelopFramework.InputSystem
{
    class InputKeyboards
    {
        #region 单件
        private static InputKeyboards instance = null;
        public static InputKeyboards Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputKeyboards();
                }
                return instance;
            }
        }
        #endregion

        #region 键盘控制变量
        //当前键盘状态
        private KeyboardState mCurrentKeyState;
        #endregion

        #region 键盘处理方法组
            
        public bool isKeyPress(Keys k)
        {
            mCurrentKeyState=Keyboard.GetState();
            return mCurrentKeyState.IsKeyDown(k);
        }
        #endregion
    }
}
