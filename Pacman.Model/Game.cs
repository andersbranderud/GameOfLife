using Pacman.Model.Models;
using System;

namespace Pacman.Model
{
    public class Game
    {
        Map _currentMap = null;
        int currentMapNr = 1;
        int DefaultNrOfRows = 5;

        private PacmanPlayer pacman;
        // Create game
        public Game()
        {
        }

        // Create game with a fixed starting pattern
        public Game(string world)
        {
            if (string.IsNullOrEmpty(world))
            {
                throw new ArgumentNullException("World must contain values");
            }

            var nrOfColumns = world.Length / DefaultNrOfRows;
            _currentMap = MapUtility.ReadStringIntoMap(world, DefaultNrOfRows, nrOfColumns, currentMapNr);
        }

        public void InitGame()
        {
            if (_currentMap == null)
            {
                _currentMap = MapUtility.GetMapFromFile(currentMapNr);
            }

            pacman = new PacmanPlayer(_currentMap.PacmanStartX, _currentMap.PacmanStartY);
        }

        // Runs every tick
        // Returns if has completed map.
        public bool Render()
        {
            if (_currentMap == null || pacman == null)
            {
                throw new InvalidOperationException("Game not initialized. Call InitGame() before Render().");
            }
            var (currentX, currentY) = (pacman.CurrentPositionX, pacman.CurrentPositionY);
            var currentDirection = pacman.CurrentDirection;
            var desiredDirection = pacman.DesiredDirection;

            // New direction has been given by user input whilst sleeping
            if (currentDirection != desiredDirection)
            {
                pacman.CurrentDirection = desiredDirection;
            }

            pacman.SetLastPosition(currentX, currentY);
            (int x, int y) newPosition = _currentMap.GetNewPosition(pacman);
            pacman.SetCurrentPosition(newPosition.x, newPosition.y);
            _currentMap.RedrawMapForNewPacmanPosition(pacman);
            MapUtility.PrintOutMap(_currentMap);

            bool hasRemainingDots = _currentMap.HasRemainingDots();

            if (!hasRemainingDots)
            {
                Console.Write("Congratulations! You completed this map!");
                return true;
            }

            return false;
        }

        public void CheckUserInputAndUpdateDirection(ConsoleKey key)
        {
            GameControlHelper.CheckUserInputAndUpdateDirection(key, pacman);
        }

    }
}
