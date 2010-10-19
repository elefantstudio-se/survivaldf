using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Survival_DevelopFramework.GraphicSystem;
using Survival_DevelopFramework.InputSystem;
using Survival_DevelopFramework.Helpers;
using Survival_DevelopFramework.PhysicsSystem;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Controllers;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;

namespace Survival_DevelopFramework.Items
{
    class cBox : ItemBase
    {
        public cBox()
        {
            InitSelf();
            ContentLoad();
            
        }
        private Texture2D texture;
        private int X;
        private int Y;

        public void DrawSelf()
        {
            Painter.Instance.DrawT(texture, new Vector2 (X,Y), 0, 0.3f);
        }
        public void UpdateSelf()
        {
            if (InputKeyboards.Instance.isKeyPress(Keys.Left)) X--;
            if (InputKeyboards.Instance.isKeyPress(Keys.Right )) X++;
            if (InputKeyboards.Instance.isKeyPress(Keys.Up )) Y--;
            if (InputKeyboards.Instance.isKeyPress(Keys.Down)) Y++;
        }
        public void ContentLoad()
        {
            texture = LoadHelper.Content.Load<Texture2D>("GameandMe");
        }
        public void InitSelf()
        { 
            X = 300;
            Y = 300;
        }
    }
}
