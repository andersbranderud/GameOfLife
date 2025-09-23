using System;
using Xunit;
using GameOfLife.Model;

namespace GameOfLife.Tests
{
    public class StateHelperTests
    {
        [Fact]
        public void GenerateNewGameOfLifeState_AliveCellWithFewerThanTwoAliveNeighbours_Dies()
        {
            int[,] state = new int[,]
            {
                { 0, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 0 }
            };
            var newState = StateHelper.GenerateNewGameOfLifeState(state);
            Assert.Equal(0, newState[1, 1]);
        }

        [Fact]
        public void GenerateNewGameOfLifeState_AliveCellWithTwoOrThreeAliveNeighbours_Lives()
        {
            int[,] state = new int[,]
            {
                { 1, 1, 0 },
                { 0, 1, 0 },
                { 0, 0, 0 }
            };
            var newState = StateHelper.GenerateNewGameOfLifeState(state);
            Assert.Equal(1, newState[0, 1]); // cell with two alive neighbours
            Assert.Equal(1, newState[1, 1]); // cell with two alive neighbours
        }

        [Fact]
        public void GenerateNewGameOfLifeState_AliveCellWithMoreThanThreeAliveNeighbours_Dies()
        {
            int[,] state = new int[,]
            {
                { 1, 1, 1 },
                { 1, 1, 0 },
                { 0, 0, 0 }
            };
            var newState = StateHelper.GenerateNewGameOfLifeState(state);
            Assert.Equal(0, newState[1, 1]);
        }

        [Fact]
        public void GenerateNewGameOfLifeState_DeadCellWithExactlyThreeAliveNeighbours_BecomesAlive()
        {
            int[,] state = new int[,]
            {
                { 1, 1, 0 },
                { 0, 0, 0 },
                { 0, 1, 0 }
            };
            var newState = StateHelper.GenerateNewGameOfLifeState(state);
            Assert.Equal(1, newState[1, 1]);
            Assert.Equal(1, newState[1, 0]);
        }

        [Fact]
        public void GenerateNewGameOfLifeState_DeadCellWithNotThreeAliveNeighbours_StaysDead()
        {
            int[,] state = new int[,]
            {
                { 1, 0, 0 },
                { 0, 0, 0 },
                { 0, 1, 0 }
            };
            var newState = StateHelper.GenerateNewGameOfLifeState(state);
            Assert.Equal(0, newState[1, 1]);
            Assert.Equal(0, newState[1, 0]);
            Assert.Equal(0, newState[0, 1]);
            Assert.Equal(0, newState[2, 1]);
            Assert.Equal(0, newState[1, 2]);
            Assert.Equal(0, newState[1, 0]);
        }

        [Fact]
        public void GenerateNewGameOfLifeState_EdgeCellsHandledCorrectly()
        {
            int[,] state = new int[,]
            {
                { 1, 1 },
                { 0, 1 }
            };
            var newState = StateHelper.GenerateNewGameOfLifeState(state);
            Assert.Equal(1, newState[0, 0]);
            Assert.Equal(1, newState[0, 1]);
            Assert.Equal(1, newState[1, 1]);
            Assert.Equal(1, newState[1, 0]);
        }

        [Fact]
        public void GenerateNewGameOfLifeState_ComplexPattern_ProducesExpectedResult()
        {
            int[,] state = new int[,]
            {
                { 0, 1, 0 },
                { 0, 0, 1 },
                { 1, 1, 1 },
                { 0, 0, 0 }
            };
            var newState = StateHelper.GenerateNewGameOfLifeState(state);

            int[,] expectedState = new int[,]
            {
                { 0, 0, 0 },
                { 1, 0, 1 },
                { 0, 1, 1 },
                { 0, 1, 0 }
            };
            Assert.Equal(expectedState, newState);
        }

        [Fact]
        public void GenerateNewGameOfLifeState_WithGliderPattern_VerifyProgression()
        {
            var initialState = new int[,]
            {
                { 0, 1, 0, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 1, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 }
            };

            // Output after generation 1
            /*   0 0 0 0 0
   1 0 1 0 0
   0 1 1 0 0
   0 1 0 0 0
   0 0 0 0 0*/
            var expectedStateAfter1 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 1, 0, 1, 0, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0 }
            };
            var newState = StateHelper.GenerateNewGameOfLifeState(initialState);
            Assert.Equal(expectedStateAfter1, newState);

            // Output after generation 1
            var expectedStateAfterGeneration1 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 0 }
            };
            newState = StateHelper.GenerateNewGameOfLifeState(newState);
            Assert.Equal(expectedStateAfterGeneration1, newState);

   
            var expectedStateAfterGeneration2 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 0 }
            };
            newState = StateHelper.GenerateNewGameOfLifeState(newState);
            Assert.Equal(expectedStateAfterGeneration2, newState);

        }

    }
}   