using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survival_DevelopFramework.Items;

namespace Survival_DevelopFramework.Factory
{
    class ItemFactory
    {
        public static ItemBase CreateItem(string itemname)
        {
            //根据字符串动态创建类
            return (ItemBase)Activator.CreateInstance(System.Type.GetType( "Survival_DevelopFramework.Items."+itemname));
        }
    }
}
