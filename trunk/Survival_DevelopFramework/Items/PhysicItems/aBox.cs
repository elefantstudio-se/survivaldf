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
    class aBox : ItemBase
    {
        public aBox(Texture2D texture):base(texture)
        {
            Y = 100;
            body = BodyFactory.Instance.CreateRectangleBody(PhysicsSys.Instance.PhysicsSimulator, 100.0f, 100.0f, 100.0f);
            body.Position = new Vector2(X, Y);
            geom = GeomFactory.Instance.CreateRectangleGeom(PhysicsSys.Instance.PhysicsSimulator, body, 100, 100);
        }
        private Body body;
        private Geom geom;
        public int X;
        public int Y;

        public override void Draw()
        {
            Painter.DrawT(texture, body.Position, body.Rotation, 0.5f);
        }
        public override void Update()
        {

        }
    }
}
