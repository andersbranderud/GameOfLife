using GameOfLife.Model;
using System;
using System.Threading;

namespace GameOfLife.ConsoleApp
{
    class Program
    {
        private static bool _run = true;
        private const int DefaultWidth = 10;
        private const int DefaultLength = 10;
        private const int DefaultStartIndex = 0;
        private const bool ReplaceZerosWithBlanks = true;
        static void Main(string[] args)
        {
            var world = new World();

            Console.CancelKeyPress += (sender, args) =>
            {
                _run = false;
                Console.WriteLine("\nExit");
            };

            // Prompt for world dimensions
            Console.Write("Enter width (Default 10): ");
            if (!int.TryParse(Console.ReadLine(), out int width) || width <= 0)
            {
                if (width == 0)
                {
                    width = DefaultWidth;
                }
                else
                {
                    Console.WriteLine("Invalid width. Exiting.");
                    return;
                }
            }


            Console.Write("Enter length (Default 10): ");
            if (!int.TryParse(Console.ReadLine(), out int length) || length <= 0)
            {
                if (length == 0)
                {
                    length = DefaultLength;
                }
                else
                {
                    Console.WriteLine("Invalid length. Exiting.");
                    return;
                }
            }

            // Optional: prompt for initial pattern
            Console.Write("Enter initial pattern (Press enter for random pattern).\n " +
                "Example valid input: 0110 (for 2x2 grid); glider (defaults to 5x5 grid) \n");

            string pattern = Console.ReadLine();

            world.InitWorld(width, length, pattern, ReplaceZerosWithBlanks);

            var tickCount = 1;
            var currentStateNr = 1;

            Console.WriteLine("Press Ctrl+C to exit.");

            while (_run)
            {
                Console.Clear();
                world.Render();                
                Console.Write($"\nCurrent state {currentStateNr}\n\n");
                Thread.Sleep(1000);
                tickCount++;
                currentStateNr++;
            }
        }
    }
}
