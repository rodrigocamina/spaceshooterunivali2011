using System;

namespace XNAGameSpaceShotter {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameCore game = new GameCore())
            {
                game.Run();
            }
        }
    }
#endif
}

