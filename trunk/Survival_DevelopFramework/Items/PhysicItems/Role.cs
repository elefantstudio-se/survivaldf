using System;
using System.Collections.Generic;
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
using Survival_DevelopFramework.Factory;

namespace Survival_DevelopFramework.Items
{
    class Role : PhysicsItem
    {
        #region Variables
        /// <summary>
        /// 健康状况
        /// </summary>
        public int health;

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
            Dead,
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
        /// 跑动力量
        /// </summary>
        private float runForce;
        /// <summary>
        /// 跳跃力量
        /// </summary>
        private float jumpForce;

        /// <summary>
        /// 最大限制跑动速度
        /// </summary>
        private float MaxRunSpeed;

        /// <summary>
        /// 自发水平力量
        /// </summary>
        private float InnerXForce;

        /// <summary>
        /// 道具目录
        /// </summary>
        private Dictionary<String, ItemBase> itemDic = new Dictionary<string,ItemBase>();

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
        public Role(String texturePath,Vector2 size,float mass, int frameWidth, int frameHeight, int frameNumber,float runForce, float jumpForce, float maxRunSpeed)
            : base(texturePath,size,mass, frameWidth, frameHeight, frameNumber)
        {
            this.runForce = runForce;
            this.jumpForce = jumpForce;
            this.MaxRunSpeed = maxRunSpeed;
        }
        /// <summary>
        /// 使用RoleData序列化类进行构造
        /// </summary>
        /// <param name="roleData"></param>
        public Role(RoleData roleData)
        {
            // ItemBase
            texture = LoadHelper.LoadTexture2D(roleData.textureName);
            layer = roleData.layer;

            // AnimItem
            frameWidth = roleData.frameWidth;
            frameHeight = roleData.frameHeight;
            frameNumber = roleData.frameNumber;
            animSeqList = roleData.animSeqList;

            // PhysicsItem
            Body body = BodyFactory.Instance.CreateRectangleBody(PhysicsSys.Instance.PhysicsSimulator, roleData.size.X, roleData.size.Y, roleData.mass);
            body.Position = roleData.position;
            Geom geom = GeomFactory.Instance.CreateRectangleGeom(PhysicsSys.Instance.PhysicsSimulator, body, roleData.size.X, roleData.size.Y);

            // Role
            health = roleData.health;
            roleState = roleData.roleState;
            faceRight = roleData.facingRight;
            runForce = roleData.runForce;
            jumpForce = roleData.jumpForce;
            MaxRunSpeed = roleData.maxRunSpeed;
            foreach (KeyValuePair<String, String> itemPair in roleData.itemDic)
            {
                itemDic.Add(itemPair.Value, ItemFactory.CreateItem(itemPair.Key));
            }
        }
        #endregion

