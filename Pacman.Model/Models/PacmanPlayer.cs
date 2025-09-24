using Pacman.Model.Enums;

namespace Pacman.Model.Models
{
    internal class PacmanPlayer
    {
        private int _currentPositionX;
        private int _currentPositionY;

        private int _lastPositionX; 
        private int _lastPositionY;


        // Getters for position fields
        public int CurrentPositionX => _currentPositionX;
        public int CurrentPositionY => _currentPositionY;
        public int LastPositionX => _lastPositionX;
        public int LastPositionY => _lastPositionY;

        public DirectionEnum CurrentDirection { get; set; } = DirectionEnum.None;

        // property for desired direction, updates when a key is pressed
        public DirectionEnum DesiredDirection { get; set; } = DirectionEnum.None;
        public PacmanPlayer(int intialPositionX, int initialPositionY)
        {
            _currentPositionX = intialPositionX;
            _currentPositionY = initialPositionY;
        }

        // Last Position set x and y
        public void SetLastPosition(int lastPositionX, int lastPositionY)
        {
            _lastPositionX = lastPositionX;
            _lastPositionY = lastPositionY;
        }

        public void SetCurrentPosition(int currentPositionX, int currentPositionY)
        {
            _currentPositionX = currentPositionX;
            _currentPositionY = currentPositionY;
        }
    }
}
