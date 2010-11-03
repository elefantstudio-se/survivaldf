using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Survival_DevelopFramework.GraphicSystem;
using Survival_DevelopFramework.InputSystem;
using Survival_DevelopFramework.Helpers;
using Survival_DevelopFramework.SceneManager;
using Survival_DevelopFramework.PhysicsSystem;
using Survival_DevelopFramework.UISystem;
using Microsoft.Xna.Framework.Input;


namespace Survival_DevelopFramework.GameManager
{
    public class GameMgr
    {
        #region Variables
        static private GameMgr instance;
        private bool isScenePause = false;     //场景是否暂停
        private bool isUIPause = false;           //UI是否暂停
        private bool isEditorMode = false;     //是否为编辑模式
        
        // 显示版本提示信息
        private bool showInfo = true;
        // 显示正交包围矩形
        public bool showBound = false;
        // 显示Body
        public bool showBody = false;
        #endregion

        #region Properties
        /// <summary>
        /// 单例
        /// </summary>
        static public GameMgr Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    instance = new GameMgr();
                    return instance;
                }
            }
        }
        private GameMgr()
        {
            // 进行初始化处理...

        }
        #endregion

        #region Update
        //游戏更新
        public void Update()
        {
            // 更新场景管理器
            SceneMgr.Instance.Update();

            // 更新UI
            UIMgr.Instance.Update();

            //更新物理系统
            PhysicsSys.Instance.Update();

            // 提示消息更新
            if (InputKeyboards.isKeyJustPress(Keys.F12))
            {
                showInfo = showInfo ? false : true;
            }
            // 显示Bound
            if (InputKeyboards.isKeyJustPress(Keys.F9))
            {
                showBody = showBody ? false : true;
            }
            // 显示Body
            if (InputKeyboards.isKeyJustPress(Keys.F10))
            {
                showBound = showBound ? false : true;
            }
        }
        #endregion

        #region Draw
        public void Draw()
        {
            Painter.DrawBegin();
            SceneMgr.Instance.Draw();
            UIMgr.Instance.Draw();

            // 显示全局信息
            if (showInfo)
            {
                Painter.DrawLine("SDF内部编辑版", new Vector2(700, 10));
                Painter.DrawLine("F9  - 显示Body", new Vector2(700, 30));
                Painter.DrawLine("F10 - 显示包围盒", new Vector2(700, 50));
                Painter.DrawLine("F12 - 隐藏提示", new Vector2(700, 70));
            }
            else
            {
                Painter.DrawLine("F12 - 显示提示",new Vector2(700, 10));
            }


            Painter.DrawEnd();
        }
        #endregion

        #region GamePause
        //游戏暂停
        public void GamePause(bool ui_pause)
        { 
        }
        //游戏结束 
        static public void GameEnd()
        {
        }
        #endregion
    }
}
