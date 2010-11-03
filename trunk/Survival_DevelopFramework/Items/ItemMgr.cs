using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survival_DevelopFramework.GameManager;
using Survival_DevelopFramework.SceneManager;
using Survival_DevelopFramework.Helpers;
using Microsoft.Xna.Framework;
using Survival_DevelopFramework.InputSystem;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Survival_DevelopFramework.GraphicSystem;
using Microsoft.Xna.Framework.Graphics;

namespace Survival_DevelopFramework.Items
{
    /// <summary>
    /// Item 管理器类
    /// 对Item列表进行常规处理
    /// </summary>
    class ItemMgr
    {
        #region Variables
        protected List<ItemBase> itemList = new List<ItemBase>();
        #endregion

        #region Properties
        public int MaxLayer
        {
            get
            {
                return itemList.Count() - 1;
            }
        }
        #endregion

        #region Instance
        private ItemMgr instance;
        public ItemMgr Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    instance = new ItemMgr();
                    return instance;
                }
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ItemMgr()
        {
            // 进行初始化处理...

        }
        #endregion

        #region Update
        virtual public void Update()
        {
            foreach (ItemBase item in itemList)
            {
                item.Update();
            }
        }
        #endregion

        #region Draw
        virtual public void Draw()
        {
            foreach(ItemBase item in itemList)
            {
                item.Draw();
            }
        }
        #endregion

        #region Layer Sort
        public void MoveToUpperLayer(int layer)
        {
            if (layer == MaxLayer)
            {
                Console.WriteLine("该Item已经处于顶层!");
                return;
            }

            // 寻找最近一个覆盖它的Item
            ItemBase curItem = itemList[layer];

            ItemBase upperItem = null;
            for (int i = layer + 1; i <= MaxLayer; i++)
            {
                ItemBase compItem = itemList[i];
                if (curItem.GetOrthoBound().Intersects(compItem.GetOrthoBound()))
                {
                    upperItem = compItem;
                    break;
                }
            }

            if(upperItem == null)
            {
                Console.WriteLine("该Item未被任Item覆盖");
                return;
            }

            // 移动
            MoveTo(layer,upperItem.layer);
        }
        public void MoveToLowerLayer(int layer)
        {
            if (layer == 0)
            {
                Console.WriteLine("该Item已经处于底层!");
                return;
            }

            // 寻找最近一个被覆盖的Item
            ItemBase curItem = itemList[layer];

            ItemBase lowerItem = null;
            for (int i = layer - 1; i >= 0; i--)
            {
                ItemBase compItem = itemList[i];
                if (curItem.GetOrthoBound().Intersects(compItem.GetOrthoBound()))
                {
                    lowerItem = compItem;
                    break;
                }
            }

            if(lowerItem == null)
            {
                Console.WriteLine("该Item未覆盖任何其他Item");
                return;
            }

            // 移动
            MoveTo(layer,lowerItem.layer);
        }
        public void MoveTo(int srcLayer,int destLayer)
        {
            if (srcLayer > MaxLayer || destLayer > MaxLayer)
            {
                Console.WriteLine("指定层级不存在于itemList当中！");
                return;
            }

            // 层级和Id是对应的 layer0 -- id0 
            int srcId = srcLayer;
            int destId = destLayer;
            // sort
            ItemBase srcItem = itemList[srcId];
            srcItem.layer = destLayer;
            if (srcLayer < destLayer)
            {
                for (int i = srcId + 1; i <= destId; i++)
                {
                    itemList[i].layer--;
                    itemList[i - 1] = itemList[i];
                }
            }
            else if (srcLayer > destLayer)
            {
                for (int i = srcId-1; i >= destId; i--)
                {
                    itemList[i].layer++;
                    itemList[i+1] = itemList[i];
                }
            }
            itemList[destId] = srcItem;
        }
        public void MoveToTop(int layer)
        {
            if (layer >MaxLayer)
            {
                Console.WriteLine("指定层级不存在于itemList当中！");
                return;
            }

            ItemBase srcItem = itemList[layer];
            srcItem.layer = MaxLayer;
            for (int i = layer + 1; i <= itemList.Count() - 1; i++)
            {
                itemList[i].layer--;
                itemList[i - 1] = itemList[i];
            }
            itemList[MaxLayer] = srcItem;
        }
        public void MoveToBottom(int layer)
        {
            if (layer > MaxLayer)
            {
                Console.WriteLine("指定层级不存在于itemList当中！");
                return;
            }
            
            ItemBase srcItem = itemList[layer];
            srcItem.layer = 0;
            for (int i = layer - 1; i >= 0; i--)
            {
                itemList[i].layer++;
                itemList[i + 1] = itemList[i];
            }
            itemList[0] = srcItem;
        }
        #endregion

