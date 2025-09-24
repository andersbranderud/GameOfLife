using Pacman.Model.Enums;
using System.Collections.Generic;

namespace Pacman.Model.Models
{
    internal class Map
    {
        private readonly int _initialDots;
        private readonly int _mapNr;
        private readonly int _pacmanStartX;
        private readonly int _pacmanStartY;
        private readonly char[,] _grid;
        private readonly int _nrOfColumns;
        private readonly int _nrOfRows;
        private HashSet<(int, int)> _wallPositions = new HashSet<(int, int)>();

        // Getters for all private properties
        public int InitialDots => _initialDots;
        public int RemainingDots {private set; get; }
        public int MapNr => _mapNr;
        public int PacmanStartX => _pacmanStartX;
        public int PacmanStartY => _pacmanStartY;
        public char[,] Grid => _grid;
        public int NrOfColumns => _nrOfColumns;
        public int NrOfRows => _nrOfRows;
        public HashSet<(int, int)> WallPositions => _wallPositions;

        internal Map(int mapNr, int pacmanStartX, int pacmanStartY, int rowCount, int colCount, char[,] grid, int initialDots, HashSet<(int, int)> wallPositions)
        {
            _pacmanStartX = pacmanStartX;
            _pacmanStartY = pacmanStartY;
            this._nrOfRows = rowCount;
            this._nrOfColumns = colCount;
            this._initialDots = initialDots;
            RemainingDots = initialDots;
            _grid = grid;
            _wallPositions = wallPositions;
            _mapNr = mapNr;
        }
        public bool IsValidPosition(int x, int y)
        {
            return !_wallPositions.Contains((y, x));
        }

        /// <summary>
        /// Pacman has moved already. Just redraw for new position.
        /// </summary>
        /// <param name="pacman"></param>
        public void RedrawMapForNewPacmanPosition(PacmanPlayer pacman)
        {
            var (oldX, oldY) = (pacman.LastPositionX, pacman.LastPositionY);
            var (newX, newY) = (pacman.CurrentPositionX, pacman.CurrentPositionY);

            // Old position now has an eaten fruit.
            _grid[oldY, oldX] = SymbolConstants.EatenFruit;

            char currentSymbol = _grid[newY, newX];
            // New position has pacman depending on direction.
            _grid[newY, newX] = GetSymbolForCurrentDirection(pacman);

            if (currentSymbol ==  SymbolConstants.Fruit)
            {
                RemainingDots--;
            }            
        }

        private char GetSymbolForCurrentDirection(PacmanPlayer pacman)
        {
            switch (pacman.CurrentDirection)
            {
                case DirectionEnum.Up:
                    return SymbolConstants.PacmanUp;
                case DirectionEnum.Down:
                    return SymbolConstants.PacmanDown;
                case DirectionEnum.Left:
                    return SymbolConstants.PacmanLeft;
                case DirectionEnum.Right:
                    return SymbolConstants.PacmanRight;
                default:
                    return SymbolConstants.PacmanRight; // Default to right if no direction is set
            }
        }

        internal (int x, int y) GetNewPosition(PacmanPlayer pacman)
        {
            var currentX = pacman.CurrentPositionX;
            var currentY = pacman.CurrentPositionY;
            var originalX = currentX;
            var originalY = currentY;

            switch (pacman.CurrentDirection)
            {
                case DirectionEnum.Up:
                    if (currentY - 1 < 0)
                    {
                        currentY = NrOfRows;
                    }

                    if (IsValidPosition(currentX, currentY - 1))
                    {
                        return (currentX, currentY - 1);
                    }
                    break;
                case DirectionEnum.Down:
                    if (currentY + 1 >= NrOfRows)
                    {
                        currentY = -1;
                    }

                    if (IsValidPosition(currentX, currentY + 1))
                    {
                        return (currentX, currentY + 1);
                    }
                    break;
                case DirectionEnum.Left:
                    if (currentX - 1 < 0)
                    {
                        currentX = NrOfColumns;
                    }

                    if (IsValidPosition(currentX - 1, currentY))
                    {
                        return (currentX - 1, currentY);
                    }
                    break;
                case DirectionEnum.Right:
                    if (currentX + 1 >= NrOfColumns)
                    {
                        currentX = -1;
                    }

                    if (IsValidPosition(currentX + 1, currentY))
                    {
                        return (currentX + 1, currentY);
                    }
                    break;
                default:
                    return (currentX, currentY); // No movement if direction is None
            }
            // Return original position if movement is invalid
            return (originalX, originalY); 
        }

        internal bool HasRemainingDots()
        {
            return RemainingDots != 0;
        }
    }
}