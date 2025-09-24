namespace Pacman.Model.Tests
{
    public class MapUtilityTests
    {
        [Fact]
        public void ReadStringIntoMap_WithValidMap_ReturnsMapWithCorrectProperties()
        {
            // Arrange
            // 2x2 grid: Pacman, Fruit, Wall, Empty
            string world = $"{SymbolConstants.PacmanStartPosition}{SymbolConstants.Fruit}{SymbolConstants.Wall}{SymbolConstants.EmptySpace}";
            int rowCount = 2;
            int colCount = 2;
            int mapNr = 1;

            // Act
            var map = MapUtility.ReadStringIntoMap(world, rowCount, colCount, mapNr);

            // Assert
            Assert.Equal(mapNr, map.MapNr);
            Assert.Equal(0, map.PacmanStartX);
            Assert.Equal(0, map.PacmanStartY);
            Assert.Equal(1, map.InitialDots);
            Assert.Equal(rowCount, map.NrOfRows);
            Assert.Equal(colCount, map.NrOfColumns);
            Assert.Contains((1, 0), map.WallPositions);
            Assert.Equal(SymbolConstants.PacmanStartPosition, map.Grid[0, 0]);
            Assert.Equal(SymbolConstants.Fruit, map.Grid[0, 1]);
            Assert.Equal(SymbolConstants.Wall, map.Grid[1, 0]);
            Assert.Equal(SymbolConstants.EmptySpace, map.Grid[1, 1]);
        }

        [Theory]
        [InlineData(null, 2, 2, 1)]
        [InlineData("", 2, 2, 1)]
        public void ReadStringIntoMap_WithNullOrEmptyWorld_ThrowsArgumentException(string world, int rowCount, int colCount, int mapNr)
        {
            Assert.Throws<ArgumentException>(() => MapUtility.ReadStringIntoMap(world, rowCount, colCount, mapNr));
        }

        [Theory]
        [InlineData("....", 0, 2, 1)]
        [InlineData("....", 2, 0, 1)]
        public void ReadStringIntoMap_WithZeroRowsOrColumns_ThrowsArgumentException(string world, int rowCount, int colCount, int mapNr)
        {
            Assert.Throws<ArgumentException>(() => MapUtility.ReadStringIntoMap(world, rowCount, colCount, mapNr));
        }

        [Fact]
        public void ReadStringIntoMap_WithInvalidMapNr_ThrowsArgumentException()
        {
            string world = "....";
            Assert.Throws<ArgumentException>(() => MapUtility.ReadStringIntoMap(world, 2, 2, 0));
        }

        [Fact]
        public void ReadStringIntoMap_WithInvalidCharacters_ThrowsArgumentException()
        {
            string world = "A...";
            Assert.Throws<ArgumentException>(() => MapUtility.ReadStringIntoMap(world, 2, 2, 1));
        }

        [Fact]
        public void GetMapFromFile_WithNonExistentFile_ThrowsFileNotFoundException()
        {
            int mapNr = 9999; // unlikely to exist
            Assert.Throws<FileNotFoundException>(() => MapUtility.GetMapFromFile(mapNr));
        }

        [Fact]
        public void PrintOutMap_PrintsMapWithoutError()
        {
            // Arrange
            string world = $"{SymbolConstants.PacmanStartPosition}{SymbolConstants.Fruit}{SymbolConstants.Wall}{SymbolConstants.EmptySpace}";
            var map = MapUtility.ReadStringIntoMap(world, 2, 2, 1);

            // Act & Assert
            var ex = Record.Exception(() => MapUtility.PrintOutMap(map));
            Assert.Null(ex);
        }
    }
}   