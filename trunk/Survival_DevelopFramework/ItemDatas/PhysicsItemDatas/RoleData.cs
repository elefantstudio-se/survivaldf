using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Survival_DevelopFramework.Items.PhysicItems
{
    [Serializable]
    class RoleData
    {
        // Role
        public int health = 3;
        public Role.RState roleState = Role.RState.Free;
        public bool facingRight = false;
        public float runForce = 100;
        public float jumpForce = 100;
        public float maxRunSpeed = 100;
        /// <summary>
        /// 道具描述字典
        /// TKey   -- 道具的类名
        /// TValue -- 道具的命名 (例如：有2个链条，用以区分)
        /// </summary>
        public Dictionary<String, String> itemDic = new Dictionary<string, string>();

        // PhysicsItem
        public Vector2 position = Vector2.Zero;
        public Vector2 size = Vector2.Zero;
        public float mass = 0;

        // AnimItem
        public int frameWidth = 0;
        public int frameHeight = 0;
        public int frameNumber = 0;
        public List<AnimItem.AnimSequence> animSeqList = new List<AnimItem.AnimSequence>();

        // ItemBase
        public String textureName = "GameandMe";
        public int layer = 0;
    }
}
