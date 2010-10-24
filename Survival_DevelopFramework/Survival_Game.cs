using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Survival_DevelopFramework.GameManager;

namespace Survival_DevelopFramework
{
    public class Survival_Game : BaseGame
    {
        #region Constructor
        public Survival_Game(string titleName):base(titleName)
        {
        }
        #endregion

        #region Initialize
        protected override void Initialize()
        {
            base.Initialize();
        }
        #endregion

        #region LoadContent
        protected override void LoadContent()
        {
            base.LoadContent();
        }
        #endregion

        #region UnLoadContent
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        #endregion

        #region Update
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GameMgr.Instance.Update();
        }
        #endregion

        #region Draw
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GameMgr.Instance.Draw();
        }
        #endregion
    }
}
