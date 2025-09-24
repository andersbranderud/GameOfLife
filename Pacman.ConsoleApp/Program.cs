using Pacman.Model;
using System;
using System.Threading;

namespace Pacman.ConsoleApp
{
    class Program
    {
        private static bool _run = true;

        static void Main(string[] args)
        {
            var game = new Game();
            Console.WriteLine("Hello and welcome to Pacman!\n");
            Console.WriteLine("Use the arrow keys to control the direction that Pacman is moving. " +
                "You can press several consecutive keys, and they will be put in a queue and ran in order. Any potential direction change takes place at the next tick.\n");

            // Wait
            Console.WriteLine("Press a key to start the game.");
            Console.ReadKey();

            Console.CancelKeyPress += (sender, args) =>
            {
                _run = false;
                Console.WriteLine("\nExit");
            };

            var tickCount = 1;
            var currentStateNr = 1;

            Console.WriteLine("Press Ctrl+C to exit.");

            game.InitGame();

            while (_run)
            {
                Console.Clear();
                bool isComplete = game.Render();
                
                if (isComplete)
                {
                    break;
                }

                // Non-blocking keyboard event listener
                if (Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    game.CheckUserInputAndUpdateDirection(keyInfo.Key);
                }

                Thread.Sleep(500);
                tickCount++;
                currentStateNr++;
            }
        }
    }
}
