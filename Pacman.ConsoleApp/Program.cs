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
            Console.WriteLine("Hello and welcome to Pacman!");

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

                Thread.Sleep(1000);
                tickCount++;
                currentStateNr++;
            }
        }
    }
}
