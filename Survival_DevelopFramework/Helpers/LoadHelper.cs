using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Survival_DevelopFramework.Helpers
{
    class LoadHelper
    {
        private static ContentManager mContent;
        public static ContentManager Content
        {
            get { return mContent; }
        }
        static public void InitLoadHelper(ContentManager c)
        {
            mContent = c;
        }
    }
}
