using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survival_DevelopFramework.Items;
using Survival_DevelopFramework.GraphicSystem;
using Survival_DevelopFramework.PhysicsSystem;
using Microsoft.Xna.Framework;
using Survival_DevelopFramework.GameManager;
using Survival_DevelopFramework.Items.MovieManager;
using Survival_DevelopFramework.Helpers;
using Survival_DevelopFramework.Items.PhysicItems;
using Survival_DevelopFramework.ItemDatas;

namespace Survival_DevelopFramework.SceneManager
{
    class SceneMgr:ItemMgr
    {
        #region Variable
        /// <summary>
        /// 游戏阶段
        /// </summary>
        public enum gameStage
        {
            MainMenu,
            InGame,
        }
        public gameStage GameStage = gameStage.MainMenu;

        /// <summary>
        /// 电影模式
        /// </summary>
        private bool inMovieMode = false;

        /// <summary>
        /// 摄像机
        /// </summary>
        public Camera camera = null;

        /// <summary>
        /// 背景管理器
        /// </summary>
        public ItemMgr backgroundMgr;

        /// <summary>
        /// 地形管理器
        /// </summary>
        public TerrainMgr terrainMgr;

        /// <summary>
        /// 交互对象管理器
        /// </summary>
        public ActItemMgr actItemMgr;

        /// <summary>
        /// 游戏角色
        /// </summary>
        public Role role;

        /// <summary>
        /// 前景管理器
        /// </summary>
        public ItemMgr forgroundMgr;

        /// <summary>
        /// 触发连协管理 -- 尚未肯定，可能Item本身就能够完成
        /// </summary>
        //TriggerLink triggerLink;
        #endregion

        #region 单件
        private static SceneMgr instance = null;
        /// <summary>
        /// 单例
        /// </summary>
        public new static SceneMgr Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneMgr();
                }
                return instance;
            }
        }
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private SceneMgr()
        {
            // 进行初始化处理...
            camera = new Camera(50);
            camera.Focus = new Vector2(BaseGame.Width/2, BaseGame.Height/2);
        }
        #endregion

        #region Update
        //更新所有item
        public override void Update()
        {
            // -- 在电影模式中可能需要调整
            if (camera != null)
            {
                camera.Update();
            }
            if (backgroundMgr != null)
            {
                backgroundMgr.Update();
            }
            if (terrainMgr != null)
            {
                terrainMgr.Update();
            }
            if (actItemMgr != null)
            {
                actItemMgr.Update();
            }
            if (role != null && inMovieMode == false)
            {
                role.Update();
            }
            if (forgroundMgr != null)
            {
                forgroundMgr.Update();
            }

            foreach (ItemBase item in itemList)
            {
                item.Update();
            }
        }
        #endregion

        #region Draw
        //绘制所有item
        public override void Draw()
        {
            if (backgroundMgr != null)
            {
                backgroundMgr.Draw();
            }
            if (terrainMgr != null)
            {
                terrainMgr.Draw();
            }
            if (actItemMgr != null)
            {
                actItemMgr.Draw();
            }
            if (role != null && inMovieMode == false)
            {
                role.Draw();
            }
            if (forgroundMgr != null)
            {
                forgroundMgr.Draw();
            }

            foreach (ItemBase item in itemList)
            {
                item.Draw();
            }
        }
        #endregion

        #region Common Functions
        /// <summary>
        /// 分析场景数据
        /// 使用SceneData构建SceneMgr的Item列表
        /// </summary>
        /// <param name="sceneData"></param>
        public void LoadScene(String sceneName)
        {
            // 读取文件
            SceneData sceneData = SceneData.Load(sceneName);

            // 读取背景
            backgroundMgr = new ItemMgr();
            foreach (BackgroundData backgroundData in sceneData.backgroundDataList)
            {
                backgroundMgr.AddItem(new Background(backgroundData));
            }
            

            // 读取场景地板...
            terrainMgr = new TerrainMgr();
            foreach (TerrainData terrainData in sceneData.terrainDataList)
            {
                terrainMgr.AddItem(new Terrain(terrainData));
            }
            foreach (TiledTerrainData tiledTerrainData in sceneData.tiledTerrainDataList)
            {
                terrainMgr.AddItem(new TiledTerrain(tiledTerrainData));
            }

            // 读取位置信息...
            // 包括角色出生点、道具、交互物体、开关触发器、门等等的位置信息...
            // 并关联程序中定义的触发行为 （触发行为包括：获得物品、物理效应、电影模式等等）
            // （另外还有一些非触发行为： 使用道具枪支钩绳，这些部分适合定义在管理器中，而不是角色本身）

            // 读取连协开关...
            // 设定这类对象的关联参数
            // 并关联程序中定义的触发行为

            // 读取动画数据...
            // 包括事件列表
            // 动画的构成包括 -- Item的动态创建、动画、物理效应、固定暂停、角色对话心理、摄像机动画、屏幕特效、音乐切换、音效等等及其组合表现形式

            // 读取前景、等杂项...

            // 读取完毕
        }

        /// <summary>
        /// 切换场景
        /// </summary>
        /// <param name="sceneName"></param>
        public void ChangeScene(String sceneName)
        {
            // 清空场景
            ResetScene();
            // 载入对应的Scene文件
            LoadScene(sceneName);

            // 从存档信息中查找此Scene的开关数据是否存在...

            // 如果存在则使用存档的设定...

            // 否则使用Scene的默认设定...

        }
        /// <summary>
        /// 退出场景
        /// 例如：在快捷菜单中选择退出游戏
        ///       返回主菜单
        /// </summary>
        public void QuitScene()
        {
            // 如果当前数据未保存，则弹出提示信息...

            // 退出游戏...

        }
        public override void ClearItemList()
        {
            base.ClearItemList();
            // 重置物理模拟环境
            PhysicsSys.Instance.PhysicsSimulator.Clear();
        }
        #endregion

        #region MainMenu Functions
        /// <summary>
        /// 读取玩家数据
        /// </summary>
        /// <param name="saveFileName"></param>
        public void LoadProfile(String profileName)
        {
            // 载入玩家数据，分析存档...

            // 载入存档，处理玩家信息和开关信息 ...

            // 载入玩家所在的Scene
        }
        #endregion 

        #region InGame Functions
        /// <summary>
        /// 清空场景
        /// </summary>
        public void ResetScene()
        {
            // 重置物理模拟环境
            PhysicsSys.Instance.PhysicsSimulator.Clear();
            // 重置摄像机...

            // 清空ItemMgr
            ClearItemList(); // 过期..
            backgroundMgr = null;
            terrainMgr = null;
            actItemMgr = null;
            role = null;
            forgroundMgr = null;
            
        }


        /// <summary>
        /// 是否进入电影模式
        /// </summary>
        /// <param name="isMovieOn"></param>
        public void StartMovie(String movieName)
        {
            inMovieMode = true;
            // 其他 ....
        }

        /// <summary>
        /// 保存游戏
        /// </summary>
        public void SaveProfile(string profileName)
        {
            // 查找玩家数据中的最新场景...

            // 载入存档场景，或者载入默认场景
        }
        #endregion
    }
}
