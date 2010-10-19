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
        #region 单件
        private static PhysicsSys instance = null;
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
        #endregion

        #region 物理变量
        //物理模拟系统
        private PhysicsSimulator mPhysicsSimulator;
        public PhysicsSimulator PhysicsSimulator
        {
            get { return mPhysicsSimulator; }
        }
       //重力
        private Vector2 Gvec;
        #endregion

        #region 初始化
        public void InitPhysics()
        {
            Gvec = new Vector2(0,100.0f);
            mPhysicsSimulator = new PhysicsSimulator(Gvec);
        }
        #endregion

        #region 物理方法组
        public void UpdatePhysics(float dt)
        {
            mPhysicsSimulator.Update(dt);
        }
        #endregion
    }
}
