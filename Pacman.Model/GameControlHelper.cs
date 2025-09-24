using Pacman.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Model
{
    internal static class GameControlHelper
    {
        internal static void CheckUserInputAndUpdateDirection(ConsoleKey key, PacmanPlayer pacman)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    pacman.DesiredDirection = Enums.DirectionEnum.Up;
                    break;
                case ConsoleKey.DownArrow:
                    pacman.DesiredDirection = Enums.DirectionEnum.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    pacman.DesiredDirection = Enums.DirectionEnum.Left;
                    break;
                case ConsoleKey.RightArrow:
                    pacman.DesiredDirection = Enums.DirectionEnum.Right;
                    break;
                default:
                    // Ignore other keys
                    break;
            }
        }
    }
}
