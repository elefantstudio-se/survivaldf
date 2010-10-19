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
    class anim : ItemBase
    {
        public anim()
        {
            InitSelf();
            ContentLoad();
            
        }
        private Texture2D texture;
        private int X;
        private int Y;
        private Rectangle Frect;
        private float nowF;
        private int allF;
        private int rowF;

        public void DrawSelf()
        {
            Painter.Instance.PlayA(texture, new Vector2(X, Y), 0.0f, 1.0f, Frect, allF, rowF, (int)nowF);
        }
        public void UpdateSelf()
        {
            nowF += 0.3f;
            if (nowF > 6) nowF -= 6;
        }
        public void ContentLoad()
        {
            texture = LoadHelper.Content.Load<Texture2D>("zhuzhen");
        }
        public void InitSelf()
        { 
            X = 700;
            Y = 200;
            nowF = 0;
            allF = 6;
            rowF=3;
            Frect = new Rectangle(0, 0, 174, 125);
        }
    }
}
