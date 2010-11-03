using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework.Graphics;
using Survival_DevelopFramework.SceneManager;
using Survival_DevelopFramework.GraphicSystem;
using FarseerGames.FarseerPhysics.Factories;
using Survival_DevelopFramework.PhysicsSystem;
using Survival_DevelopFramework.GameManager;

namespace Survival_DevelopFramework.Items
{
    /// <summary>
    /// 物理体
    /// </summary>
    public class PhysicsItem:AnimItem
    {
        #region Variables
        /// <summary>
        /// Geom
        /// </summary>
        protected Geom geom;
        #endregion

        #region Properties
        /// <summary>
        /// 质量
        /// </summary>
        public float Mass
        {
            get
            {
                return body.Mass;
            }
            set
            {
                body.Mass = value;
            }
        }
        #endregion

        #region Advanced Field Setting
        /// <summary>
        /// 重载AnimItem的SetSize方法
        /// 1. 调用基类方法修改scale
        /// 2. 创建或重建物理体 构造尺寸
        /// 注意： 物理体Mass需要通过PhysicsItem.Mass单独设置
        /// </summary>
        public override void SetSize(Vector2 size)
        {
            // 设置图形尺寸
            base.SetSize(size);

            // 创建或重建物理体 
            // 默认取质量10
            body = BodyFactory.Instance.CreateRectangleBody(PhysicsSys.Instance.PhysicsSimulator,Size.X, Size.Y,10);
            geom = GeomFactory.Instance.CreateRectangleGeom(PhysicsSys.Instance.PhysicsSimulator, body, Size.X, Size.Y);
        }
        #endregion

        #region Constructor
        /// <summary>
        /// PhysicItem
        /// 不启用动画
        /// </summary>
        public PhysicsItem(String texturePath,Vector2 size,float mass):base(texturePath)
        {
            SetSize(size);
            Mass = mass;
        }

        /// <summary>
        /// PhysicItem
        /// 启用动画：必须设置动画帧尺寸
        /// 需要调用AddAnimSeq方法添加动画序列
        /// </summary>
        public PhysicsItem(String texturePath,Vector2 size,float mass,int frameWidth,int frameHeight,int frameNumber):base(texturePath,frameWidth,frameHeight,frameNumber)
        {
            SetSize(size);
            Mass = mass;
        }

        /// <summary>
        /// 空的构造函数
        /// 默认不启用动画：将EnableAnim设为true启用动画
        /// 限制使用于：1.编辑器 2.序列化支持
        /// </summary>
        public PhysicsItem():base()
        {
        }
        #endregion

        #region Draw
        /// <summary>
        /// 绘制物理体
        /// </summary>
        public override void Draw()
        {
            if (Visible)
            {
                base.Draw();

                // 显示Body
                DrawBody();
            }
        }
        protected virtual void DrawBody()
        {
            if (GameMgr.Instance.showBody)
            {
                Painter.DrawBody(Position, Size, Rotation);
            }
        }
        #endregion
    }
}
