using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework.Graphics;
using Survival_DevelopFramework.SceneManager;

namespace Survival_DevelopFramework.Items
{
    /// <summary>
    /// 物理体
    /// </summary>
    class PhysicItem:AnimItem
    {
        #region Variables
        /// <summary>
        /// Body
        /// </summary>
        protected Body body;
        /// <summary>
        /// Geom
        /// </summary>
        protected Geom geom;
        #endregion

        #region Constructor
        /// <summary>
        /// PhysicItem
        /// 不启用动画
        /// 如果需要启用动画，需要设置frameHeight、frameWidth和frameNumber
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="body"></param>
        /// <param name="geom"></param>
        public PhysicItem(Texture2D texture,Body body, Geom geom):base(texture)
        {
            this.body = body;
            this.geom = geom;
        }
        /// <summary>
        /// PhysicItem
        /// 启用动画
        /// 预设动画帧尺寸
        /// 需要调用AddAnimSeq方法添加动画序列
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="frameWidth"></param>
        /// <param name="frameHeight"></param>
        /// <param name="frameNumber"></param>
        /// <param name="body"></param>
        /// <param name="geom"></param>
        public PhysicItem(Texture2D texture,int frameWidth,int frameHeight,int frameNumber, Body body, Geom geom):base(texture,frameWidth,frameHeight,frameNumber)
        {
            this.body = body;
            this.geom = geom;
        }
        #endregion

        #region Update
        public override void Update()
        {
            base.Update();
            position = body.Position;
            rotation = body.Rotation;
        }
        #endregion

        #region Draw
        public override void Draw()
        {
            base.Draw();
        }
        #endregion
    }
}
