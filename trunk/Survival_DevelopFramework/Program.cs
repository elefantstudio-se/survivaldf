using System;
using Survival_DevelopFramework.Items.DramaManager;
using Survival_DevelopFramework.Items;

namespace Survival_DevelopFramework
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //using (Survival_Game game = new Survival_Game("Game"))
            //{
            //    game.Run();
            //}


            // Uint Test

            // ----   绘制级   ----
             AnimItem.UnitTest();
            // Role.UnitTest();

            // ---- Manager 级 ----
            // DramaMgr.UnitTest();
        }
    }
}

