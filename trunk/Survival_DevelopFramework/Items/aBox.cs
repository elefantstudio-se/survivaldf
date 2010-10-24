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
        public aBox()
        {
        }
        private Texture2D texture;
        private Body body;
        private Geom geom;
        private int X;
        private int Y;

        void ItemBase.DrawSelf()
        {
            Painter.Instance.DrawT(texture, body.Position, body.Rotation, 0.5f);
        }
        void ItemBase.UpdateSelf()
        {

        }
        void ItemBase.ContentLoad()
        {
            texture = LoadHelper.Content.Load<Texture2D>("GameandMe");
        }
        void ItemBase.InitSelf()
        { 
            X = 200;
            Y = 100;
            body = BodyFactory.Instance.CreateRectangleBody(PhysicsSys.Instance.PhysicsSimulator,100.0f, 100.0f,100.0f);
            body.Position = new Vector2(X, Y);
            geom = GeomFactory.Instance.CreateRectangleGeom(PhysicsSys.Instance.PhysicsSimulator, body, 100, 100);
        }
    }
}
