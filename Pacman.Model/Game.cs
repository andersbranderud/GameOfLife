using System;

namespace Pacman.Model
{
    public class Game
    {
        // Create game
        public Game()
        {

        }

        // Create game with a fixed starting pattern
        public Game(string world)
        {

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
