using GameOfLife.Model;
using System;
using System.Threading;

namespace GameOfLife.ConsoleApp
{
    class Program
    {
        private static bool _run = true;

        static void Main(string[] args)
        {
            var world = new World();

            Console.CancelKeyPress += (sender, args) =>
            {
                _run = false;
                Console.WriteLine("\nExit");
            };

            // Prompt for world dimensions
            Console.Write("Enter width: ");
            if (!int.TryParse(Console.ReadLine(), out int width) || width <= 0)
            {
                Console.WriteLine("Invalid width. Exiting.");
                return;
            }

            Console.Write("Enter length: ");
            if (!int.TryParse(Console.ReadLine(), out int length) || length <= 0)
            {
                Console.WriteLine("Invalid length. Exiting.");
                return;
            }

            // Optional: prompt for initial pattern
            Console.Write("Enter initial pattern (optional, press Enter to skip). Example valid input: 0110 (for 2x2 grid); glider (uses 5x5 grid) ");
            string pattern = Console.ReadLine();

            world.InitWorld(width, length, pattern);

            var tickCount = 1;
            var currentStateNr = 1;

            Console.WriteLine("Press Ctrl+C to exit.");

            while (_run)
            { 
                world.Render();
                Console.Write($"\nCurrent state {currentStateNr}\n\n");
                Thread.Sleep(500);
                tickCount++;
                currentStateNr++;
            }
        }
    }
}
