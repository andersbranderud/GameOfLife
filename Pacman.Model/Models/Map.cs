namespace Pacman.Model.Models
{
    internal class Map
    {
        private readonly int _initialDots;
        private readonly int _remainingDots;
        private readonly int _mapNr;
        private readonly int _pacmanStartX;
        private readonly int _pacmanStartY;
        private readonly int _id;
        private readonly char[,] _grid;
        private readonly int _nrOfColumns;
        private readonly int _nrOfRows;

        // Getters for all private properties
        public int InitialDots => _initialDots;
        public int RemainingDots => _remainingDots;
        public int MapNr => _mapNr;
        public int PacmanStartX => _pacmanStartX;
        public int PacmanStartY => _pacmanStartY;
        public int Id => _id;
        public char[,] Grid => _grid;
        public int NrOfColumns => _nrOfColumns;
        public int NrOfRows => _nrOfRows;

        internal Map(int mapNr, int pacmanStartX, int pacmanStartY, int rowCount, int colCount, int initialDots)
        {
            _pacmanStartX = pacmanStartX;
            _pacmanStartY = pacmanStartY;
            this._nrOfRows = rowCount;
            this._nrOfColumns = colCount;
            this._initialDots = initialDots;
            this._remainingDots = initialDots;
            _mapNr = mapNr;
            _grid = new char[rowCount, colCount];
        }
    }
}
