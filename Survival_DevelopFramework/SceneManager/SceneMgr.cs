using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survival_DevelopFramework.Items;
using Survival_DevelopFramework.GraphicSystem;
using Survival_DevelopFramework.PhysicsSystem;
using Microsoft.Xna.Framework;
using Survival_DevelopFramework.GameManager;
using Survival_DevelopFramework.Items.DramaManager;
using Survival_DevelopFramework.Helpers;

namespace Survival_DevelopFramework.SceneManager
{
    class SceneMgr:ItemMgr
    {
        #region Variable
        /// <summary>
        /// 摄像机
        /// </summary>
        public Camera camera = null;
        #endregion

        #region 单件
        private new static SceneMgr instance = null;
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

        }
        #endregion

        #region Update
        //更新所有item
        public override void Update()
        {
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
            foreach (ItemBase item in itemList)
            {
                item.Draw();
            }
        }
        #endregion


        public override void ClearItemList()
        {
            base.ClearItemList();
            // 重置物理模拟环境
            PhysicsSys.Instance.PhysicsSimulator.Clear();
        }

        /// <summary>
        /// 使用SceneData构建SceneMgr的Item列表
        /// </summary>
        /// <param name="sceneData"></param>
        public void LoadSceneData(SceneData sceneData)
        {

        }
    }
}
