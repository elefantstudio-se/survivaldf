using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using Survival_DevelopFramework.InputSystem;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Survival_DevelopFramework.PhysicsSystem;
using FarseerGames.FarseerPhysics.Factories;
using Survival_DevelopFramework.GraphicSystem;
using Survival_DevelopFramework.GameManager;
using Microsoft.Xna.Framework.Graphics;
using Survival_DevelopFramework.Helpers;
using Survival_DevelopFramework.SceneManager;
using Survival_DevelopFramework.Items.PhysicItems;

namespace Survival_DevelopFramework.Items
{
    class Role : PhysicItem
    {
        #region Variables

        #region RoleState Enum
        /// <summary>
        /// 标志角色的各种状态
        /// </summary>
        public enum RState
        {
            Free,
            Jumping,
            Running,
            Pushing,
            GettingItem,
            UsingHook,
            UsingGun,
            UsingItem,
            UsingGunInAir,
        }
        #endregion
        /// <summary>
        /// 角色状态
        /// </summary>
        private RState roleState = RState.Free;

        /// <summary>
        /// 面向右
        /// </summary>
        private bool faceRight;
        /// <summary>
        /// 在空中
        /// </summary>
        private bool inAir;

        /// <summary>
        /// 跑动力量
        /// </summary>
        private Vector2 runForce;
        /// <summary>
        /// 跳跃力量
        /// </summary>
        private Vector2 jumpForce;

        /// <summary>
        /// 状态、动画关联量
        /// </summary>
        private bool usingGun = false;
        private bool usingHook = false;
        private bool usingItem = false;
        #endregion

