namespace Pacman.Model.Tests
{
    public class GameTests
    {
        [Fact]
        public void Constructor_Default_DoesNotThrow()
        {
            var game = new Game();
            Assert.NotNull(game);
        }

        [Fact]
        public void Constructor_WithNullWorld_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Game(null));
        }

        [Fact]
        public void Constructor_WithEmptyWorld_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Game(string.Empty));
        }

        [Fact]
        public void Constructor_WithValidWorld_InitializesMap()
        {
            // Arrange
            string world = new string('.', 24) + 'P'; // 5 rows, 5 columns

            var game = new Game(world);

            // Act
            game.InitGame();

            // Assert
            // If no exception, map and pacman should be initialized
            // (Cannot access private fields, but Render should not throw)
            var ex = Record.Exception(() => game.Render());
            Assert.Null(ex);
        }

        [Fact]
        public void InitGame_InitializesMapAndPacman()
        {
            var game = new Game();
            game.InitGame();

            // Render should not throw after InitGame
            var ex = Record.Exception(() => game.Render());
            Assert.Null(ex);
        }

        [Fact]
        public void Render_WithoutInitGame_ThrowsInvalidOperationException()
        {
            var game = new Game();
            Assert.Throws<InvalidOperationException>(() => game.Render());
        }

        [Fact]
        public void CheckUserInputAndUpdateDirection_DoesNotThrow()
        {
            var game = new Game(new string('.', 25));
            game.InitGame();

            var ex = Record.Exception(() => game.CheckUserInputAndUpdateDirection(ConsoleKey.LeftArrow));
            Assert.Null(ex);
        }
    }
}   