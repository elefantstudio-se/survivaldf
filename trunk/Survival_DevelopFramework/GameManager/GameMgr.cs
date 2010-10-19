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


namespace Survival_DevelopFramework.GameManager
{
    class GameMgr
    {
        #region 单件
        private static GameMgr instance = null;
        public static GameMgr Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameMgr();
                }
                return instance;
            }
        }
        #endregion

        #region 游戏控制变量
        private bool isScenePause;     //场景是否暂停
        private bool isUIPause;           //UI是否暂停
        private bool isEditorModel;     //是否为编辑模式
        #endregion

        #region 游戏循环控制方法组
        //游戏开始（即开始主循环）
        public void GameStart(SpriteBatch sb,GraphicsDevice gd,ContentManager c)
        {
            //初始化Graphics系统
            Painter.Instance.initGraphics(sb,gd);

            //初始化物理系统
            PhysicsSys.Instance.InitPhysics();

            //初始化helpers;
            LoadHelper.InitLoadHelper(c);

            //********Demo添加box********
            SceneMgr.Instance.addItem();
        }
        //游戏更新
        public void GameUpdate(GameTime gametime)
        {
            SceneMgr.Instance.UpdateItemList();
            SceneMgr.Instance.DrawItemList();
            //********Demo空格键增加一个item********
            if (InputKeyboards.Instance.isKeyPress(Microsoft.Xna.Framework.Input.Keys.Space))
                SceneMgr.Instance.addItem();
            //********Demo Esc清空页面(清空itemlist)********
            if (InputKeyboards.Instance.isKeyPress(Microsoft.Xna.Framework.Input.Keys.Escape))
                SceneMgr.Instance.ClearItemList();

            //更新物理系统
            PhysicsSys.Instance.UpdatePhysics(gametime.ElapsedGameTime.Milliseconds * 0.001f);
        }
        //游戏暂停
        public void GamePause(bool ui_pause)
        { 
        }
        //游戏结束 
        public void GameEnd()
        { 
        }
        #endregion
    }
}
