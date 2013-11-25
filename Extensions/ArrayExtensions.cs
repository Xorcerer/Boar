using System;

namespace LoganZhou.Boar
{
    public static class ArrayExtensions
    {
        public static int FindIndex<T>(this T[] array, Func<T, bool> predicator)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (predicator(array[i]))
                    return i;
            }
            return -1;
        }

        public static void InplaceMap<T>(this T[] array, Func<T, T> func)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = func(array[i]);
            }
        }

        /// <summary>
        /// Find a slot which is null, and set value to it.
        /// </summary>
        /// <param name="array">Array.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">Array type, should be reference type.</typeparam>
        /// <exception cref="ArgumentOutOfRangeException">Throw if null slot not found.</exception>
        public static void InsertToEmptySlot<T>(this T[] array, T value)
            where T : class
        {
            int nullIndex = array.FindIndex(e => e == default(T));
            array[nullIndex] = value;
        }

        #region InsertToZeroSlot

        /// <summary>
        /// Find a slot which is 0, and set value to it.
        /// </summary>
        /// <param name="array">Array.</param>
        /// <param name="value">Value.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw if 0 slot not found.</exception>
        public static void InsertToZeroSlot(this int[] array, int value)
        {
            int zeroIndex = array.FindIndex(e => e == 0);
            array[zeroIndex] = value;
        }

        public static void InsertToZeroSlot(this long[] array, long value)
        {
            int zeroIndex = array.FindIndex(e => e == 0);
            array[zeroIndex] = value;
        }

        public static void InsertToZeroSlot(this short[] array, short value)
        {
            int zeroIndex = array.FindIndex(e => e == 0);
            array[zeroIndex] = value;
        }
      
        public static void InsertToZeroSlot(this uint[] array, uint value)
        {
            int zeroIndex = array.FindIndex(e => e == 0);
            array[zeroIndex] = value;
        }

        public static void InsertToZeroSlot(this ulong[] array, ulong value)
        {
            int zeroIndex = array.FindIndex(e => e == 0);
            array[zeroIndex] = value;
        }

        public static void InsertToZeroSlot(this ushort[] array, ushort value)
        {
            int zeroIndex = array.FindIndex(e => e == 0);
            array[zeroIndex] = value;
        }

        public static void InsertToZeroSlot(this byte[] array, byte value)
        {
            int zeroIndex = array.FindIndex(e => e == 0);
            array[zeroIndex] = value;
        }

        public static void InsertToZeroSlot(this sbyte[] array, sbyte value)
        {
            int zeroIndex = array.FindIndex(e => e == 0);
            array[zeroIndex] = value;
        }

        #endregion
    }
}

