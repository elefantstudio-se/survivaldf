using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Storage;

namespace Survival_DevelopFramework.Helpers
{
    class LoadHelper
    {
        private const String TextureDir = "Textures";

        private static ContentManager mContent = BaseGame.ContentMgr;
        public static ContentManager Content
        {
            get { return mContent; }
        }

        static public Texture2D LoadTexture2D(string textureName)
        {
            return Content.Load<Texture2D>(Path.Combine(TextureDir,textureName));
        }

        public static FileStream LoadFileStream(string relativeFileName)
        {
            string fullPath = Path.Combine(
                StorageContainer.TitleLocation, relativeFileName);
            if (File.Exists(fullPath) == false)
                return null;
            else
                return File.Open(fullPath,
                    FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
    }
}
