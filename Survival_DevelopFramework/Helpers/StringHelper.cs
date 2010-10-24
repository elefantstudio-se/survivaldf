using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survival_DevelopFramework.Helpers
{
    class StringHelper
    {
        public static bool IsNullOrEmpty(string str)
        {
            if (str == null || str == "")
            {
                return true;
            }
            return false;
        }
    }
}
