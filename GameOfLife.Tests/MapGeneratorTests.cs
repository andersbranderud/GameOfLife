using GameOfLife.Model;
using Xunit;
using System;

namespace GameOfLife.Tests
{
    public class MapGeneratorTests
    {
        [Fact]
        public void GenerateWorld_WithValidDimensions_ReturnsCorrectSizeArray()
        {
            int width = 5;
            int height = 3;
            var result = MapGenerator.GenerateWorld(width, height);

            Assert.Equal(width, result.GetLength(0));
            Assert.Equal(height, result.GetLength(1));
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(5, 0)]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        public void GenerateWorld_WithInvalidDimensions_ThrowsArgumentException(int width, int height)
        {
            Assert.Throws<ArgumentException>(() => MapGenerator.GenerateWorld(width, height));
        }

        [Fact]
        public void GenerateWorld_WithPattern_ReturnsCorrectArray()
        {
            int width = 2;
            int height = 2;
            string pattern = "0110";
            var result = MapGenerator.GenerateWorld(pattern, width, height);

            Assert.Equal(width, result.GetLength(0));
            Assert.Equal(height, result.GetLength(1));
            Assert.Equal('0', result[0, 0]);
            Assert.Equal('1', result[0, 1]);
            Assert.Equal('1', result[1, 0]);
            Assert.Equal('0', result[1, 1]);
        }

        [Fact]
        public void GenerateWorld_WithPatternLengthMismatch_ThrowsArgumentException()
        {
            int width = 2;
            int height = 2;
            string pattern = "01";
            Assert.Throws<ArgumentException>(() => MapGenerator.GenerateWorld(pattern, width, height));
        }

        [Fact]
        public void GenerateWorld_WithInvalidPatternCharacters_ThrowsArgumentException()
        {
            int width = 2;
            int height = 2;
            string pattern = "01a0";
            Assert.Throws<ArgumentException>(() => MapGenerator.GenerateWorld(pattern, width, height));
        }

        [Theory]
        [InlineData(0, 2, "01")]
        [InlineData(2, 0, "01")]
        [InlineData(-1, 2, "01")]
        [InlineData(2, -1, "01")]
        public void GenerateWorld_WithPatternAndInvalidDimensions_ThrowsArgumentException(int width, int height, string pattern)
        {
            string validPattern = new string('0', Math.Max(1, width * height));
            Assert.Throws<ArgumentException>(() => MapGenerator.GenerateWorld(validPattern, width, height));
        }

        //Test for GenerateWorldBasedOnPattern
        [Fact]
        public void GenerateWorldBasedOnPattern_WithGliderPattern_ReturnsCorrectArray()
        {
            int[,] result = MapGenerator.GenerateWorldBasedOnPattern("glider");
            Assert.Equal(5, result.GetLength(0));
            Assert.Equal(5, result.GetLength(1));
        
            VerifyGrid(result, new int[,]
            {
                {'0','1','0','0','0'},
                {'0','0','1','0','0'},
                {'1','1','1','0','0'},
                {'0','0','0','0','0'},
                {'0','0','0','0','0'}
            });
        }

        private void VerifyGrid(int[,] result, int[,] expected)
        {
            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.Equal(expected[i, j], result[i, j]);
                }
            }

        }
    }
}