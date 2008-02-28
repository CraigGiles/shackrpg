using System;

namespace TestProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            // GameEngine.TestTitleScreenSprites();
                        
            using (GameEngine game = new GameEngine())
            { game.Run(); }
            
        }
    }
}

