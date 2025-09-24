using Pacman.Model.Enums;
using Pacman.Model.Models;
using System.Collections;

namespace Pacman.Model.Tests
{
    public class GameControlHelperTests
    {
        private PacmanPlayer CreatePacmanPlayer()
        {
            var player = new PacmanPlayer(0, 0);
            player.DesiredDirectionsQueue = new Queue();
            return player;
        }

        [Theory]
        [InlineData(ConsoleKey.UpArrow, DirectionEnum.Up)]
        [InlineData(ConsoleKey.DownArrow, DirectionEnum.Down)]
        [InlineData(ConsoleKey.LeftArrow, DirectionEnum.Left)]
        [InlineData(ConsoleKey.RightArrow, DirectionEnum.Right)]
        public void CheckUserInputAndUpdateDirection_EnqueuesCorrectDirection(ConsoleKey key, DirectionEnum expected)
        {
            var pacman = CreatePacmanPlayer();

            GameControlHelper.CheckUserInputAndUpdateDirection(key, pacman);

            Assert.Single(pacman.DesiredDirectionsQueue);
            Assert.Equal(expected, pacman.DesiredDirectionsQueue.Peek());
        }

        [Fact]
        public void CheckUserInputAndUpdateDirection_IgnoresNonArrowKeys()
        {
            var pacman = CreatePacmanPlayer();

            GameControlHelper.CheckUserInputAndUpdateDirection(ConsoleKey.Spacebar, pacman);

            Assert.Empty(pacman.DesiredDirectionsQueue);
        }

        [Fact]
        public void GetNextDirectionInQueue_ReturnsNullIfQueueEmpty()
        {
            var pacman = CreatePacmanPlayer();

            var result = GameControlHelper.GetNextDirectionInQueue(pacman);

            Assert.Null(result);
        }

        [Fact]
        public void GetNextDirectionInQueue_ReturnsAndDequeuesDirection()
        {
            var pacman = CreatePacmanPlayer();
            pacman.DesiredDirectionsQueue.Enqueue(DirectionEnum.Left);
            pacman.DesiredDirectionsQueue.Enqueue(DirectionEnum.Right);

            var first = GameControlHelper.GetNextDirectionInQueue(pacman);
            var second = GameControlHelper.GetNextDirectionInQueue(pacman);

            Assert.Equal(DirectionEnum.Left, first);
            Assert.Equal(DirectionEnum.Right, second);
            Assert.Empty(pacman.DesiredDirectionsQueue);
        }
    }
}           