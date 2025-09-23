using GameOfLife.Model.Utility;
using System;
using System.Linq;

namespace GameOfLife.Model
{
    public static class MapGenerator
    {
        // Return a 2D array of ints representing the world
        public static int[,] GenerateWorld(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Width and Height must be greater than zero.");
            }

            int[,] worldArray = new int[width, height];

            foreach (var x in Enumerable.Range(0, width))
            {
                foreach (var y in Enumerable.Range(0, height))
                {
                    // Randomly assign 0 or 1 to each cell
                    worldArray[x, y] = new Random().Next(2);
                }
            }

            return worldArray;
        }

        // Create world with a fixed starting pattern
        public static int[,] GenerateWorld(string pattern, int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Width and Height must be greater than zero.");
            }

            int[,] worldArray = new int[width, height];
            int currentIndex = 0;
            
            var totalNumberOfCells = width * height;
            
            if (pattern.Length != totalNumberOfCells)
            {
                throw new ArgumentException($"Pattern length {pattern.Length} does not match the total number of cells {totalNumberOfCells}.");
            }

            // Validate only 0 and 1 in pattern via reg exp
            string regExpPattern = "^[01]+$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(pattern, regExpPattern))
            {
                throw new ArgumentException("Pattern must only contain '0' and '1' characters.");
            }

            foreach (var x in Enumerable.Range(0, width))
            {
                foreach (var y in Enumerable.Range(0, height))
                {
                    // Randomly assign 0 or 1 to each cell
                    worldArray[x, y] = pattern[currentIndex];
                    currentIndex++;
                }
            }

            return worldArray;
        }
        
        internal static void PrintOutCurrentState(int[,] currentState)
        {
            var width = currentState.GetLength(0);
            var height = currentState.GetLength(1);

            foreach (int x in Enumerable.Range(0, width))
            {
                int[] allChars = ArrayUtility.GetRow(currentState, x);
                Console.WriteLine($"{allChars.Select(t => $"{t} ")} ");
            }
        }
    }
}