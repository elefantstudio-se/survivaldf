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
using Survival_DevelopFramework.Helpers;
using Survival_DevelopFramework.GameManager;
using FarseerGames.FarseerPhysics.Factories;

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
        /// Texture
        /// </summary>
        protected Texture2D texture;

        /// <summary>
        /// 显示层级
        /// </summary>
        public int layer = 0;

        /// <summary>
        /// Texture空间原点
        /// </summary>
        public Vector2 origin = Vector2.Zero;

        /// <summary>
        /// Body 
        /// 保存 Position Rotation 等信息
        /// </summary>
        protected Body body = BodyFactory.Instance.CreateRectangleBody(10,10,10);
        /// <summary>
        /// Texture到绝对场景 放缩
        /// </summary>
        protected Vector2 scale = new Vector2(1,1);

        /// <summary>
        /// 颜色
        /// </summary>
        public Color color = Color.White;

        /// <summary>
        /// 可见性
        /// </summary>
        public bool Visible = true;
        #endregion

        #region Properties
        /// <summary>
        /// body 位置
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return body.Position;
            }
            set
            {
                body.Position = value;
            }
        }
        /// <summary>
        /// body 旋转
        /// </summary>
        public float Rotation
        {
            get
            {
                return body.Rotation;
            }
            set
            {
                body.Rotation = value;
            }
        }
        /// <summary>
        /// 绝对尺寸
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return new Vector2(TexSize.X * scale.X,TexSize.Y * scale.Y);
            }
        }

        /// <summary>
        /// Texture 尺寸
        /// </summary>
        public Vector2 TexSize
        {
            get
            {
                return new Vector2(texture.Width, texture.Height);
            }
        }
        #endregion

        #region Advanced Field Setting
        /// <summary>
        /// 设置图形尺寸
        /// </summary>
        /// <param name="size"></param>
        public virtual void SetSize(Vector2 size)
        {
            // 保存位置旋转
            Vector2 bodyPos = body.Position;
            float bodyRot = body.Rotation;
            // 重建Body
            body = BodyFactory.Instance.CreateRectangleBody(size.X, size.Y, 10);
            // 恢复位置旋转
            body.Position = bodyPos;
            body.Rotation = bodyRot;

            // 关联scale
            scale.X = size.X / TexSize.X;
            scale.Y = size.Y / TexSize.Y;
        }

        /// <summary>
        /// 设置绝对原点
        /// 计算并保存Texture空间原点
        /// </summary>
        public virtual void SetOrigin(Vector2 absOrigin)
        {
            origin.X = absOrigin.X / scale.X;
            origin.Y = absOrigin.Y / scale.Y;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// ItemBase
        /// </summary>
        public ItemBase(String texturePath)
        {
            this.texture = LoadHelper.LoadTexture2D(texturePath);

            SetSize(new Vector2(TexSize.X, TexSize.Y));
        }
        /// <summary>
        /// 空的构造函数
        /// 限制使用于：1.编辑器 2.序列化支持
        /// </summary>
        public ItemBase()
        {
        }
        #endregion

        #region Draw
        /// <summary>
        /// 绘制Texture
        /// 覆盖此函数执行自己的逻辑
        /// </summary>
        public virtual void Draw()
        {
            if (Visible)
            {
                // 相对于摄像机的位置
                Vector2 camPos = Position - SceneMgr.Instance.camera.UpLeft;
                // 绘制
                Painter.DrawT(texture, camPos, origin, scale, Rotation, color);

                // 显示包围盒
                DrawBound();
            }
        }
        /// <summary>
        /// 绘制包围正交矩形
        /// 供Draw调用
        /// </summary>
        protected virtual void DrawBound()
        {
            if (GameMgr.Instance.showBound)
            {
                Painter.DrawBound(GetOrthoBound());
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Update
        /// </summary>
        public virtual void Update(){}
        #endregion

        #region Collation Detection
        /// <summary>
        /// 返回和坐标轴正交的矩形包围盒
        /// 用于：场景编辑器、碰撞检测优化
        /// 功能：
        /// 如果Item未旋转 -- 则返回它本身的矩形区域
        /// 如果Item旋转   -- 则返回它的界限矩形
        /// </summary>
        /// <returns></returns>
        public virtual Rectangle GetOrthoBound()
        {
            // Texture矩形
            Rectangle orgRect = new Rectangle(0, 0, (int)TexSize.X, (int)TexSize.Y);

            // 变换矩阵
            Matrix transMat = Matrix.Identity;
            // Item空间变换
            transMat *= Matrix.CreateTranslation(-origin.X, -origin.Y, 0); // Texture空间位置
            // 世界空间变换
            transMat *= Matrix.CreateScale(scale.X, scale.Y, 0); // Texture 尺寸映射到世界坐标尺寸
            transMat *= Matrix.CreateRotationZ(Rotation); // 世界空间旋转
            transMat *= Matrix.CreateTranslation(Position.X,Position.Y,0); // Texture位置映射到世界位置
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
        public static void UnitTest()
        {
            ItemBase item = null;
            TestGame.StartTest("ItemBase测试 F1-开启正交包围盒",
                null,
                delegate
                {
                    // item1
                    item = new ItemBase("GameandMe");
                    item.SetSize(new Vector2(100,100));
                    item.Position = new Vector2(400, 300);
                    item.origin = new Vector2(100, 100);
                    item.Rotation = 1.90f;
                    SceneMgr.Instance.AddItem(item);

                    // item2
                    item = new ItemBase("GameandMe");
                    item.SetSize(new Vector2(100, 100));
                    item.origin = new Vector2(100, -100);
                    item.Position = new Vector2(400, 300);
                    item.Rotation = -0.78f;
                    SceneMgr.Instance.AddItem(item);

                    // item3
                    item = new ItemBase("GameandMe");
                    item.SetSize(new Vector2(100, 100));
                    item.origin = new Vector2(0, 100);
                    item.Position = new Vector2(400, 300);
                    item.Rotation = -1.57f;
                    SceneMgr.Instance.AddItem(item);
                },
                delegate
                {
                },
                delegate
                {
                });
        }
        #endregion
    }
}
