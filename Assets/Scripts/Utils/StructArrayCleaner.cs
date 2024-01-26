using System;

namespace Utils
{
    public static class StructArrayCleaner
    {
        public static void Clean<T>(T[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i] = default;
            }
        }
    }
}