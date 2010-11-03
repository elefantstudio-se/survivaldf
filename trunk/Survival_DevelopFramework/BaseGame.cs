using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Survival_DevelopFramework.InputSystem;

namespace Survival_DevelopFramework
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BaseGame : Microsoft.Xna.Framework.Game
    {
        #region Variables
        /// <summary>
        /// Graphic Device Manager
        /// </summary>
        GraphicsDeviceManager graphics;

        protected static ContentManager contentMgr;
        /// <summary>
        /// 游戏的GraphicDevice
        /// 允许任何需要的地方通过Device属性访问它
        /// </summary>
        private static GraphicsDevice device;

        /// <summary>
        /// 游戏的分辨率
        /// </summary>
        protected static int width, height;


        /// <summary>
        /// 游戏宽高比
        /// </summary>
        private static float aspectRatio = 1.0f;

        /// <summary>
        /// 游戏时间
        /// </summary>
        private static float elapseTimeThisFrameInMs = 0.001f, totalTimeMs = 0;
        #endregion 

        #region Property
        #region Content
        public static ContentManager ContentMgr
        {
            get
            {
                return contentMgr;
            }
        }
        #endregion 
        
        #region Device
        /// <summary>
        /// GraphicDevice 允许全局访问
        /// </summary>
        public static GraphicsDevice Device
        {
            get
            {
                return device;
            }
        }
        #endregion

        #region Resolution
        public static int Width
        {
            get
            {
                return width;
            }
        }
        public static int Height
        {
            get
            {
                return height;
            }
        }
        public static Rectangle ReslutionRect
        {
            get
            {
                return new Rectangle(0, 0, width, height);
            }
        }
        #endregion

        #region Time
        public static float ElapsedTimeThisFrameInMilliseconds
        {
            get
            {
                return elapseTimeThisFrameInMs;
            }
        }
        public static float TotalTime
        {
            get
            {
                return totalTimeMs / 1000.0f;
            }
        }
        public static float TotalTimeMilliseconds
        {
            get
            {
                return totalTimeMs;
            }
        }
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// BaseGame
        /// </summary>
        /// <param name="titleName">游戏窗口名称</param>
        public BaseGame(String titleName)
        {
            Window.Title = titleName;
            graphics = new GraphicsDeviceManager(this);
            contentMgr = new ContentManager(Services);
        }
        #endregion

        #region Initialize
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ContentMgr.RootDirectory = "Content";
            device = graphics.GraphicsDevice;

            width = graphics.GraphicsDevice.Viewport.Width;
            height = graphics.GraphicsDevice.Viewport.Height;

            aspectRatio = (float)width / (float)height;

            // 使用变长时间度量
            this.IsFixedTimeStep = false;

            // 关闭垂直同步
            this.graphics.SynchronizeWithVerticalRetrace = false;

            base.Initialize();
        }
        #endregion

        #region LoadContent
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            // TODO: use this.Content to load your game content here
        }
        #endregion

        #region UnloadContent
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion

        #region Update
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            // 更新游戏时间
            elapseTimeThisFrameInMs = (float)gameTime.ElapsedRealTime.Milliseconds;
            totalTimeMs += elapseTimeThisFrameInMs;

            // 更新设备输入
            InputKeyboards.Update();
            InputMouse.Update();

            base.Update(gameTime);
        }
        #endregion

        #region Draw
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        #endregion
    }
}
