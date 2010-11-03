using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Survival_DevelopFramework.Helpers;

namespace Survival_DevelopFramework.GraphicSystem
{
    static class Painter
    {
        #region 绘图变量
        static private GraphicsDevice mGraphicsDevice = BaseGame.Device;
        static private SpriteBatch mSpriteBatch = new SpriteBatch(mGraphicsDevice);
        static private SpriteFont mSpriteFont = LoadHelper.LoadSpriteFont("Default");
        // 测试用
        static private Texture2D bodyTexture = LoadHelper.LoadTexture2D("Body");
        static private Vector2 bodyOrigin = new Vector2(2, 2);
        static private Texture2D boundTexture = LoadHelper.LoadTexture2D("Bound");
        static private Vector2 boundOrigin = new Vector2(2, 2);
        #endregion

        #region 绘制的方法组
        //begin
        static public void DrawBegin()
        { 
            mSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, 
                SpriteSortMode.Immediate, SaveStateMode.None);
        }
        //end
        static public void DrawEnd()
        { 
            mSpriteBatch.End();
        }

        /// <summary>
        /// 元DrawT方法 - 1
        /// </summary>
        static public void DrawT(Texture2D texture, Vector2 position,Color color,Rectangle srcRect,float rotation,Vector2 origin,Vector2 scale)
        {
            mSpriteBatch.Draw(texture, position, srcRect, color, rotation, origin, scale,SpriteEffects.None,0);
        }

        /// <summary>
        /// 
        /// 特点： 指定向量scale，指定目标点
        /// 暂记： 被ItemBase调用
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="origin"></param>
        /// <param name="scale"></param>
        static public void DrawT(Texture2D texture, Vector2 position,Vector2 origin,Vector2 scale, float rotation)
        {
            mSpriteBatch.Draw(texture, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
        }
        static public void DrawT(Texture2D texture, Vector2 position, Vector2 origin, Vector2 scale, float rotation,Color color)
        {
            mSpriteBatch.Draw(texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 0);
        }

        // 使用向量scale的情况下，指定目标矩形没有太大意义

        /// <summary>
        /// 
        /// 特点： 指定向量scale，指定源矩形，指定绘制位置
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="srcRectangle"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        static public void DrawT(Texture2D texture, Rectangle? srcRectangle, Vector2 origin, Vector2 position, Vector2 scale, float rotation)
        {
            mSpriteBatch.Draw(texture, position, srcRectangle, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
        }
        // 增加SpriteEffects
        static public void DrawT(Texture2D texture, Rectangle? srcRectangle, Vector2 origin, Vector2 position, Vector2 scale, float rotation, SpriteEffects spriteEffect)
        {
            mSpriteBatch.Draw(texture, position, srcRectangle, Color.White, rotation, origin, scale, spriteEffect, 0);
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
        static public void DrawT(Texture2D texture, Rectangle destRect, Color color)
        {
            mSpriteBatch.Draw(texture, destRect, color);
        }

        #endregion

        #region Draw Text
        public static void DrawLine(string str,Vector2 position)
        {
            mSpriteBatch.DrawString(mSpriteFont, str, position, Color.White);
        }
        #endregion

        static public void DrawBody(Vector2 position,Vector2 size,float rotation)
        {
            Color halfAlpha = new Color(225,225,225,128);
            Vector2 scale = new Vector2(size.X/bodyTexture.Width,size.Y/bodyTexture.Height);
            Painter.DrawT(bodyTexture, position, bodyOrigin, scale, rotation,halfAlpha);
        }

        static public void DrawBound(Rectangle destRect)
        {
            Color halfAlpha = new Color(225, 225, 225, 128);
            Painter.DrawT(boundTexture,destRect,halfAlpha);
        }

        static public void DrawTiledTerrain(Texture2D texture, Vector2 position, Rectangle? srcRectangle ,Vector2 origin, Vector2 scale, float rotation)
        {
            mSpriteBatch.Draw(texture, position, srcRectangle, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
        }
    }
}
