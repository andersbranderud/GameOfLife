using Pacman.Model.Models;
using System;

namespace Pacman.Model
{
    public class Game
    {
        Map _currentMap = null;
        int currentMapNr = 1;

        private PacmanPlayer pacman;
        // Create game
        public Game()
        {
        }

        // Create game with a fixed starting pattern
        public Game(string world)
        {
            _currentMap = MapUtility.ReadStringIntoMap(world);
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
        public void Render()
        {
            if (_currentMap == null || pacman == null)
            {
                throw new InvalidOperationException("Game not initialized. Call InitGame() before Render().");
            }
            var (currentX, currentY) = pacman.CurrentPosition;
            var currentDirection = pacman.CurrentDirection;
            var desiredDirection = pacman.DesiredDirection;

            // New direction has been given by user input whilst sleeping
            if (currentDirection != desiredDirection)
            {
                pacman.CurrentDirection = desiredDirection;
            }
            
            // Calculate new position based on current direction
            int newX = currentX;
            int newY = currentY;            
        }

        public void CheckUserInputAndUpdateDirection(ConsoleKey key)
        {
            GameControlHelper.CheckUserInputAndUpdateDirection(key, pacman);
        }

    }


    // Rules with examples
    /*
     * - The game can be solved with the help of ticks, example: If pacman is facing right, pacman will continue moving right with every tick, 
     *   until his direction is changed with a keypress, then pacman will start moving in the new direction with every tick
     *   
     * - The game can also be solved with keypresses, pressing the left key, pacman will move left, pressing the up key, pacman will move up
     * 
     * - Pacman is on a world full of dots (facing right)
     *  . . . . .
     *  . . . . .
     *  . . < . .
     *  . . . . .
     *  . . . . .
     *  
     *  - Pacman is on a world full of dots (facing left)
     *  . . . . .
     *  . . . . .
     *  . . > . .
     *  . . . . .
     *  . . . . .
     *  
     *  - Pacman is on a world full of dots (facing up)
     *  . . . . .
     *  . . . . .
     *  . . V . .
     *  . . . . .
     *  . . . . .
     *  
     *  - Pacman is on a world full of dots (facing down)
     *  . . . . .
     *  . . . . .
     *  . . ^ . .
     *  . . . . .
     *  . . . . .
     *  
     *   - Pacman is eating dots while moving
     *  . . . . .   . . . . .
     *  . . . . .   . . . . .
     *  . . x < .   . . x x <
     *  . . . . .   . . . . .
     *  . . . . .   . . . . .
     *  
     *  - Pacman exists on one site and turns up on the opposite side
     *  . . . . .   . . . . .   . . . . .
     *  . . . . .   . . . . .   . . . . .
     *  . . x < .   . . x x <   < . x x x
     *  . . . . .   . . . . .   . . . . .
     *  . . . . .   . . . . .   . . . . .
     *  
     *  - Pacman stops at a wall
     *  . . . . . 
     *  . . . . .
     *  . . < | .
     *  . . . . . 
     *  . . . . . 
     *  
     *  - The game ends when all dots have been eaten
     *  x x x x x
     *  x x x x x
     *  x x x < x
     *  x x x x x 
     *  x x x x x 
     *  
     */

}
