using Pacman.Model.Enums;

namespace Pacman.Model.Models
{
    internal class PacmanPlayer
    {
        private int _currentPositionX;
        private int _currentPositionY;

        public DirectionEnum CurrentDirection { get; set; } = DirectionEnum.None;

        // property for desired direction, updates when a key is pressed
        public DirectionEnum DesiredDirection { get; set; } = DirectionEnum.None;
        public PacmanPlayer(int intialPositionX, int initialPositionY)
        {
            _currentPositionX = intialPositionX;
            _currentPositionY = initialPositionY;
        }

        public (int X, int Y) CurrentPosition
        {
            get => (_currentPositionX, _currentPositionY);
            set
            {
                _currentPositionX = value.X;
                _currentPositionY = value.Y;
            }
        }
    }
}
