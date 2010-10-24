using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Controllers;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics;

namespace Survival_DevelopFramework.PhysicsSystem
{
    class PhysicsSys
    {
        #region Variables
        //物理模拟系统
        private PhysicsSimulator mPhysicsSimulator;
        public PhysicsSimulator PhysicsSimulator
        {
            get { return mPhysicsSimulator; }
        }
        //重力
        private Vector2 Gvec;
        #endregion

        #region 单件
        private static PhysicsSys instance = null;
        /// <summary>
        /// 单件
        /// </summary>
        public static PhysicsSys Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PhysicsSys();
                }
                return instance;
            }
        }
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private PhysicsSys()
        {
            Gvec = new Vector2(0, 1.0f);
            mPhysicsSimulator = new PhysicsSimulator(Gvec);
        }
        #endregion

        #region Update
        public void Update()
        {
            mPhysicsSimulator.Update(BaseGame.ElapsedTimeThisFrameInMilliseconds * 0.001f);
        }
        #endregion
    }
}
