using System.Runtime.InteropServices;

namespace Utility.Utility
{
    public static class ArrayUtility
    {
        public static T[] GetRow<T>(T[,] array, int row)
        {
            if (!typeof(T).IsPrimitive)
                throw new InvalidOperationException("Not supported for managed types.");

            if (array == null)
                throw new ArgumentNullException("array");

            int cols = array.GetUpperBound(1) + 1;
            T[] result = new T[cols];

            int size;

            if (typeof(T) == typeof(bool))
                size = 1;
            else if (typeof(T) == typeof(char))
                size = 2;
            else
                size = Marshal.SizeOf<T>();

            Buffer.BlockCopy(array, row * cols * size, result, 0, cols * size);

            return result;
        }

        public static char[,] ReadStringIntoCharArray(string inputGrid, int width, int height)
        {
            var array = new char[width, height];
            int currentIndex = 0;

            foreach (var x in Enumerable.Range(0, width))
            {
                foreach (var y in Enumerable.Range(0, height))
                {
                    char currentValue = inputGrid[currentIndex];
                    array[x, y] = currentValue;
                    currentIndex++;
                }
            }
            return array;
        }
    }
}
