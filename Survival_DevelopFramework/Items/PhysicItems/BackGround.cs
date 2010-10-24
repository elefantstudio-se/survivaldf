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
using Survival_DevelopFramework.SceneManager;

namespace Survival_DevelopFramework.Items
{
    /// <summary>
    /// 静态背景
    /// </summary>
    class BG : ItemBase
    {
        public BG(Texture2D texture):base(texture)
        {
            position.X = BaseGame.Width / 2;
            position.Y = BaseGame.Height / 2;
        }

        public override void Draw()
        {
            Painter.DrawT(texture, Position,0, 1.0f);
        }
        public override void Update()
        {

        }
    }
}
