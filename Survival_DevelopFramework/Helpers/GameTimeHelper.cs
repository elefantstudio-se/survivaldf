using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

//暂时不用
namespace Survival_DevelopFramework.Helpers
{
    class GameTimeHelper
    {
        private  static GameTime  mGameTime;
        public static GameTime GameTime
        {
            get { return mGameTime; }
        }

        public static void InitTimeHelper(GameTime gt)
        {
            mGameTime = gt;
        }
    }
}
