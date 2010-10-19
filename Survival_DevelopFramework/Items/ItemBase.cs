using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survival_DevelopFramework.Items
{
    public interface ItemBase
    {
        #region Item的基础方法组
        void DrawSelf();
        void UpdateSelf();
        void ContentLoad();
        void InitSelf();
        #endregion
    }
}
