using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survival_DevelopFramework.Items
{
    /// <summary>
    /// Item 管理器类
    /// 对Item列表进行常规处理
    /// </summary>
    class ItemMgr
    {
        #region Variables
        protected List<ItemBase> itemList = new List<ItemBase>();
        #endregion

        #region Instance
        protected ItemMgr instance;
        public ItemMgr Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    instance = new ItemMgr();
                    return instance;
                }
            }
        }
        /// <summary>
        /// 私有构造函数
        /// </summary>
        protected ItemMgr()
        {
            // 进行初始化处理...

        }
        #endregion

        #region Update
        virtual public void Update()
        {
            foreach (ItemBase item in itemList)
            {
                item.Update();
            }
        }
        #endregion

        #region Draw
        virtual public void Draw()
        {
            foreach(ItemBase item in itemList)
            {
                item.Draw();
            }
        }
        #endregion

        #region Item Control
        /// <summary>
        /// 添加Item
        /// </summary>
        /// <param name="item"></param>
        virtual public void AddItem(ItemBase item)
        {
            itemList.Add(item);
        }

        virtual public void ClearItemList()
        {
            itemList.Clear();
        }

        // 需要补充排序方法等等...
        #endregion
    }
}
