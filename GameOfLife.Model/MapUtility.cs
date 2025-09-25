using System;
using System.Linq;
using Utility.Utility;

namespace GameOfLife.Model
{
    public static class MapUtility
    {
        public const string GliderPattern = "glider";
        public const string GliderPatternString = "0100000100111000000000000";
        public const int PatternWidthDefault = 5;
        public const int PatternHeightDefault = 5;

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
                    AssignDeadOrAliveStateRandomly(worldArray, x, y);
                }
            }

            return worldArray;
        }

        private static void AssignDeadOrAliveStateRandomly(int[,] worldArray, int x, int y)
        {
            worldArray[x, y] = new Random().Next(2);
        }

        // Create world with a given input grid
        public static int[,] GenerateWorld(string inputGrid, int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Width and Height must be greater than zero.");
            }

            int[,] worldArray = new int[width, height];
            
            var totalNumberOfCells = width * height;
            var totalLengthGrid = inputGrid.Length;

            if (totalLengthGrid != totalNumberOfCells)
            {
                throw new ArgumentException($"Pattern length {totalLengthGrid} does not match the total number of cells {totalNumberOfCells}.");
            }

            // Validate only 0 and 1 in pattern via reg exp
            string regExpPattern = "^[01]+$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(inputGrid, regExpPattern))
            {
                throw new ArgumentException("Pattern must only contain '0' and '1' characters.");
            }

            PopulateArrayBasedOnInputGrid(inputGrid, width, height, worldArray);

            return worldArray;
        }

        private static void PopulateArrayBasedOnInputGrid(string inputGrid, int width, int height, int[,] worldArray)
        {
            int currentIndex = 0;
            foreach (var x in Enumerable.Range(0, width))
            {
                foreach (var y in Enumerable.Range(0, height))
                {
                    int currentValue = (int)char.GetNumericValue(inputGrid[currentIndex]);
                    worldArray[x, y] = currentValue;
                    currentIndex++;
                }
            }
        }

        // First fill the inputGrid with 0's, until we reach position of startIndex, then fill the worldArray with content of inputGrid
        // then fill remaining with 0's
        private static int PopulateArrayBasedOnInputGridWithStartIndex(string inputGrid, int width, int height, int[,] worldArray, int startIndex)
        {
            int currentIndex = 0;
            var endIndexPatternInInputGrid = inputGrid.Length + startIndex;

            foreach (var x in Enumerable.Range(0, width))
            {
                foreach (var y in Enumerable.Range(0, height))
                {
                    int currentValue = StateHelper.DeadCell;

                    if (currentIndex >= startIndex | currentIndex < endIndexPatternInInputGrid )
                    {
                        var isWithinInputGridBoundaries = currentIndex < inputGrid.Length;
                        if (isWithinInputGridBoundaries)
                        {
                            currentValue = (int)char.GetNumericValue(inputGrid[currentIndex]);
                        }
                    }

                    worldArray[x, y] = currentValue;
                    currentIndex++;
                }
            }

            return currentIndex;
        }

        /// <summary>
        /// Generates an initial state based on a pattern and returns it.
        /// </summary>
        /// <param name="pattern">Currently supported patterns: glider</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static int[,] GenerateWorldBasedOnPattern(string pattern)
        {
            if (pattern == GliderPattern)
            {
                return GenerateWorld(GliderPatternString, PatternWidthDefault, PatternHeightDefault);                                                                            
            }
            else
            {
                throw new ArgumentException("Currently only 'glider' pattern is supported.");
            }

        }

        internal static void PrintOutCurrentState(int[,] currentState, bool replaceZerosWithBlankSpace = false)
        {
            var width = currentState.GetLength(0);
            var height = currentState.GetLength(1);
            
            foreach (int x in Enumerable.Range(0, width))
            {
                int[] allChars = ArrayUtility.GetRow(currentState, x);
                string rowString = string.Join(" ", allChars);

                if (replaceZerosWithBlankSpace)
                {
                    rowString = rowString.Replace('0', ' ');
                }
                
                Console.WriteLine($"{rowString}");
            }
        }
    }
}