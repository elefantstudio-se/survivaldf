using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Survival_DevelopFramework.Helpers;
using Microsoft.Xna.Framework;

namespace Survival_DevelopFramework.GraphicSystem
{
    class UTF8Writer
    {
        #region Constructors
        public UTF8Writer(String spriteFontName)
        {
            // 载入字体
            spriteFont = LoadHelper.Content.Load<SpriteFont>("Fonts//" + spriteFontName);
            // 实例化
            spriteBatch = new SpriteBatch(Survival_Game.Device);
        }

        #endregion

        #region Variables
        /// <summary>
        /// Font texture
        /// </summary>
        private static SpriteFont spriteFont;
        /// <summary>
        /// Font sprite
        /// </summary>
        private static SpriteBatch spriteBatch = new SpriteBatch(Survival_Game.Device);
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (spriteBatch != null)
            {
                spriteBatch.Dispose();
                spriteBatch = null;
            }
        }
        #endregion

        #region Write All
        internal class UTF8Line
        {
            #region Variables
            /// <summary>
            /// 坐标位置
            /// </summary>
            public int x, y;
            /// <summary>
            /// 行文本
            /// </summary>
            public string text;
            /// <summary>
            /// 颜色
            /// </summary>
            public Color color;
            #endregion

            #region Constructor
            public UTF8Line(int set_x, int set_y, string set_lineStr, Color set_color)
            {
                x = set_x;
                y = set_y;
                text = set_lineStr;
                color = set_color;
            }
            #endregion

        }
        static List<UTF8Line> remTexts = new List<UTF8Line>();

        /// <summary>
        /// 在指定的位置显示指定颜色的行文本
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="lineStr">行文本</param>
        /// <param name="color">颜色</param>
        public static void WriteLine(int x, int y, string lineStr, Color color)
        {
            remTexts.Add(new UTF8Line(x, y, lineStr, color));
        } // WriteLine(x, y, lineStr, color)

        /// <summary>
        /// 在指定的位置显示白色的行文本
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="text">Text</param>
        public static void WriteText(int x, int y, string lineStr)
        {
            remTexts.Add(new UTF8Line(x, y, lineStr, Color.White));
        } // WriteText(x, y, text)

        public static void WriteAll()
        {
            if (remTexts.Count == 0 ||
                spriteBatch == null)
                return;

            // Start rendering
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            // Draw each character in the text
            //foreach (UIRenderer.FontToRender fontText in texts)
            for (int textNum = 0; textNum < remTexts.Count; textNum++)
            {
                UTF8Line fontText = remTexts[textNum];
                spriteBatch.DrawString(spriteFont, fontText.text, new Vector2(fontText.x, fontText.y), fontText.color);
            } // foreach (fontText)

            // End rendering
            spriteBatch.End();

            remTexts.Clear();
        }
        #endregion

        public void Load()
        {
        }
    }
}
