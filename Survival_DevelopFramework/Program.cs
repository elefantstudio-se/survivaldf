using System;
using Survival_DevelopFramework.GraphicSystem;

namespace Survival_DevelopFramework
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
           
            using (Survival_Game game = new Survival_Game())
            {
                game.Run();
           }
        }
    }
}

