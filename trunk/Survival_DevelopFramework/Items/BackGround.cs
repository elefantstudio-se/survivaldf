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
using Survival_DevelopFramework.ItemDatas;

namespace Survival_DevelopFramework.Items
{
    /// <summary>
    /// 静态背景
    /// </summary>
    class Background : ItemBase
    {
        public Background(String texturePath)
            : base(texturePath)
        {
        }
        public Background(BackgroundData backgroundData)
        {
            // ItemBase
            texture = LoadHelper.LoadTexture2D("Backgrounds/"+backgroundData.textureName);
            layer = backgroundData.layer;

            // Background
        }

        public override void Draw()
        {
            Rectangle destRect = new Rectangle(0,0,BaseGame.Width,BaseGame.Height);
            Painter.DrawT(texture, destRect);
        }
        public override void Update()
        {

        }
    }
}
