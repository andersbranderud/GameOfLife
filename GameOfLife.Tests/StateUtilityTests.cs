using GameOfLife.Model;

namespace GameOfLife.Tests
{
    public class StateUtilityTests
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
            var newState = StateUtility.GenerateNewGameOfLifeState(state);
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
            var newState = StateUtility.GenerateNewGameOfLifeState(state);
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
            var newState = StateUtility.GenerateNewGameOfLifeState(state);
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
            var newState = StateUtility.GenerateNewGameOfLifeState(state);
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
            var newState = StateUtility.GenerateNewGameOfLifeState(state);
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
            var newState = StateUtility.GenerateNewGameOfLifeState(state);
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
            var newState = StateUtility.GenerateNewGameOfLifeState(state);

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

            var generation1 = StateUtility.GenerateNewGameOfLifeState(initialState);
            var generation2 = StateUtility.GenerateNewGameOfLifeState(generation1);
            var generation3 = StateUtility.GenerateNewGameOfLifeState(generation2);
            var generation4 = StateUtility.GenerateNewGameOfLifeState(generation3);
            var generation5 = StateUtility.GenerateNewGameOfLifeState(generation4);
            var generation6 = StateUtility.GenerateNewGameOfLifeState(generation5);
            var generation7 = StateUtility.GenerateNewGameOfLifeState(generation6);
            var generation8 = StateUtility.GenerateNewGameOfLifeState(generation7);
            var generation9 = StateUtility.GenerateNewGameOfLifeState(generation8);
            var generation10 = StateUtility.GenerateNewGameOfLifeState(generation9);

            int[,] expectedGeneration1 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 1, 0, 1, 0, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0 }
            };
            int[,] expectedGeneration2 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 1, 0, 1, 0, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 0 }
            };
            int[,] expectedGeneration3 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 0 }
            };
            int[,] expectedGeneration4 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0 }
            };
            int[,] expectedGeneration5 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 1, 0, 1, 0 },
                { 0, 0, 1, 1, 0 },
                { 0, 0, 1, 0, 0 }
            };
            int[,] expectedGeneration6 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 1, 0, 1, 0 },
                { 0, 0, 1, 1, 0 }
            };
            int[,] expectedGeneration7 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 1, 1 },
                { 0, 0, 1, 1, 0 }
            };
            int[,] expectedGeneration8 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 0, 0, 1 },
                { 0, 0, 1, 1, 1 }
            };
            int[,] expectedGeneration9 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 1 },
                { 0, 0, 0, 1, 1 }
            };
            int[,] expectedGeneration10 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 1 },
                { 0, 0, 0, 1, 1 }
            };
            Assert.Equal(expectedGeneration1, generation1);
            Assert.Equal(expectedGeneration2, generation2);
            Assert.Equal(expectedGeneration3, generation3);
            Assert.Equal(expectedGeneration4, generation4);
            Assert.Equal(expectedGeneration5, generation5);
            Assert.Equal(expectedGeneration6, generation6);
            Assert.Equal(expectedGeneration7, generation7);
            Assert.Equal(expectedGeneration8, generation8);
            Assert.Equal(expectedGeneration9, generation9);
            Assert.Equal(expectedGeneration10, generation10);
        }

        [Fact]
        public void GenerateWorldBasedOnPattern_WithGliderPattern_StabilizesAfter11Generations()
        {
            int[,] result = MapUtility.GenerateWorldBasedOnPattern("glider");

            var generation1 = StateUtility.GenerateNewGameOfLifeState(result); // Generation 1
            var generation2 = StateUtility.GenerateNewGameOfLifeState(generation1); // Generation 2
            var generation3 = StateUtility.GenerateNewGameOfLifeState(generation2); // Generation 3
            var generation4 = StateUtility.GenerateNewGameOfLifeState(generation3); // Generation 4
            var generation5 = StateUtility.GenerateNewGameOfLifeState(generation4); // Generation 5
            var generation6 = StateUtility.GenerateNewGameOfLifeState(generation5); // Generation 6
            var generation7 = StateUtility.GenerateNewGameOfLifeState(generation6); // Generation 7
            var generation8 = StateUtility.GenerateNewGameOfLifeState(generation7); // Generation 8
            var generation9 = StateUtility.GenerateNewGameOfLifeState(generation8); // Generation 9
            var generation10 = StateUtility.GenerateNewGameOfLifeState(generation9); // Generation 10
            var generation11 = StateUtility.GenerateNewGameOfLifeState(generation10); // Generation 11
            var generation12 = StateUtility.GenerateNewGameOfLifeState(generation11); // Generation 12

            int[,] stabilizedExpectedGeneration = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 1 },
                { 0, 0, 0, 1, 1 }
            };

            Assert.Equal(generation11, generation12);
            Assert.Equal(stabilizedExpectedGeneration, generation11);
        }
    }
}