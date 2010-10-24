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


namespace Survival_DevelopFramework.GameManager
{
    public class GameMgr
    {
        #region Variables
        static private GameMgr instance;
        private bool isScenePause;     //场景是否暂停
        private bool isUIPause;           //UI是否暂停
        private bool isEditorModel;     //是否为编辑模式
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

            //更新物理系统
            PhysicsSys.Instance.Update();
        }
        #endregion

        #region Draw
        public void Draw()
        {
            Painter.DrawBegin();
            SceneMgr.Instance.Draw();
            UIMgr.Instance.Draw();
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
