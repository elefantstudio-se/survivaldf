using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Survival_DevelopFramework.GraphicSystem;
using Survival_DevelopFramework.PhysicsSystem;
using Survival_DevelopFramework.Helpers;
using FarseerGames.FarseerPhysics.Factories;

namespace Survival_DevelopFramework.Items.PhysicItems
{
    /// <summary>
    /// 矩形地板
    /// </summary>
    public class Terrain:PhysicsItem
    {
        #region Properties
        /// <summary>
        /// 摩擦因数
        /// </summary>
        public float FrictionCoefficient
        {
            get
            {
                return geom.FrictionCoefficient;
            }
            set
            {
                geom.FrictionCoefficient = value;
            }   
        }
        #endregion

        #region Constructor
        public Terrain(String texturePath, Vector2 size, float frictionCoefficient)
            : base(texturePath, size, 10)
        {
            geom.FrictionCoefficient = frictionCoefficient;

            // 地板保持静止
            body.IsStatic = true;
        }
        /// <summary>
        /// 使用TerrainData序列化类进行构造
        /// </summary>
        public Terrain(TerrainData terrainData)
        {
            // 从基类开始设置字段、载入资源

            // ItemBase
            texture = LoadHelper.LoadTexture2D("Terrains/" + terrainData.textureName);
            layer = terrainData.layer;

            // AnimItem

            // PhysicsItem
            SetSize(terrainData.size);
            SetOrigin(terrainData.size / 2);
            body.Position = terrainData.position;
            body.Rotation = terrainData.rotation;

            // Terrain
            FrictionCoefficient = terrainData.frictionCoefficient;
            body.IsStatic = true;
        }
        /// <summary>
        /// 仅供编辑器和派生类序列化使用
        /// </summary>
        public Terrain()
        {
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
