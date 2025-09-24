using Pacman.Model.Enums;
using Pacman.Model.Models;
using System;

namespace Pacman.Model
{
    internal static class GameControlHelper
    {
        internal static void CheckUserInputAndUpdateDirection(ConsoleKey key, PacmanPlayer pacman)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    EnqueueItem(pacman, DirectionEnum.Up);
                    break;
                case ConsoleKey.DownArrow:
                    EnqueueItem(pacman, DirectionEnum.Down);
                    break;
                case ConsoleKey.LeftArrow:
                    EnqueueItem(pacman, DirectionEnum.Left);
                    break;
                case ConsoleKey.RightArrow:
                    EnqueueItem(pacman, DirectionEnum.Right);
                    break;
                default:
                    // Ignore other keys
                    break;
            }
        }

        public static DirectionEnum? GetNextDirectionInQueue(PacmanPlayer player)
        {
            if (player.DesiredDirectionsQueue.Count == 0) {
                return null;
            }

            var desiredDirection = player.DesiredDirectionsQueue.Dequeue();
            return (DirectionEnum)desiredDirection;
        }

        private static void EnqueueItem(PacmanPlayer pacman, DirectionEnum direction)
        {
            pacman.DesiredDirectionsQueue.Enqueue(direction);
        }
    }
}