        #region Item Control
        /// <summary>
        /// 添加Item
        /// </summary>
        /// <param name="item"></param>
        virtual public void AddItem(ItemBase item)
        {
            item.layer = MaxLayer + 1;
            itemList.Add(item);
        }

        virtual public void ClearItemList()
        {
            itemList.Clear();
        }

        // 需要补充排序方法等等...
        #endregion

        #region UnitTest
        public static void UnitTest()
        {
            ItemMgr boxMgr = null;
            ItemBase selItem = null;

            TestGame.StartTest("层级管理： 单击-选择 Up-上一级 Down-下一级 Left-底层 Right-顶层", null,
                delegate
                {
                    SceneMgr.Instance.backgroundMgr = new ItemMgr();
                    boxMgr = SceneMgr.Instance.backgroundMgr;
                    // 随机添加Box (ItemBase)
                    Random r = new Random(DateTime.Now.Millisecond);
                    Random r2 = new Random(DateTime.Now.Millisecond);
                    for (int i = 0; i < 10; i++)
                    {
                        ItemBase box = new ItemBase("GameandMe");
                        Vector2 boxPos = new Vector2();
                        boxPos.X = (int)(BaseGame.Width * r.NextDouble());
                        boxPos.Y = (int)(BaseGame.Height * r.NextDouble());
                        box.Position = boxPos;
                        box.Rotation = (float)(r2.NextDouble() * Math.PI *2);
                        // 添加到列表
                        boxMgr.AddItem(box);
                    }
                },
                delegate
                {
                    // 获取鼠标单击，确定选中的item
                    if (InputMouse.isLeftMouseClick())
                    {
                        selItem = null;
                        for (int i = boxMgr.MaxLayer; i >= 0; i--)
                        {
                            ItemBase item = boxMgr.itemList[i];
                            if (InputMouse.GetMouseClickRect().Intersects(item.GetOrthoBound()))
                            {
                                selItem = boxMgr.itemList[i];
                                Console.WriteLine("Select Item on Layer: "+i);
                                break;
                            }
                        }
                        if (selItem == null)
                        {
                            Console.WriteLine("--- Nothing Selected in this click ---");
                        }
                    }
                    // 获取键盘按键，调整层级
                    if (selItem != null)
                    {
                        // 向上箭头 -- 上移一级
                        if (InputKeyboards.isKeyJustPress(Keys.Up))
                        {
                            boxMgr.MoveToUpperLayer(selItem.layer);
                        }
                        // 向下箭头 -- 下移一级
                        if (InputKeyboards.isKeyJustPress(Keys.Down))
                        {
                            boxMgr.MoveToLowerLayer(selItem.layer);
                        }
                        // pageup   -- 移到顶部
                        if (InputKeyboards.isKeyJustPress(Keys.Right))
                        {
                            boxMgr.MoveToTop(selItem.layer);
                        }
                        // pagedown -- 移到底部
                        if (InputKeyboards.isKeyJustPress(Keys.Left))
                        {
                            boxMgr.MoveToBottom(selItem.layer);
                        }
                    }
                },
                delegate
                {
                }
                );
        }
        #endregion
    }
}
