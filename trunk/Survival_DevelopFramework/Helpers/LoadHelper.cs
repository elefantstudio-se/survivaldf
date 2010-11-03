using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Storage;
using Survival_DevelopFramework.SceneManager;
using System.Threading;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework;

namespace Survival_DevelopFramework.Helpers
{
    class LoadHelper
    {
        #region Constants
        private const String TextureDir = "Textures";
        private const String FontDir = "Fonts";
        #endregion 

        #region StorageDevice
        public static ManualResetEvent StorageContainerMRE = new ManualResetEvent(true);

        static StorageDevice xnaUserDevice = null;
        public static StorageDevice XnaUserDevice
        {
            get
            {
                if ((xnaUserDevice != null) && !xnaUserDevice.IsConnected)
                {
                    xnaUserDevice = null;
                }
                if (xnaUserDevice == null)
                {
                    if (Guide.IsVisible)
                    {
                        return null;
                    }
                    IAsyncResult async =
                        Guide.BeginShowStorageDeviceSelector(PlayerIndex.One, null, null);
                    while (!async.IsCompleted)
                    {
                        Thread.Sleep(10);
                        BaseGame.Device.Clear(Color.Black);
                        BaseGame.Device.Present();
                    }
                    xnaUserDevice = Guide.EndShowStorageDeviceSelector(async);
                }
                return xnaUserDevice;
            }
        }
        #endregion

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

        static public SpriteFont LoadSpriteFont(string fontFileName)
        {
            return Content.Load<SpriteFont>(Path.Combine(FontDir, fontFileName));
        }
    }
}
