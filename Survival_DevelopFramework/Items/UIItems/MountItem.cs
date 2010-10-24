using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Survival_DevelopFramework.SceneManager;

namespace Survival_DevelopFramework.Items
{
    /// <summary>
    /// 挂载体
    /// 通常是不启用物理的装饰物
    /// </summary>
    class MountItem:AnimItem
    {
        #region Variables
        /// <summary>
        /// 父节点
        /// </summary>
        protected ItemBase parent;

        /// <summary>
        /// 子节点
        /// </summary>
        protected List<ItemBase> children = new List<ItemBase>();

        /// <summary>
        /// 相对位置
        /// </summary>
        public Vector2 relPos; 
        #endregion

        #region Properties
        /// <summary>
        /// 相对位置
        /// </summary>
        public Vector2 RelPos
        {
            get
            {
                if (parent != null)
                {
                    return Position - parent.Position;
                }
                else
                {
                    return Position;
                }
            }
        }
        #endregion

        #region Constructor
        public MountItem(Texture2D texture,ItemBase parent):base(texture)
        {
            this.parent = parent;
        }
        #endregion

        #region Update
        public override void Update()
        {
            // 上次parent的位置
            Vector2 parentLastPos = position - relPos;
            // parent的移动量
            Vector2 parentMV = parent.Position - parentLastPos;
            // pos 获得增量
            position += parentMV;
            // 重计算relPos
            relPos = position - parent.Position;

            // relPos在子类的重载中进行个性化处理..
        }
        #endregion
    }
}
