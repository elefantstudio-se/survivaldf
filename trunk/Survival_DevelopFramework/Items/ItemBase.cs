using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Survival_DevelopFramework.SceneManager;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework.Graphics;
using Survival_DevelopFramework.GraphicSystem;

namespace Survival_DevelopFramework.Items
{
    /// <summary>
    /// ItemBase 基本类
    /// 是其他游戏对象的基类
    /// </summary>
    public class ItemBase
    {
        #region Variable
        /// <summary>
        /// 图案
        /// </summary>
        protected Texture2D texture;

        /// <summary>
        /// 显示层级
        /// </summary>
        protected int layer;

        /// <summary>
        /// 绝对位置
        /// </summary>
        protected Vector2 position;

        /// <summary>
        /// 旋转
        /// </summary>
        protected float rotation = 0;

        /// <summary>
        /// 放缩
        /// </summary>
        protected float scale = 1;
        #endregion

        #region Properties
        /// <summary>
        /// 绝对位置
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// ItemBase
        /// </summary>
        /// <param name="texture">图案</param>
        public ItemBase(Texture2D texture)
        {
            this.texture = texture;
        }
        #endregion

        #region Draw
        /// <summary>
        /// 绘制Texture
        /// 覆盖此函数执行自己的逻辑
        /// </summary>
        public virtual void Draw()
        {
            Vector2 camPos = new Vector2();
            camPos = Position;// -scene.camera.UpLeft;
            Painter.DrawT(texture, camPos, Color.White);
        }
        #endregion

        #region Update
        /// <summary>
        /// Update
        /// </summary>
        public virtual void Update(){ }
        #endregion
    }
}
