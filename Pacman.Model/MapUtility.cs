using Pacman.Model.Models;
using System;

namespace Pacman.Model
{
    internal static class MapUtility
    {
        public const string MapFolder = "Maps";
        public static Map GetMapFromFile(int mapNr)
        {
            string filePath = $"^{MapFolder}/{mapNr}.txt";

            if (!System.IO.File.Exists(filePath))
            {
                throw new System.IO.FileNotFoundException($"The map file '{filePath}' was not found.");
            }

            string world = System.IO.File.ReadAllText(filePath);
            return ReadStringIntoMap(world);
        }

        public static Map ReadStringIntoMap(string world)
        {
            string[] rows = world.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int rowCount = rows.Length;
            int colCount = rows[0].Length;

            var pacmanStartX = -1;
            var pacmanStartY = -1;
            var initialDots = 0;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    var currentChar = rows[i][j];
                    map.Grid[i, j] = currentChar;

                    if (currentChar == SymbolConstants.PacmanStartPosition)
                    {
                        pacmanStartX = j;
                        pacmanStartY = i;
                    }
                    else if (currentChar == SymbolConstants.Fruit)
                    {
                        initialDots++;
                    }


                }
            }
            return new Map(pacmanStartX, pacmanStartY, rowCount, colCount, initialDots);
        }

    }
}
