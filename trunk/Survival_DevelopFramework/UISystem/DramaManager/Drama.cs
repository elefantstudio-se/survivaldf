using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survival_DevelopFramework.Helpers;
using Microsoft.Xna.Framework.Graphics;

namespace Survival_DevelopFramework.Items.DramaManager
{
    class Drama
    {

        #region Constructor
        // 默认构造函数
        public Drama()
        {
        }
        // 使用DramaData构造对象
        public Drama(DramaData setDramaData)
        {
            dramaData = setDramaData;
            #region 载入图片资源
            foreach (DramaData.DPicture dPicture in dramaData.dPictures)
            {
                if (!StringHelper.IsNullOrEmpty(dPicture.PictureName))
                {
                    imageList.Add(LoadHelper.LoadTexture2D(dPicture.PictureName));
                }
            }
            foreach (DramaData.DRole dRole in dramaData.dRoles)
            {
                if (!StringHelper.IsNullOrEmpty(dRole.RoleName))
                {
                    roleList.Add(LoadHelper.LoadTexture2D(dRole.RoleName));
                }
            }
            #endregion

        }
        #endregion

        #region Variables
        public int currentEvent = -1;
        public int currentDialog = -1;
        public int currentShowPicture = -1;
        public int currentHidePicture = -1;
        public int currentShowRole = -1;
        public int currentHideRole = -1;
        public int currentAside = -1;
        public int currentFlicker = -1;
        public int currentSound = -1;
        public int currentMusic = -1;

        /// <summary>
        /// 剧本数据
        /// </summary>
        private DramaData dramaData;
        public List<Texture2D> imageList = new List<Texture2D>();

        public List<Texture2D> roleList = new List<Texture2D>();
        #endregion

        #region Proterties
        #endregion

        #region Drama Logic
        /// <summary>
        ///  当前事件索引
        /// </summary>
        /// <returns></returns>
        public DramaData.DEventIndex GetNextEvent()
        {
            currentEvent++;
            if (currentEvent >= dramaData.dEventIndices.Count)
            {
                return null;
            }
            else
            {
                return dramaData.dEventIndices[currentEvent];
            }
        }
        /// <summary>
        /// 当前对话索引
        /// </summary>
        /// <returns></returns>
        public DramaData.DEventDialog GetNextDialog()
        {
            currentDialog++;
            if (currentDialog >= dramaData.dEventDialogs.Count)
            {
                return null;
            }
            else
            {
                return dramaData.dEventDialogs[currentDialog];
            }
        }
        /// <summary>
        /// 当前显示图片事件索引
        /// </summary>
        /// <returns></returns>
        public DramaData.DEventShowPicture GetNextShowPicture()
        {
            currentShowPicture++;
            if (currentShowPicture >= dramaData.dEventShowPictures.Count)
            {
                return null;
            }
            else
            {
                return dramaData.dEventShowPictures[currentShowPicture];
            }
        }

        public DramaData.DEventShowRole GetNextShowRole()
        {
            currentShowRole++;
            if (currentShowRole >= dramaData.dEventShowRoles.Count)
            {
                return null;
            }
            else
            {
                return dramaData.dEventShowRoles[currentShowRole];
            }
        }

        public DramaData.DEventHideRole GetNextHideRole()
        {
            currentHideRole++;
            if (currentHideRole >= dramaData.dEventShowPictures.Count)
            {
                return null;
            }
            else
            {
                return dramaData.dEventHideRoles[currentHideRole];
            }
        }

        public DramaData.DEventAside GetNextAside()
        {
            currentAside++;
            if (currentAside >= dramaData.dEventAsides.Count)
            {
                return null;
            }
            else
            {
                return dramaData.dEventAsides[currentHideRole];
            }
        }


        public DramaData.DEventFlicker GetNextFlicker()
        {
            currentFlicker++;
            if (currentFlicker >= dramaData.dEventFlickers.Count)
            {
                return null;
            }
            else
            {
                return dramaData.dEventFlickers[currentFlicker];
            }
        }

        public DramaData.DEventSound GetNextSound()
        {
            currentSound++;
            if (currentSound >= dramaData.dEventSounds.Count)
            {
                return null;
            }
            else
            {
                return dramaData.dEventSounds[currentSound];
            }
        }

        public DramaData.DEventMusic GetNextMusic()
        {
            currentMusic++;
            if (currentMusic >= dramaData.dEventMusics.Count)
            {
                return null;
            }
            else
            {
                return dramaData.dEventMusics[currentMusic];
            }
        }
        #endregion
    }
}
