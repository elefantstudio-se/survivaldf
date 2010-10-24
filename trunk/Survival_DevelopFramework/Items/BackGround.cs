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
    class BG : ItemBase
    {
        public BG()
        {
        }
        private Texture2D texture;
        private int X;
        private int Y;

        public void DrawSelf()
        {
            Painter.Instance.DrawT(texture, new Vector2(X,Y),0, 1.0f);
        }
        public void UpdateSelf()
        {

        }
        public void ContentLoad()
        {
            texture = LoadHelper.Content.Load<Texture2D>("bg");
        }
        public void InitSelf()
        { 
            X =Painter.Instance.GraphicsDevice.Viewport.Width /2;
            Y = Painter.Instance.GraphicsDevice.Viewport.Height /2;
        }
    }
}
