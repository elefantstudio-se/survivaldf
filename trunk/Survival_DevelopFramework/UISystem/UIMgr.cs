﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survival_DevelopFramework.Items;
using Survival_DevelopFramework.Items.DramaManager;

namespace Survival_DevelopFramework.UISystem
{
    class UIMgr:ItemMgr
    {
        #region Variables
        #endregion

        #region Instance
        protected new static UIMgr instance;
        /// <summary>
        /// 单例
        /// </summary>
        public new static UIMgr Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    instance = new UIMgr();
                    return instance;
                }
            }
        }
        /// <summary>
        /// 私有构造函数
        /// </summary>
        protected UIMgr()
        {
            // 进行初始化处理...

        }
        #endregion

        #region Update
        public override void Update()
        {
            DramaMgr.Instance.Update();
            base.Update();
        }
        #endregion

        #region Draw
        public override void Draw()
        {
            DramaMgr.Instance.Draw();
            base.Draw();
        }
        #endregion
    }
}
