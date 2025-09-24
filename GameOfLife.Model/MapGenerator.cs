using GameOfLife.Model.Utility;
using System;
using System.Linq;

namespace GameOfLife.Model
{
    public static class MapGenerator
    {
        public const string GliderPattern = "glider";

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

        // Create world with a given input grid
        public static int[,] GenerateWorld(string inputGrid, int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Width and Height must be greater than zero.");
            }

            int[,] worldArray = new int[width, height];
            int currentIndex = 0;
            
            var totalNumberOfCells = width * height;
            
            if (inputGrid.Length != totalNumberOfCells)
            {
                throw new ArgumentException($"Pattern length {inputGrid.Length} does not match the total number of cells {totalNumberOfCells}.");
            }

            // Validate only 0 and 1 in pattern via reg exp
            string regExpPattern = "^[01]+$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(inputGrid, regExpPattern))
            {
                throw new ArgumentException("Pattern must only contain '0' and '1' characters.");
            }

            foreach (var x in Enumerable.Range(0, width))
            {
                foreach (var y in Enumerable.Range(0, height))
                {
                    // Randomly assign 0 or 1 to each cell
                    int currentValue = (int)char.GetNumericValue(inputGrid[currentIndex]);
                    worldArray[x, y] = currentValue;
                    currentIndex++;
                }
            }

            return worldArray;
        }

        public static int[,] GenerateWorldBasedOnPattern(string pattern)
        {
            int width = 5;
            int height = 5;

            if (pattern == GliderPattern)
            {
                string gridArray = "0100000100111000000000000";
                return GenerateWorld(gridArray, width, height);                                                                            
            }
            else
            {
                throw new ArgumentException("Currently only 'glider' pattern is supported.");
            }

        }

        internal static void PrintOutCurrentState(int[,] currentState)
        {
            var width = currentState.GetLength(0);
            var height = currentState.GetLength(1);
            
            foreach (int x in Enumerable.Range(0, width))
            {
                int[] allChars = ArrayUtility.GetRow(currentState, x);
                string rowString = string.Join(" ", allChars);
                Console.WriteLine($"{rowString}");
            }
        }
    }
}