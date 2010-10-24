using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survival_DevelopFramework.GraphicSystem;
using Microsoft.Xna.Framework.Graphics;
using Survival_DevelopFramework.Helpers;
using Microsoft.Xna.Framework;
using Survival_DevelopFramework.GameManager;

namespace Survival_DevelopFramework.Items.DramaManager
{
    class DramaMgr:ItemBase
    {
        #region Constants
        /// <summary>
        /// 需要从图片中切割矩形框
        /// </summary>
        public readonly static Rectangle
            DialogLeftGfxRect = new Rectangle(0, 0, 59, 168),
            DialogMiddleGfxRect = new Rectangle(59, 0, 53, 168),
            DialogRightGfxRect = new Rectangle(112, 0, 141, 168),
            NextButtonGfxRect = new Rectangle(226, 169, 17, 17);
        #endregion

        #region Variables

        /// <summary>
        /// 剧本界面图片
        /// </summary>
        public Texture2D dramaUI;

        /// <summary>
        /// 剧本
        /// </summary>
        public Drama drama; 

        /// <summary>
        /// 当前讲话内容
        /// </summary>
        private String dialogStr;

        /// <summary>
        /// 在讲话的角色Id
        /// </summary>
        private int speakingRole;

        /// <summary>
        /// 图片显示
        /// </summary>
        public Texture2D showPicture;
        /// <summary>
        /// 左侧人物
        /// </summary>
        public Texture2D roleLeft;
        /// <summary>
        /// 中间人物
        /// </summary>
        public Texture2D roleMiddle;
        /// <summary>
        /// 右侧人物
        /// </summary>
        public Texture2D roleRight;
		#endregion
		
		#region Constructor
        public DramaMgr()
		{
		}
		#endregion

        #region Dispose
        public void Dispose()
        {
            if (showPicture != null)
            {
                showPicture.Dispose();
            }
            if (roleLeft != null)
            {
                roleLeft.Dispose();
            }
            if (roleMiddle != null)
            {
                roleMiddle.Dispose();
            }
            if (roleRight != null)
            {
                roleRight.Dispose();
            }
            if (dramaUI != null)
            {
                dramaUI.Dispose();
            }
        }
        #endregion

        #region ContentLoad
        public void ContentLoad()
        {
            dramaUI = LoadHelper.LoadTexture2D("GameUI");
        }
        #endregion

        #region InitSelf
        public void InitSelf()
        {
            speakingRole = -1;
        }
        #endregion

        #region UpdateSelf
        public void UpdateSelf()
        {
        }
        #region Update Drama
        public enum DramaState
        {
            On,
            Finished,
        }
        /// <summary>
        /// 更新剧本
        /// </summary>
        public DramaState UpdateDrama()
        {
            DramaData.DEventIndex dEventIndex = drama.GetNextEvent();
            if (dEventIndex != null)
            {
                switch (dEventIndex.EventType)
                {
                    case DramaData.DEventIndex.DEventType.Dialog:
                        DramaData.DEventDialog dEventDialog = drama.GetNextDialog();
                        if (dEventDialog != null)
                        {
                            // Ui显示对话框
                            ShowDialog();
                            dialogStr = dEventDialog.DialogContent;
                            speakingRole = dEventDialog.RoleId;
                        }
                        else
                        {
                            throw new Exception("剧本文件错误, 对话事件超出最大索引");
                        }
                        break;

                    case DramaData.DEventIndex.DEventType.ShowPicture:
                        DramaData.DEventShowPicture dEventShowPicture = drama.GetNextShowPicture();
                        if (dEventShowPicture != null)
                        {
                            showPicture = drama.imageList[dEventShowPicture.PictureId];
                        }
                        else
                        {
                            //throw new Exception("No more picture to show !");
                        }
                        break;

                    case DramaData.DEventIndex.DEventType.HidePicture:
                        // 隐藏图片
                        showPicture = null;
                        // 继续下一个剧本事件
                        UpdateDrama();
                        break;

                    case DramaData.DEventIndex.DEventType.ShowRole:
                        DramaData.DEventShowRole dEventShowRole = drama.GetNextShowRole();
                        // 显示角色...
                        switch (dEventShowRole.Pos)
                        {
                            case DramaData.DEventShowRole.RolePos.Left:
                                roleLeft = drama.roleList[dEventShowRole.RoleId];
                                break;
                            case DramaData.DEventShowRole.RolePos.Right:
                                roleRight = drama.roleList[dEventShowRole.RoleId];
                                break;
                        }
                        // 继续下一个剧本事件
                        UpdateDrama();
                        break;

                    case DramaData.DEventIndex.DEventType.HideRole:
                        DramaData.DEventHideRole dEventHideRole = drama.GetNextHideRole();
                        if (dEventHideRole != null)
                        {
                            //ChangeRole(dEventHideRole.
                        }
                        // 继续下一个剧本事件
                        UpdateDrama();
                        break;

                    case DramaData.DEventIndex.DEventType.Aside:
                        DramaData.DEventAside dEventAside = drama.GetNextAside();
                        // 显示对话，但是不显示角色的名字...
                        break;

                    case DramaData.DEventIndex.DEventType.Flicker:
                        DramaData.DEventFlicker dEventFlicker = drama.GetNextFlicker();
                        // 闪烁...
                        break;
                }
                return DramaState.On;
            }
            else
            {
                dialogStr = "";
                // 清理剧本
                ClearDrama();

                return DramaState.Finished;
            }
        }

