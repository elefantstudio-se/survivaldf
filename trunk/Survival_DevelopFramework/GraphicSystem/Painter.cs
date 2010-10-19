using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survival_DevelopFramework.GraphicSystem
{
    class Painter
    {
        #region 单件
        private static Painter instance = null;
        public static Painter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Painter();
                }
                return instance;
            }
        }
        #endregion

        #region 绘图变量
        private SpriteBatch mSpriteBatch;
        private GraphicsDevice mGraphicsDevice;
        public GraphicsDevice GraphicsDevice
        {
            get{return mGraphicsDevice;}
        }
        #endregion

        #region 初始化
        public void initGraphics(SpriteBatch sb,GraphicsDevice gd)
        {
            mSpriteBatch = sb;
            mGraphicsDevice = gd;
        }
        #endregion

        #region 绘制的方法组
        //begin
        public void DrawBegin()
        { 
            mSpriteBatch.Begin();
        }
        //end
        public void DrawEnd()
        { 
            mSpriteBatch.End();
        }
        //一下均为 demo所用，到时候要重写
        //按大小位置绘制一张texture2D矩形
        public void DrawT(Texture2D texture,Vector2 position,float rotation,float scale)
        {
            mSpriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0);
        }

        public void PlayA(Texture2D texture, Vector2 position, float rotation, float scale, Rectangle frect, int fnum, int fofrow, int i)
        {
            Rectangle currentrect = new Rectangle(frect.X + i % fofrow * frect.Width,
                                                        frect.Y + i / fofrow * frect.Height,
                                                        frect.Width,
                                                        frect.Height);
            mSpriteBatch.Draw(texture, position,currentrect, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2),scale, SpriteEffects.None, 0);
        }
        #endregion
    }
}
