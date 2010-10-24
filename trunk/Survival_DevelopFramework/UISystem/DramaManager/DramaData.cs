using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Survival_DevelopFramework.Helpers;

namespace Survival_DevelopFramework.Items.DramaManager
{
    [Serializable]
    public class DramaData
    {
        #region Constants
        /// <summary>
        /// Directory where all the track data files are stored.
        /// </summary>
        public const string ContentDir = "Content\\Dramas";
        /// <summary>
        /// Extension for the track data files.
        /// </summary>
        public const string Extension = "Dra";
        #endregion

        #region Variables
        ///<summary>
        /// 对白事件
        /// </summary>
        [Serializable]
        public class DEventDialog
        {
            /// <summary>
            /// 讲话人物Id
            /// </summary>
            public int RoleId;

            /// <summary>
            /// 讲话内容
            /// </summary>
            public string DialogContent;

            /// <summary>
            /// Create width helper
            /// </summary>
            public DEventDialog()
            {
            }

            /// <summary>
            /// Create width helper
            /// </summary>
            /// <param name="setPos">Set position</param>
            /// <param name="setScale">Set scale</param>
            public DEventDialog(int setRoleId, string setString)
            {
                RoleId = setRoleId;
                DialogContent = setString;
            }
        }
        public List<DEventDialog> dEventDialogs = new List<DEventDialog>();

        /// <summary>
        /// 剧本人物
        /// </summary>
        [Serializable]
        public class DRole
        {
            /// <summary>
            /// 人物名称
            /// </summary>
            public String RoleName;
        }
        /// <summary>
        /// 剧本人物列表
        /// </summary>
        public List<DRole> dRoles = new List<DRole>();

        /// <summary>
        /// 剧本图片
        /// </summary>
        [Serializable]
        public class DPicture
        {
            /// <summary>
            /// 图片名称
            /// </summary>
            public String PictureName;
        }
        public List<DPicture> dPictures = new List<DPicture>();

        /// <summary>
        /// 剧本音乐
        /// </summary>
        [Serializable]
        public class DMusic
        {
            /// <summary>
            /// 音乐名称
            /// </summary>
            public String MusicName;
        }
        public List<DMusic> dMusics = new List<DMusic>();

        /// <summary>
        /// 剧本音效
        /// </summary>
        [Serializable]
        public class DSound
        {
            /// <summary>
            /// 音效名称
            /// </summary>
            public String SoundName;
        }
        public List<DSound> dSounds = new List<DSound>();

        ///<summary>
        /// 剧本事件索引
        ///</summary>
        [Serializable]
        public class DEventIndex
        {
            /// <summary>
            /// 事件类型
            /// </summary>
            public enum DEventType
            {
                Dialog,
                ShowPicture,
                HidePicture,
                ShowRole,
                HideRole,
                Aside,
                Flicker,
                Sound,
                Music,
            }
            public DEventType EventType;
            /// <summary>
            /// 事件索引
            /// </summary>
            public uint DEventId;
        }
        public List<DEventIndex> dEventIndices = new List<DEventIndex>();

        ///<summary>
        /// 显示图片事件
        ///</summary>
        [Serializable]
        public class DEventShowPicture
        {
            /// <summary>
            /// 图片名称
            /// </summary>
            public int PictureId;
        }
        public List<DEventShowPicture> dEventShowPictures = new List<DEventShowPicture>();

        ///<summary>
        /// 清除图片事件
        ///</summary>
        [Serializable]
        public class DEventHidePicture
        {
            /// <summary>
            /// 图片id
            /// </summary>
            public int PictureId;
        }
        public List<DEventHidePicture> dEventHidePictures = new List<DEventHidePicture>();

        ///<summary>
        /// 显示角色事件
        ///</summary>
        [Serializable]
        public class DEventShowRole
        {
            /// <summary>
            /// 角色id
            /// </summary>
            public int RoleId;

            /// <summary>
            /// 显示位置
            /// </summary>
            public enum RolePos
            {
                Left,
                Middle,
                Right,
            }
            public RolePos Pos;

        }
        public List<DEventShowRole> dEventShowRoles = new List<DEventShowRole>();

        ///<summary>
        /// 隐藏角色事件
        ///</summary>
        [Serializable]
        public class DEventHideRole
        {
            /// <summary>
            /// 角色id
            /// </summary>
            public int RoleId;
        }
        public List<DEventHideRole> dEventHideRoles = new List<DEventHideRole>();

        ///<summary>
        /// 旁白事件
        /// </summary>
        [Serializable]
        public class DEventAside
        {
            /// <summary>
            /// 旁白内容
            /// </summary>
            public string AsideContent;
        }
        public List<DEventAside> dEventAsides = new List<DEventAside>();


        ///<summary>
        /// 闪屏事件
        /// </summary>
        [Serializable]
        public class DEventFlicker
        {
            /// <summary>
            /// 持续时间，以毫秒为单位
            /// </summary>
            public int SustainTime;
        }
        public List<DEventFlicker> dEventFlickers = new List<DEventFlicker>();

        ///<summary>
        /// 音乐事件
        /// </summary>
        [Serializable]
        public class DEventMusic
        {
            /// <summary>
            /// 音乐Id
            /// </summary>
            public int MusicId;

            /// <summary>
            /// 音乐事件类型
            /// </summary>
            public enum DEMType
            {
                On,
                Off,
            }
        }
        public List<DEventMusic> dEventMusics = new List<DEventMusic>();

        ///<summary>
        /// 音效事件
        /// </summary>
        [Serializable]
        public class DEventSound
        {
            /// <summary>
            /// 音效Id
            /// </summary>
            public int SoundId;
            /// <summary>
            /// 音效事件类型
            /// </summary>
            public enum DESType
            {
                Loop,
                Once,
            }
        }
        public List<DEventSound> dEventSounds = new List<DEventSound>();
        #endregion

        #region Constrator
        /// <summary>
        /// 构造Drama对象，以进行序列化
        /// </summary>
        public DramaData()
        {
        }

        /// <summary>
        /// 读取Drama文件
        /// </summary>
        static public DramaData Load(String setFilename)
        {
            // 读取文件
            StreamReader file = new StreamReader(LoadHelper.LoadFileStream(ContentDir + "\\" + setFilename + "." + "Dra"));
            // 将数据读入对象
            DramaData loadDramaData = (DramaData)
                new XmlSerializer(typeof(DramaData)).Deserialize(file.BaseStream);
            // 关闭文件
            file.Close();
            // 返回反序列化数据
            return loadDramaData;
        }
        #endregion
    }
}
