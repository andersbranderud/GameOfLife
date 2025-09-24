using Pacman.Model.Enums;
using Pacman.Model;
using Pacman.Model.Models;

namespace Pacman.Model.Tests
{
    public class MapTests
    {
        private const char Fruit = 'F';
        private const char EatenFruit = 'E';
        private const char PacmanUp = '^';
        private const char PacmanDown = 'v';
        private const char PacmanLeft = '<';
        private const char PacmanRight = '>';

        private static char[,] CreateGrid(int rows, int cols, char fill)
        {
            var grid = new char[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    grid[r, c] = fill;
                }
            }
            return grid;
        }

        private static PacmanPlayer CreatePacmanPlayer(int x, int y, DirectionEnum direction)
        {
            var player = new PacmanPlayer(x, y);
            player.SetCurrentPosition(x, y);
            player.SetLastPosition(x, y);
            player.CurrentDirection = direction;
            return player;
        }

        [Fact]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            var grid = CreateGrid(2, 2, Fruit);
            var walls = new HashSet<(int, int)> { (0, 1) };
            var map = new Map(1, 0, 0, 2, 2, grid, 4, walls);

            Assert.Equal(1, map.MapNr);
            Assert.Equal(0, map.PacmanStartX);
            Assert.Equal(0, map.PacmanStartY);
            Assert.Equal(2, map.NrOfRows);
            Assert.Equal(2, map.NrOfColumns);
            Assert.Equal(4, map.InitialDots);
            Assert.Equal(4, map.RemainingDots);
            Assert.Equal(grid, map.Grid);
            Assert.Equal(walls, map.WallPositions);
        }

        [Theory]
        [InlineData(1, 0, false)] // wall
        [InlineData(0, 0, true)]  // not wall
        public void IsValidPosition_ReturnsExpected(int x, int y, bool expected)
        {
            var grid = CreateGrid(2, 2, Fruit);
            var walls = new HashSet<(int, int)> { (0, 1) };
            var map = new Map(1, 0, 0, 2, 2, grid, 4, walls);

            Assert.Equal(expected, map.IsValidPosition(x, y));
        }

        [Fact]
        public void RedrawMapForNewPacmanPosition_EatsFruitAndUpdatesGrid()
        {
            var grid = CreateGrid(2, 2, Fruit);
            var walls = new HashSet<(int, int)>();
            var map = new Map(1, 0, 0, 2, 2, grid, 4, walls);

            var pacman = CreatePacmanPlayer(0, 0, DirectionEnum.Right);
            pacman.SetLastPosition(0, 0);
            pacman.SetCurrentPosition(1, 0);

            grid[1, 0] = Fruit;
            map.RedrawMapForNewPacmanPosition(pacman);

            Assert.Equal(EatenFruit, grid[0, 0]);
            Assert.Equal(PacmanRight, grid[0, 1]);
            Assert.Equal(3, map.RemainingDots);
        }

        [Theory]
        [InlineData(DirectionEnum.Up, PacmanUp)]
        [InlineData(DirectionEnum.Down, PacmanDown)]
        [InlineData(DirectionEnum.Left, PacmanLeft)]
        [InlineData(DirectionEnum.Right, PacmanRight)]
        [InlineData(DirectionEnum.None, PacmanRight)]
        public void RedrawMapForNewPacmanPosition_SetsCorrectPacmanSymbol(DirectionEnum direction, char expectedSymbol)
        {
            var grid = CreateGrid(2, 2, Fruit);
            var walls = new HashSet<(int, int)>();
            var map = new Map(1, 0, 0, 2, 2, grid, 4, walls);

            var pacman = CreatePacmanPlayer(1, 1, direction);
            pacman.SetLastPosition(0, 0);
            pacman.SetCurrentPosition(1, 1);

            map.RedrawMapForNewPacmanPosition(pacman);

            Assert.Equal(expectedSymbol, grid[1, 1]);
        }

        [Fact]
        public void GetNewPosition_MovesCorrectly_WhenValid()
        {
            var grid = CreateGrid(3, 3, Fruit);
            var walls = new HashSet<(int, int)>();
            var map = new Map(1, 1, 1, 3, 3, grid, 9, walls);

            var pacman = CreatePacmanPlayer(1, 1, DirectionEnum.Up);
            var pos = map.GetNewPosition(pacman);
            Assert.Equal((1, 0), pos);

            pacman.CurrentDirection = DirectionEnum.Down;
            pos = map.GetNewPosition(pacman);
            Assert.Equal((1, 2), pos);

            pacman.CurrentDirection = DirectionEnum.Left;
            pos = map.GetNewPosition(pacman);
            Assert.Equal((0, 1), pos);

            pacman.CurrentDirection = DirectionEnum.Right;
            pos = map.GetNewPosition(pacman);
            Assert.Equal((2, 1), pos);
        }

        // Edgecases where we go beyond board, and player should end up on other side.


        [Fact]
        public void GetNewPosition_DoesNotMoveIntoWall()
        {
            var grid = CreateGrid(3, 3, Fruit);
            var walls = new HashSet<(int, int)> { (0, 1) };
            var map = new Map(1, 1, 1, 3, 3, grid, 9, walls);

            var pacman = CreatePacmanPlayer(1, 1, DirectionEnum.Up);
            var pos = map.GetNewPosition(pacman);
            Assert.Equal((1, 1), pos); // Should not move into wall
        }

        [Fact]
        public void HasRemainingDots_ReturnsTrueAndFalse()
        {
            var grid = CreateGrid(2, 2, Fruit);
            var walls = new HashSet<(int, int)>();
            var map = new Map(1, 0, 0, 2, 2, grid, 1, walls);

            Assert.True(map.HasRemainingDots());
            map.RedrawMapForNewPacmanPosition(CreatePacmanPlayer(0, 0, DirectionEnum.Right));
            Assert.False(map.HasRemainingDots());
        }
        // Edgecases where we go beyond board, and player should end up on other side.

        [Theory]
        [InlineData(0, 0, DirectionEnum.Up, 0, 2)]    // Move up from top row, should wrap to bottom
        [InlineData(2, 2, DirectionEnum.Down, 2, 0)]  // Move down from bottom row, should wrap to top
        [InlineData(0, 0, DirectionEnum.Left, 2, 0)]  // Move left from leftmost column, should wrap to rightmost
        [InlineData(2, 2, DirectionEnum.Right, 0, 2)] // Move right from rightmost column, should wrap to leftmost
        public void GetNewPosition_WrapsAroundBoard(int startX, int startY, DirectionEnum direction, int expectedX, int expectedY)
        {
            var grid = CreateGrid(3, 3, Fruit);
            var walls = new HashSet<(int, int)>();
            var map = new Map(1, startX, startY, 3, 3, grid, 9, walls);

            var pacman = CreatePacmanPlayer(startX, startY, direction);
            var pos = map.GetNewPosition(pacman);

            Assert.Equal((expectedX, expectedY), pos);
        }
    }
}