using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survival_DevelopFramework.GraphicSystem
{
    static class Painter
    {
        #region 绘图变量
        static private GraphicsDevice mGraphicsDevice = BaseGame.Device;
        static private SpriteBatch mSpriteBatch = new SpriteBatch(mGraphicsDevice);
        #endregion

        #region 绘制的方法组
        //begin
        static public void DrawBegin()
        { 
            mSpriteBatch.Begin();
        }
        //end
        static public void DrawEnd()
        { 
            mSpriteBatch.End();
        }
        //一下均为 demo所用，到时候要重写
        //按大小位置绘制一张texture2D矩形
        static public void DrawT(Texture2D texture, Vector2 position, float rotation, float scale)
        {
            mSpriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0);
        }

        static public void DrawT(Texture2D texture, Rectangle srcRect, Vector2 position, Vector2 origin,float rotation, float scale)
        {
            mSpriteBatch.Draw(texture, position, srcRect, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
        }
        static public void DrawT(Texture2D texture, Rectangle srcRect, Vector2 position, float rotation, float scale)
        {
            mSpriteBatch.Draw(texture, position, srcRect, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0);
        }
        static public void DrawT(Texture2D texture, Vector2 position, Color color)
        {
            mSpriteBatch.Draw(texture,position,color);
        }
        static public void DrawT(Texture2D texture, Rectangle srcRect, Rectangle destRect)
        {
            mSpriteBatch.Draw(texture, srcRect, destRect,Color.White);
        }
        static public void DrawT(Texture2D texture, Rectangle srcRect, Rectangle destRect, Color color)
        {
            mSpriteBatch.Draw(texture, srcRect, destRect, color);
        }
        static public void DrawT(Texture2D texture, Rectangle destRect)
        {
            mSpriteBatch.Draw(texture, destRect,Color.White);
        }

        #endregion
    }
}
