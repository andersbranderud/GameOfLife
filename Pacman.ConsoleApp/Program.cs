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
                game.Render();
                Console.Write($"\nCurrent state {currentStateNr}\n\n");

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
