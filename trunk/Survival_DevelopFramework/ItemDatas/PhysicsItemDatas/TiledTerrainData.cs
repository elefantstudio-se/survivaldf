using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Survival_DevelopFramework.Items.PhysicItems
{
    [Serializable]
    public class TiledTerrainData
    {
        // ItemBase
        public String textureName;
        public int layer;

        // AnimItem

        // PhysicsItem
        public Vector2 position;
        public Vector2 size;
        public float rotation;

        // Terrain
        public float frictionCoefficient;

        // TiledTerrain
        public Vector2 tiledSize;
        public Vector2 tiledOrigin;
        public TiledTerrain.TiledType tiledType;

    }
}
