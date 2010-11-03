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
        public cBox(Texture2D texture):base(texture)
        {
            X = 300;
            Y = 300;
        }
        private Texture2D texture;
        private int X;
        private int Y;

        public override void Draw()
        {
            Painter.DrawT(texture, new Vector2 (X,Y), 0, 0.3f);
        }
        public override void Update()
        {
            if (InputKeyboards.isKeyPress(Keys.Left)) X--;
            if (InputKeyboards.isKeyPress(Keys.Right )) X++;
            if (InputKeyboards.isKeyPress(Keys.Up )) Y--;
            if (InputKeyboards.isKeyPress(Keys.Down)) Y++;
        }
    }
}
