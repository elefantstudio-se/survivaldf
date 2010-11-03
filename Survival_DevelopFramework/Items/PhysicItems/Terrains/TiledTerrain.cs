using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Survival_DevelopFramework.GraphicSystem;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework.Graphics;
using Survival_DevelopFramework.PhysicsSystem;
using FarseerGames.FarseerPhysics.Factories;
using Survival_DevelopFramework.Helpers;
using Survival_DevelopFramework.GameManager;

namespace Survival_DevelopFramework.Items.PhysicItems
{
    /// <summary>
    /// 重复地板
    /// </summary>
    public class TiledTerrain:Terrain
    {
        #region Variables
        /// <summary>
        /// 每个平铺单元的绝对显示尺寸
        /// </summary>
        private Vector2 tiledSize;
        /// <summary>
        /// 矩框原点
        /// </summary>
        private Vector2 rectOrigin;

        /// <summary>
        /// 平铺类型
        /// </summary>
        public enum TiledType
        {
            Both,
            Horizontal,
            Vertical,
        }
        TiledType tiledType;
        #endregion

        #region Properties
        /// <summary>
        /// 平铺Texture缩放
        /// </summary>
        public Vector2 TiledScale
        {
            get
            {
                return new Vector2(tiledSize.X / TexSize.X, tiledSize.Y / TexSize.Y);
            }
        }
        #endregion

        #region Constructor
        public TiledTerrain(String texturePath, Vector2 size, float frictionCoefficient, TiledType tiledType, Vector2 tileSize)
            : base(texturePath,size,frictionCoefficient)
        {
            this.tiledSize = tileSize;
            this.tiledType = tiledType;
            
        }
        /// <summary>
        /// 使用TiledTerrainData序列化类进行构造
        /// </summary>
        public TiledTerrain(TiledTerrainData tiledTerrainData)
        {
            // ItemBase
            texture = LoadHelper.LoadTexture2D("Terrains/"+tiledTerrainData.textureName);
            layer = tiledTerrainData.layer;

            // AnimItem

            // PhysicsItem
            SetSize(tiledTerrainData.size);
            SetOrigin(tiledTerrainData.size/2);
            body.Position = tiledTerrainData.position;
            body.Rotation = tiledTerrainData.rotation;
            body.IsStatic = true;

            // RectTerrain
            FrictionCoefficient = tiledTerrainData.frictionCoefficient;

            // TiledTerrain
            rectOrigin = Size / 2;
            tiledSize = tiledTerrainData.tiledSize;
           // origin = tiledTerrainData.tiledOrigin; -- 过期
            tiledType = tiledTerrainData.tiledType;
        }
        #endregion
        
        #region Draw
        public override void Draw()
        {
            if (Visible)
            {
                if (tiledType == TiledType.Both)
                {
                    BaseGame.Device.SamplerStates[0].AddressU = TextureAddressMode.Wrap;
                    BaseGame.Device.SamplerStates[0].AddressV = TextureAddressMode.Wrap;
                    Rectangle srcRect = new Rectangle(0, 0, (int)Math.Ceiling(Size.X * TiledScale.X), (int)Math.Ceiling(Size.Y * TiledScale.Y));
                    Vector2 Origin = new Vector2(srcRect.Width / 2, srcRect.Height / 2);
                    Painter.DrawTiledTerrain(texture, Position, srcRect, Origin, new Vector2(1, 1), Rotation);
                }
                else if (tiledType == TiledType.Horizontal)
                {
                    BaseGame.Device.SamplerStates[0].AddressU = TextureAddressMode.Wrap;
                    BaseGame.Device.SamplerStates[0].AddressV = TextureAddressMode.Wrap;
                    Rectangle srcRect = new Rectangle(0, 0, (int)(Size.X * TiledScale.X), (int)TexSize.Y);
                    Vector2 Origin = new Vector2(srcRect.Width / 2, srcRect.Height / 2);
                    Painter.DrawTiledTerrain(texture, Position, srcRect, Origin, new Vector2(1, 1), Rotation);
                }
                else if (tiledType == TiledType.Vertical)
                {
                    BaseGame.Device.SamplerStates[0].AddressU = TextureAddressMode.Wrap;
                    BaseGame.Device.SamplerStates[0].AddressV = TextureAddressMode.Wrap;
                    Rectangle srcRect = new Rectangle(0, 0, (int)TexSize.X, (int)(Size.Y  * TiledScale.Y));
                    Vector2 Origin = new Vector2(srcRect.Width / 2, srcRect.Height / 2);
                    Painter.DrawTiledTerrain(texture, Position, srcRect, Origin, new Vector2(1, 1), Rotation);
                }

                DrawBody();
                DrawBound();
            }
        }
        protected override void DrawBound()
        {
            if (GameMgr.Instance.showBound)
            {
                Painter.DrawBound(GetOrthoBound());
            }
        }
        #endregion

