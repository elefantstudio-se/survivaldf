using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Survival_DevelopFramework
{
    public partial class TestGame : Survival_Game
    {
        #region Variables
        /// <summary>
        /// 单元测试代理
        /// </summary>
        public delegate void TestHandler();

        protected TestHandler initCode, loadCode, updateCode, drawCode;
        #endregion

        #region Constructor
        protected TestGame(string titleName, TestHandler initCode, TestHandler loadCode, TestHandler updateCode, TestHandler drawCode)
            : base(titleName)
        {
            this.initCode = initCode;
            this.loadCode = loadCode;
            this.updateCode = updateCode;
            this.drawCode = drawCode;
        }
        #endregion

        #region Initialize
        protected override void Initialize()
        {
            base.Initialize();

           if (initCode != null)
                initCode();
        }
        #endregion

        #region LoadContent
        protected override void LoadContent()
        {
            base.LoadContent();

          if (loadCode != null)
                loadCode();
        }
        #endregion

        #region Update
        protected override void Update(GameTime gametime)
        {
            base.Update(gametime);

            if (updateCode != null)
                updateCode();
        }
        #endregion

        #region Draw
        protected override void Draw(GameTime gametime)
        {
            base.Draw(gametime);

            if (drawCode != null)
                drawCode();
        }
        #endregion

        #region Start Test
        public static void StartTest(string testName, TestHandler initCode, TestHandler loadCode, TestHandler updateCode, TestHandler drawCode)
        {
            using (TestGame game = new TestGame(testName,initCode,loadCode,updateCode,drawCode))
            {
                game.Run();
            }
        }
        #endregion
    }
}
