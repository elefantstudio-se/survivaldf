using System;
using Survival_DevelopFramework.Items.MovieManager;
using Survival_DevelopFramework.Items;
using Survival_DevelopFramework.SceneManager;

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

            // ----    Item    ----
            // ItemBase.UnitTest();
            // AnimItem.UnitTest();
            // Role.UnitTest();

            // ---- Manager 级 ----
            // DramaMgr.UnitTest();
            // ItemMgr.UnitTest();
             SceneData.UnitTest();
        }
    }
}

