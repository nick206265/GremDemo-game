using System;

namespace GremDemo
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DemoGame game = new DemoGame())
            {
                game.Run();
            }
        }
    }
#endif
}

