using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survival_DevelopFramework.Items;
using Survival_DevelopFramework.GraphicSystem;
using Survival_DevelopFramework.PhysicsSystem;

namespace Survival_DevelopFramework.SceneManager
{
    class SceneMgr
    {
        #region 单件
        private static SceneMgr instance = null;
        public static SceneMgr Instance
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
        #endregion

        #region 构造函数（初始化ItemList）
        private SceneMgr()
        {
            ItemList = new List<ItemBase>();

            //***DEMO 添加了一个静态sbox和一个背景BG,一个动画，以及一个受键盘控制的非物理box,
            ItemList.Add(new BG());
            ItemList.Add(new sBox());
            ItemList.Add(new cBox());
            ItemList.Add(new anim());
            
        }
        #endregion

        private List<ItemBase> ItemList;

        #region 场景管理方法组
        //添加item
        public void addItem()
        {
            ItemList.Add(new aBox());
        }

        //更新所有item
        public void UpdateItemList()
        {
            foreach(ItemBase ib in ItemList)
            ib.UpdateSelf();
        }
        
        //绘制所有item
        public void DrawItemList()
        {
            Painter.Instance.DrawBegin();

            foreach (ItemBase ib in ItemList)
                ib.DrawSelf();

            Painter.Instance.DrawEnd();
        }

        public void ClearItemList()
        {
            //清空item列表
            ItemList.Clear();
            // 重置物理模拟环境
            PhysicsSys.Instance.PhysicsSimulator.Clear();
        }

        #endregion
    }
}
