using GameOfLife.Model.Utility;
using Pacman.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pacman.Model
{
    internal static class MapUtility
    {
        public const string MapFolder = "Maps";
        public static Map GetMapFromFile(int mapNr)
        {
            string filePath = $"{MapFolder}/{mapNr}.txt";

            if (!System.IO.File.Exists(filePath))
            {
                throw new System.IO.FileNotFoundException($"The map file '{filePath}' was not found.");
            }

            var rowCount = 0;
            string world = "";

            using (StreamReader reader = File.OpenText(filePath))
            {
                var lines = new List<string>();
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                rowCount = lines.Count;
                world = string.Concat(lines);
            }

            var nrOfChars = world.Length;
            var colCount = nrOfChars / rowCount;

            return ReadStringIntoMap(world, rowCount, colCount, mapNr);
        }

        public static Map ReadStringIntoMap(string world, int rowCount, int colCount, int mapNr)
        {
            var pacmanStartX = -1;
            var pacmanStartY = -1;
            var initialDots = 0;

            if (string.IsNullOrEmpty(world))
            {
                throw new ArgumentException("World must have a value!");
            }
 
            if (rowCount <= 0 || colCount <= 0 )
            {
                throw new ArgumentException("Row count and col count must be bigger than 0.");
            }

            if (mapNr < 1)
            {
                throw new ArgumentException("Map nr must be 1 or bigger.");                
            }
            char[,] worldCharArray = ArrayUtility.ReadStringIntoCharArray(world, rowCount, colCount);

            HashSet<(int, int)> wallPositions = new HashSet<(int, int)>();
            // grid new char array
            
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    var currentChar = worldCharArray[i,j];
                    
                    if (currentChar == SymbolConstants.PacmanStartPosition)
                    {
                        pacmanStartX = j;
                        pacmanStartY = i;
                    }
                    else if (currentChar == SymbolConstants.Fruit)
                    {
                        initialDots++;
                    }
                    else if (currentChar == SymbolConstants.Wall)
                    {
                        wallPositions.Add((i, j));
                    }
                    else if (currentChar == SymbolConstants.EmptySpace)
                    {
                        //Ignore
                    }
                    else
                    {
                        throw new ArgumentException("Only fruits, walls and pacman symbols are allowed in the map.");
                    }
                }
            }
            return new Map(mapNr, pacmanStartX, pacmanStartY, rowCount, colCount, worldCharArray, initialDots, wallPositions);
        }


        public static void PrintOutMap(Map map)
        {
            char[,] currentMap = map.Grid;

            var width = currentMap.GetLength(0);
            var height = currentMap.GetLength(1);

            foreach (int x in Enumerable.Range(0, height))
            {
                char[] allChars = ArrayUtility.GetRow<char>(currentMap, x);
                string rowString = string.Join(" ", allChars);
                Console.WriteLine($"{rowString}");
            }
        }
    }
}