        #region Update
        public override void Update()
        {
            // 更新动画
            base.Update();

            // 速度限制 -- 有更方便的限制取值范围方法
            float hSpeed = body.LinearVelocity.X;
            if (hSpeed > MaxRunSpeed)
            {
                body.LinearVelocity.X = MaxRunSpeed;
            }
            else if (hSpeed < -MaxRunSpeed)
            {
                body.LinearVelocity.X = -MaxRunSpeed;
            }

            switch (roleState)
            {
                case RState.Free:
                    #region FSM
                    // 检查脸的朝向
                    if (body.LinearVelocity.X > 0.01)
                    {
                        faceRight = true;
                    }
                    else if(body.LinearVelocity.X < -0.01)
                    {
                        faceRight = false;
                    }
                    // 按方向键，进行左右移动
                    if (InputKeyboards.isKeyPress(Keys.Right))
                    {
                        body.ClearForce();
                        this.InnerXForce = runForce;
                        body.ApplyForce(new Vector2(runForce, 0) * BaseGame.ElapsedTimeThisFrameInMilliseconds);
                    }
                    else if (InputKeyboards.isKeyPress(Keys.Left))
                    {
                        body.ClearForce();
                        this.InnerXForce = -runForce;
                        body.ApplyForce(new Vector2(-runForce, 0) * BaseGame.ElapsedTimeThisFrameInMilliseconds);
                    }
                    // 按向上键，跳跃
                    else if (InputKeyboards.isKeyPress(Keys.Up))
                    {
                        roleState = RState.Jumping;
                        PlaySeq("Jumping");
                        body.ClearForce();
                        body.ApplyForce(new Vector2(0, -jumpForce) * BaseGame.ElapsedTimeThisFrameInMilliseconds);
                    }
                    // 按空格键，射击
                    else if (InputKeyboards.isKeyPress(Keys.Space))
                    {
                        roleState = RState.UsingGun;
                        PlaySeq("UsingGun");
                    }
                    // 如果body有速度并且处于地面上，则转换状态为Running
                    if (body.LinearVelocity.Length() > 0.01f && isOnGround())
                    {
                        roleState = RState.Running;
                        PlaySeq("Running");
                    }

                    // 如果非OnGround，则获取和地面之间的关系，并转换为对应的Jumping
                    if (!isOnGround())
                    {
                        roleState = RState.Jumping;
                    }

                    // 如果在地面上、和物体接触，并且合力指向物体，则转换为Pushing
                    if (closeToPushItem())//...
                    {
                       // roleState = RState.Pushing;
                    }

                    #endregion
                    break;
                case RState.Running:
                    // 检查脸的朝向
                    if (body.LinearVelocity.X > 0.01)
                    {
                        faceRight = true;
                    }
                    else if (body.LinearVelocity.X < -0.01)
                    {
                        faceRight = false;
                    }
                    // 按方向键，进行左右移动
                    if (InputKeyboards.isKeyPress(Keys.Right))
                    {
                        body.ClearForce();
                        this.InnerXForce = runForce;
                        body.ApplyForce(new Vector2(runForce, 0) * BaseGame.ElapsedTimeThisFrameInMilliseconds);
                    }
                    else if (InputKeyboards.isKeyPress(Keys.Left))
                    {
                        body.ClearForce();
                        this.InnerXForce = -runForce;
                        body.ApplyForce(new Vector2(-runForce, 0) * BaseGame.ElapsedTimeThisFrameInMilliseconds);
                    }
                    // 按向上键，跳跃
                    else if (InputKeyboards.isKeyPress(Keys.Up))
                    {
                        roleState = RState.Jumping;
                        PlaySeq("Jumping");
                        body.ClearForce();
                        body.ApplyForce(new Vector2(0, -jumpForce) * BaseGame.ElapsedTimeThisFrameInMilliseconds);
                    }
                    // 如果body无速度并且处于地面上，则转换状态为Free
                    if ((body.LinearVelocity.Length() <= 0.01f) && isOnGround())
                    {
                        roleState = RState.Free;
                        PlaySeq("Free");
                    }

                    // 如果合力不同于内部力+摩擦，则转换状态为Pushing
                    // 临时对摩擦使用硬编码
                    if ((Math.Abs(InnerXForce) - body.Mass * 980 * 1.0 - Math.Abs(body.Force.X)) <= 0.001f)
                    {
                       // roleState = RState.Pushing;
                       // PlaySeq("UsingGun");
                    }

                    // 如果body有速度并且不处于地面上，则转换状态为Jumping

                    // 如果UsingGun开关打开，则转换为UsingGun

                    // 如果UsingItem开关打开,则转换为UsingItem

                    // 如果UsingHook开关打开,则转换为UsingHook

                    // 如果GettingItem开关打开，则转换为GettingItem

                    if (InputKeyboards.isKeyPress(Keys.Up))
                    {
                        roleState = RState.Jumping;
                        PlaySeq("Jumping");
                        body.ClearForce();
                        body.ApplyForce(new Vector2(0, -jumpForce) * BaseGame.ElapsedTimeThisFrameInMilliseconds);
                    }
                    if (InputKeyboards.isKeyPress(Keys.Space))
                    {
                        // ... 射击
                    }
                    break;
                case RState.Jumping:
                    // 如果body有速度并且处于地面上，则转换状态为Running

                    // 如果body无速度，则转换状态为Free
                    if (body.LinearVelocity.Length() <= 0.01f)
                    {
                        roleState = RState.Free;
                        PlaySeq("Free");
                    }

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
                    if ((Math.Abs(InnerXForce) - body.Mass * 980 * 1.0f) <= 0.001f)
                    {
                        roleState = RState.Free;
                        PlaySeq("Free");
                    }
                    // 如果GettingItem开关打开，则转换为GettingItem
                    break;

                case RState.GettingItem:
                    #region FSM
                    if (currentSeq.name != "GettingItem")
                    {
                        roleState = RState.Free;
                        PlaySeq("Free");
                    }
                    #endregion

                    break;
                case RState.UsingHook:
                    if (currentSeq.name != "UsingHook")
                    {
                        roleState = RState.Free;
                        PlaySeq("Free");
                    }
                    break;

                case RState.UsingItem:
                    if (currentSeq.name != "UsingItem")
                    {
                        roleState = RState.Free;
                        PlaySeq("Free");
                    }
                    break;

                case RState.UsingGun:
                    if (currentSeq.name != "UsingGun")
                    {
                        roleState = RState.Free;
                        PlaySeq("Free");
                    }
                    break;

                case RState.UsingGunInAir:
                    if (currentSeq.name != "UsingGunInAir")
                    {
                        roleState = RState.Free;
                        PlaySeq("Free");
                    }
                    break;
            }
        }
        #endregion