        #region Properties
        public RState RoleState
        {
            get
            {
                return roleState;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Role
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="frameWidth"></param>
        /// <param name="frameHeight"></param>
        /// <param name="frameNumber"></param>
        /// <param name="body"></param>
        /// <param name="geom"></param>
        /// <param name="runForce"></param>
        /// <param name="jumpForce"></param>
        public Role(Texture2D tex,int frameWidth,int frameHeight,int frameNumber,Body body,Geom geom,Vector2 runForce, Vector2 jumpForce):base(tex,frameWidth,frameHeight,frameNumber,body,geom)
        {
            this.runForce = runForce;
            this.jumpForce = jumpForce;
        }
        #endregion

        #region Update
        public override void Update()
        {
            base.Update();
            switch (roleState)
            {
                case RState.Free:
                    #region FSM
                    // 按方向键，进行左右移动
                    if (InputKeyboards.isKeyPress(Keys.Right))
                    {
                        if (!faceRight)
                        {
                            faceRight = true;
                            scale = -1;
                            rotation = 90;
                        }
                        body.ClearForce();
                        body.ApplyForce(runForce * BaseGame.ElapsedTimeThisFrameInMilliseconds);
                    }
                    else if (InputKeyboards.isKeyPress(Keys.Left))
                    {
                        if (faceRight)
                        {
                            faceRight = false;
                            scale = 1;
                            rotation = 0;
                        }
                        body.ClearForce();
                        body.ApplyForce(-runForce * BaseGame.ElapsedTimeThisFrameInMilliseconds);
                    }
                    // 按向上键，跳跃
                    else if (InputKeyboards.isKeyPress(Keys.Up))
                    {
                        roleState = RState.Jumping;
                        body.ClearForce();
                        body.ApplyForce(-jumpForce * BaseGame.ElapsedTimeThisFrameInMilliseconds);
                    }
                    // 按空格键，射击
                    else if (InputKeyboards.isKeyPress(Keys.Space))
                    {
                        roleState = RState.UsingGun;
                    }
                    // 如果body有速度并且处于地面上，则转换状态为Running
                    if(body.LinearVelocity != Vector2.Zero && isOnGround())
                    {
                        roleState = RState.Running;
                    }

                    // 如果非OnGround，则获取和地面之间的关系，并转换为对应的Jumping
                    if (!isOnGround())
                    {
                        roleState = RState.Jumping;
                    }

                    // 如果在地面上、和物体接触，并且合力指向物体，则转换为Pushing
                    if (closeToPushItem())//...
                    {
                        roleState = RState.Pushing;
                    }



                    #endregion
                    break;
                case RState.Running:
                    // 按方向键，进行左右移动
                    if (InputKeyboards.isKeyPress(Keys.Right))
                    {
                        if (!faceRight)
                        {
                            faceRight = true;
                            //scale = -1;
                            //rotate = 90;
                        }
                        body.ClearForce();
                        body.ApplyForce(runForce * BaseGame.ElapsedTimeThisFrameInMilliseconds);
                    }
                    else if (InputKeyboards.isKeyPress(Keys.Left))
                    {
                        if (faceRight)
                        {
                            faceRight = false;
                            //scale = 1;
                            //rotate = 0;
                        }
                        body.ClearForce();
                        body.ApplyForce(-runForce * BaseGame.ElapsedTimeThisFrameInMilliseconds);
                    }
                    // 按向上键，跳跃
                    else if (InputKeyboards.isKeyPress(Keys.Up))
                    {
                        roleState = RState.Jumping;
                        body.ClearForce();
                        body.ApplyForce(-jumpForce * BaseGame.ElapsedTimeThisFrameInMilliseconds);
                    }
                    // 如果body无速度并且处于地面上，则转换状态为Free

                    // 如果body有速度并且不处于地面上，则转换状态为Jumping

                    // 如果UsingGun开关打开，则转换为UsingGun

                    // 如果UsingItem开关打开,则转换为UsingItem

                    // 如果UsingHook开关打开,则转换为UsingHook

                    // 如果GettingItem开关打开，则转换为GettingItem

                    if (InputKeyboards.isKeyPress(Keys.Up))
                    {
                        roleState = RState.Jumping;
                        body.ClearForce();
                        body.ApplyForce(jumpForce * BaseGame.ElapsedTimeThisFrameInMilliseconds);
                    }
                    if (InputKeyboards.isKeyPress(Keys.Space))
                    {
                        // ... 射击
                    }
                    break;
                case RState.Jumping:
                    // 如果body有速度并且处于地面上，则转换状态为Running

                    // 如果body无速度并且处于地面上，则转换状态为Free

                    // 如果UsingGun开关打开，则转换为JumpingShooting

                    // 如果UsingHook开关打开,则转换为UsingHook

                    // 如果GettingItem开关打开，则转换为GettingItem

                    if (InputKeyboards.isKeyJustPress(Keys.Space))
                    {
                        // ... 跳跃射击
                    }
                    break;
                case RState.Pushing:
                    // 如果不和可推物体分离或者没有指向物体的合力，则转换为Free/Running

                    // 如果GettingItem开关打开，则转换为GettingItem
                    break;

                case RState.GettingItem:
                    #region FSM
                    if (currentSeq.name != "GettingItem")
                    {
                        roleState = RState.Free;
                    }
                    #endregion

                    break;
                case RState.UsingHook:
                    if (currentSeq.name != "UsingHook")
                    {
                        roleState = RState.Free;
                    }
                    break;

                case RState.UsingItem:
                    if (currentSeq.name != "UsingItem")
                    {
                        roleState = RState.Free;
                    }
                    break;

                case RState.UsingGun:
                    if (currentSeq.name != "UsingGun")
                    {
                        roleState = RState.Free;
                    }
                    break;

                case RState.UsingGunInAir:
                    if (currentSeq.name != "UsingGunInAir")
                    {
                        roleState = RState.Free;
                    }
                    break;
            }
        }
        #endregion

        #region Draw
        public override void Draw()
        {
            base.Draw();
        }
        #endregion

        #region Check Environment
        /// <summary>
        /// 在地面上
        /// </summary>
        /// <returns></returns>
        public bool isOnGround()
        {
            ///...
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool closeToPushItem()
        {
            return false;
        }
        #endregion

        #region UnitTest
        public new static void UnitTest()
        {
            Role role = null;
            TestGame.StartTest(
                "角色测试",
                null,
                delegate
                {
                    Body body = BodyFactory.Instance.CreateRectangleBody(PhysicsSys.Instance.PhysicsSimulator, 32, 64, 10);
                    body.Position = new Vector2(400, 100);
                    Geom geom = GeomFactory.Instance.CreateRectangleGeom(PhysicsSys.Instance.PhysicsSimulator, body, 32, 64);
                    Vector2 runforce = new Vector2(0.1f, 0);
                    Vector2 jumpforce = new Vector2(0.1f, 1);
                    role = new Role(LoadHelper.LoadTexture2D("soldier"), 32, 48, 17, body, geom, runforce, jumpforce);
                    role.animSeqList.Add(new AnimSequence("Free", 40, 6, 8, true, true));
                    role.animSeqList.Add(new AnimSequence("Running", 0, 8, 8, true, true));
                    role.animSeqList.Add(new AnimSequence("Jumping", 8, 9, 8, true, false));
                    role.animSeqList.Add(new AnimSequence("UsingItem", 17, 8, 7, true, false));
                    role.animSeqList.Add(new AnimSequence("UsingGun", 24, 13, 12, true, false));
                    role.animSeqList.Add(new AnimSequence("UsingHook", 37, 1, 8, true, false));
                    role.PlaySeq("Free");
                    SceneMgr.Instance.AddItem(role);
                },
                delegate
                {
                    GameMgr.Instance.Update(); // 更新物理
                },
                delegate
                {
                    GameMgr.Instance.Draw();
                }
                );
        }
        #endregion
    }
}
