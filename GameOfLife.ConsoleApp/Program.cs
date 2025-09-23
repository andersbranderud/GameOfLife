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

            var tickCount = 1;

            while (_run)
            { 
                world.Render();
                Console.Write($" {tickCount}");
                Thread.Sleep(500);
                tickCount++;
            }
        }
    }
}