        #region 外部驱动
        #endregion

        #region Draw
        public override void Draw()
        {
            if (Visible)
            {
                // 如果启用动画则绘制帧
                // 否则使用基类的绘制方式
                if (animSeqList.Count != 0)
                {
                    // 计算帧对应的矩形区域
                    Rectangle pixelRect;
                    int posX, poxY;
                    int rowId, columnId;
                    rowId = currentFrameId / ColumnCount;
                    columnId = currentFrameId % ColumnCount;
                    posX = columnId * frameWidth;
                    poxY = rowId * frameHeight;
                    pixelRect = new Rectangle(posX, poxY, frameWidth, frameHeight);
                    if (faceRight)
                    {
                        Painter.DrawT(texture, pixelRect, origin, Position, scale, Rotation, SpriteEffects.FlipHorizontally);
                    }
                    else
                    {
                        Painter.DrawT(texture, pixelRect, origin, Position, scale, Rotation);
                    }
                }
                DrawBound();
                DrawBody();
            }
        }
        #endregion

        #region Control From Outside
        public void GetItem(String itemName)
        {
            // 获取道具...
        }
        public void UseItem(String itemName)
        {
            // 使用道具...
        }
        #endregion

        #region Check Self
        public bool HasItem(ItemBase item)
        {
            // 拥有物品吗..
            return false;
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
                    Vector2 itemSize = Vector2.Zero;

                    itemSize = new Vector2(800,100);
                    Terrain rectG = new Terrain("GameandMe", itemSize, 1);
                    rectG.Position = new Vector2(400, 500);
                    rectG.origin = new Vector2(100,100);
                    SceneMgr.Instance.AddItem(rectG);

                    itemSize = new Vector2(200, 10);
                    rectG = new Terrain("GameandMe",itemSize,1);
                    rectG.Position = new Vector2(400, 430);
                    rectG.Rotation = 0.1f;
                    rectG.origin = new Vector2(100, 100);
                    SceneMgr.Instance.AddItem(rectG);

                    itemSize = new Vector2(100,600);
                    rectG = new Terrain("GameandMe",itemSize,1);
                    rectG.Position = new Vector2(50, 600);
                    rectG.origin = new Vector2(50, 300);
                    SceneMgr.Instance.AddItem(rectG);

                    itemSize = new Vector2(100, 600);
                    rectG = new Terrain("GameandMe",itemSize,1);
                    rectG.Position = new Vector2(750, 600);
                    rectG.origin = new Vector2(50, 300);
                    SceneMgr.Instance.AddItem(rectG);

                    itemSize = new Vector2(32, 48);
                    float runforce = 1000.0f;
                    float jumpforce = 3000.0f;
                    float maxRunSpeed = 50;
                    role = new Role("Roles/soldier", itemSize, 10, 32, 48, 17, runforce, jumpforce, maxRunSpeed);
                    role.Position = new Vector2(400, 100);
                    role.origin = new Vector2(16, 24);
                    role.animSeqList.Add(new AnimSequence("Free", 40, 6, 8, true, true));
                    role.animSeqList.Add(new AnimSequence("Running", 0, 8, 8, true, true));
                    role.animSeqList.Add(new AnimSequence("Jumping", 8, 9, 4, true, false));
                    role.animSeqList.Add(new AnimSequence("UsingItem", 17, 8, 7, true, false));
                    role.animSeqList.Add(new AnimSequence("UsingGun", 24, 13, 12, true, false));
                    role.animSeqList.Add(new AnimSequence("UsingHook", 37, 1, 8, true, false));
                    role.PlaySeq("Free");
                    SceneMgr.Instance.AddItem(role);
                },
                delegate
                {
                },
                delegate
                {
                    Painter.DrawBegin();
                    Painter.DrawLine("人物状态"+role.RoleState.ToString(),Vector2.Zero);
                    Painter.DrawEnd();
                }
                );
        }
        #endregion
    }
}
