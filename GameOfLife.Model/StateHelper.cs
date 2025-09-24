using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Model
{
    internal static class StateHelper
    {
        private const int AliveCell = 1;
        private const int DeadCell = 0;

        public static int[,] GenerateNewGameOfLifeState(int[,] currentState)
        {
            var nrOfRows = currentState.GetLength(0);
            var nrOfCols = currentState.GetLength(1);
            int[,] newState = new int[nrOfRows, nrOfCols];

            for (int j = 0; j < nrOfRows; j++)
            {
                for (int k = 0; k < nrOfCols; k++)
                {
                    var currentCell = currentState[j, k];
                    int[] neighboursOfCell = GetValuesOfNeighbours(currentState, j, k);
                    var newStateOfCell = GetNewStateOfCell(neighboursOfCell, currentCell);
                    newState[j, k] = newStateOfCell;
                }
            }
            return newState;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentState">The map of all states.</param>
        /// <param name="currentRow"></param>
        /// <param name="currentColumn"></param>
        /// <returns></returns>
        private static int[] GetValuesOfNeighbours(int[,] currentState, int currentRow, int currentColumn)
        {
            List<int> allNeighbours = new List<int>();
            AddNeighbourIfExists(currentState, currentRow -1, currentColumn, allNeighbours);
            AddNeighbourIfExists(currentState, currentRow -1, currentColumn +1, allNeighbours);
            AddNeighbourIfExists(currentState, currentRow, currentColumn +1, allNeighbours);
            AddNeighbourIfExists(currentState, currentRow +1, currentColumn +1, allNeighbours);
            AddNeighbourIfExists(currentState, currentRow +1, currentColumn, allNeighbours);
            AddNeighbourIfExists(currentState, currentRow +1, currentColumn -1, allNeighbours);
            AddNeighbourIfExists(currentState, currentRow, currentColumn -1, allNeighbours);
            AddNeighbourIfExists(currentState, currentRow -1, currentColumn -1, allNeighbours);
            return allNeighbours.ToArray();

        }

        private static void AddNeighbourIfExists(int[,] currentState, int currentRow, int currentColumn, List<int> allNeighbours)
        {
            var isValidPosition = currentRow >= 0 && currentRow < currentState.GetLength(0)
                                  && currentColumn >= 0 && currentColumn < currentState.GetLength(1);

            if (isValidPosition)
            {
                allNeighbours.Add(currentState[currentRow, currentColumn]);
            }
        }


        /// <summary>
        /// Alive cell – Fewer than 2 alive neighbours – dies(underpopulation).
        ///  Alive cell – 2 or 3 neighbours – continues to live(perfect situation).
        ///  Alive cell – More than 3 alive neighbours – dies(overpopulation).
        ///  Dead cell – Exactly three alive neighbours – becomes alive(reproduction).
        /// </summary>
        /// <param name="allNeighbours">All neighbours value of the cell, either 0 Dead or 1 Alive.</param>
        /// <param name="currentState">State of the current cell.</param>
        /// <returns>The new state of the cell.</returns>
        /// <exception cref="System.Exception"></exception>
        private static int GetNewStateOfCell(int[] allNeighbours, int currentState)
        {
            var nrOfAliveNeighbours = allNeighbours.Count(n => n == AliveCell);
            var nrOfDeadNeighbours = allNeighbours.Count(n => n == DeadCell);

            if (currentState == AliveCell)
            {
                // Dies of underpopulation
                if (nrOfAliveNeighbours < 2)
                {
;                    return DeadCell;
                }
                // Continues to live
                else if (nrOfAliveNeighbours == 2 || nrOfAliveNeighbours == 3)
                {
                    return AliveCell;
                }
                // Dies of overpopulation.
                else if (nrOfAliveNeighbours > 3)
                {
                    return DeadCell;
                }
            }
            // 3 neighbours => dead cell becomes alive.
            else if (currentState == DeadCell)
            {
                if (nrOfAliveNeighbours == 3)
                {
                    return AliveCell;
                }
            }
            else
            {
                throw new System.Exception($"Invalid cell state {currentState}. Cell state must be either {AliveCell} or {DeadCell}.");
            }

            return DeadCell;
        }
    }
}