        // 切换角色大图
        public void ChangeRole(bool isLeft, String roleName)
        {
            if (isLeft)
            {
                roleLeft = LoadHelper.LoadTexture2D(roleName);
            }
            else
            {
                roleRight = LoadHelper.LoadTexture2D(roleName);
            }
        }
        #endregion
        #endregion

		#region DrawSelf
		/// <summary>
		/// Draw
		/// </summary>
        public void DrawSelf()
		{
            if (drama != null)
            {
                // 绘制角色头像
                RenderGameRole();
                // 绘制界面
                RenderGameUI();
                if (!StringHelper.IsNullOrEmpty(dialogStr))
                {
                    UTF8Writer.WriteText(300, Survival_Game.Height - 80, dialogStr);
                }
                if (showPicture != null)
                {
                    RenderShowPicture();
                }
            }
		}
		#endregion

        #region DramaUI
        /// <summary>
        /// 绘制角色
        /// </summary>
        /// <param name="roleId">讲话角色Id 值是-1时所有人物正常显示</param>
        public void RenderGameRole()
        {
            int xPos, yPos;
            xPos = yPos = 0;
            if (roleLeft != null)
            {
                Color color;
                if (speakingRole == -1 || speakingRole == 0)
                {
                    color = Color.White;
                }
                else if (speakingRole == 1)
                {
                    color = Color.Gray;
                }
                else
                {
                    // 意外情况
                    color = Color.Red;
                }
                yPos = Survival_Game.Height - roleLeft.Height;
                Painter.Instance.DrawT(roleLeft, new Vector2(xPos, yPos),color);
            }
            if (roleRight != null)
            {
                Color color;
                if (speakingRole == -1 || speakingRole == 1)
                {
                    color = Color.White;
                }
                else if (speakingRole == 0)
                {
                    color = Color.Gray;
                }
                else
                {
                    // 意外情况
                    color = Color.Red;
                }
                xPos = Survival_Game.Width - roleRight.Width;
                yPos = Survival_Game.Height - roleRight.Height;
                Painter.Instance.DrawT(roleRight, new Vector2(xPos, yPos), color);
            }
        }
        /// <summary>
        /// 绘制用户界面
        /// </summary>
        /// <param name="gameTime">游戏进行时间</param>
        private bool showDialog = false;
        public void RenderGameUI()
        {
            int xPos, yPos;
            xPos = yPos = 0;
            // 绘制用户界面
            if (showDialog)
            {
                yPos = Survival_Game.Height - DialogLeftGfxRect.Height;
                int blandWidth = Survival_Game.Width - DialogLeftGfxRect.Width - DialogRightGfxRect.Width;
                Painter.Instance.DrawT(dramaUI, new Rectangle(0, yPos, DialogLeftGfxRect.Width, DialogLeftGfxRect.Height), DialogLeftGfxRect);
                Painter.Instance.DrawT(dramaUI, new Rectangle(DialogLeftGfxRect.Width, yPos, blandWidth, DialogMiddleGfxRect.Height), DialogMiddleGfxRect);
                Painter.Instance.DrawT(dramaUI, new Rectangle(DialogLeftGfxRect.Width + blandWidth, yPos, DialogRightGfxRect.Width, DialogRightGfxRect.Height), DialogRightGfxRect);
                // 绘制闪烁NextButton
                xPos = Survival_Game.Width - 67;
                yPos = Survival_Game.Height - 53;
                float alphaValue = 0;
                float t = GameMgr.gameTimeInMs % 2000;
                alphaValue = t < 1000 ? (t / 1000.0f) : 2 - t / 1000;
                Painter.Instance.DrawT(dramaUI, new Rectangle(xPos, yPos, 20, 20), NextButtonGfxRect, new Color(1, 1, 1, alphaValue));
            }
        }
        public void ShowDialog()
        {
            showDialog = true;
        }
        public void HideDialog()
        {
            showDialog = false;
        }
        /// <summary>
        /// 绘制显示图片
        /// </summary>
        public void RenderShowPicture()
        {
            Console.WriteLine("RenderShowPicture");
            int x = Survival_Game.Width / 2 - showPicture.Width / 2;
            int y = Survival_Game.Height / 2 - showPicture.Height / 2;
            Rectangle picRect = new Rectangle(x, y, showPicture.Width, showPicture.Height);
            Painter.Instance.DrawT(showPicture, picRect);

        }

        public void ClearDrama()
        {
            HideDialog();
            roleLeft = null;
            roleMiddle = null;
            roleRight = null;
        }
        #endregion

        #region UnitTest

        #endregion
    }
}