        #region Collation Detection
        /// <summary>
        /// 返回和坐标轴正交的矩形包围盒
        /// 用于：场景编辑器、碰撞检测优化
        /// 功能：
        /// 如果Item未旋转 -- 则返回它本身的矩形区域
        /// 如果Item旋转   -- 则返回它的界限矩形
        /// 
        /// 对于TiledTerrain 具有一个整体原点
        /// </summary>
        /// <returns></returns>
        public new Rectangle GetOrthoBound()
        {
            Rectangle orgRect = new Rectangle(0, 0, (int)Size.X, (int)Size.Y);

            // 变换矩阵
            Matrix transMat = Matrix.Identity;
            // Item空间变换
            Vector2 allOrigin = new Vector2(origin.X * scale.X, origin.Y * scale.Y); // 因为TiledTexture的Texture被平铺，所以要计算整体原点
            transMat *= Matrix.CreateTranslation(-allOrigin.X, -allOrigin.Y, 0); // Texture空间位置
            // 世界空间变换
            transMat *= Matrix.CreateScale(TiledScale.X, TiledScale.Y, 0); // Texture 尺寸映射到世界坐标尺寸
            transMat *= Matrix.CreateRotationZ(Rotation); // 世界空间旋转
            transMat *= Matrix.CreateTranslation(Position.X, Position.Y, 0); // Texture位置映射到世界位置
            // 摄像机空间变换 ...

            // 获取包围区域最值
            Vector2 leftTop = new Vector2(orgRect.Left, orgRect.Top);
            Vector2 rightTop = new Vector2(orgRect.Right, orgRect.Top);
            Vector2 leftBottom = new Vector2(orgRect.Left, orgRect.Bottom);
            Vector2 rightBottom = new Vector2(orgRect.Right, orgRect.Bottom);

            Vector2.Transform(ref leftTop, ref transMat, out leftTop);
            Vector2.Transform(ref rightTop, ref transMat, out rightTop);
            Vector2.Transform(ref leftBottom, ref transMat, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transMat, out rightBottom);

            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                    Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                    Vector2.Max(leftBottom, rightBottom));

            // 返回包围矩形
            return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }
        #endregion

        #region Unit Test
        public static new void UnitTest()
        {
            // 平铺地板
            TiledTerrain tiledTer = null;

            // 平铺单元尺寸
            Vector2 terTiledSize = Vector2.Zero;
            // 地板尺寸
            Vector2 terSize = Vector2.Zero;

            TestGame.StartTest("Tiled Terrain 测试",
                null,
                delegate
                {
                    // 双向平铺地板
                    terTiledSize = new Vector2(32,32);
                    terSize = new Vector2(800,100);
                    tiledTer = new TiledTerrain("Terrains/Tile_Both",terSize,1,TiledType.Both, terTiledSize);
                    tiledTer.Position = new Vector2(400,550);

                    // 垂直平铺地板
                    terTiledSize = new Vector2(32, 32);
                    terSize = new Vector2(100, 600);

                    // 水平平铺地板
                    terTiledSize = new Vector2(32, 64);
                    


                },
                delegate
                {
                    // 单击选择，查看地板属性
                },
                delegate
                {
                    // 显示地板属性
                }
            );
        }
        #endregion
    }
}
