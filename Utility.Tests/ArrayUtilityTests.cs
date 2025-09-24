using System;
using Xunit;
using GameOfLife.Model.Utility;

namespace GameOfLife.Tests
{
    public class ArrayUtilityTests
    {
        [Fact]
        public void GetRow_WithValidIntArray_ReturnsCorrectRow()
        {
            int[,] array = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var row = ArrayUtility.GetRow(array, 1);
            Assert.Equal(new int[] { 4, 5, 6 }, row);
        }

        [Fact]
        public void GetRow_WithValidBoolArray_ReturnsCorrectRow()
        {
            bool[,] array = new bool[,] { { true, false }, { false, true } };
            var row = ArrayUtility.GetRow(array, 0);
            Assert.Equal(new bool[] { true, false }, row);
        }

        [Fact]
        public void GetRow_WithValidCharArray_ReturnsCorrectRow()
        {
            char[,] array = new char[,] { { 'a', 'b' }, { 'c', 'd' } };
            var row = ArrayUtility.GetRow(array, 1);
            Assert.Equal(new char[] { 'c', 'd' }, row);
        }

        [Fact]
        public void GetRow_WithManagedType_ThrowsInvalidOperationException()
        {
            string[,] array = new string[,] { { "a", "b" }, { "c", "d" } };
            Assert.Throws<InvalidOperationException>(() => ArrayUtility.GetRow(array, 0));
        }

        [Fact]
        public void GetRow_WithNullArray_ThrowsArgumentNullException()
        {
            int[,] array = null;
            Assert.Throws<ArgumentNullException>(() => ArrayUtility.GetRow(array, 0));
        }
    }
}
