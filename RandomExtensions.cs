using System;
using System.Collections.Generic;

namespace LoganZhou.Boar
{
    public static class RandomExtensions
    {
        public static T Pick<T>(this Random random, IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");
                
            return random.Pick(list, 0, list.Count);
        }

        public static T Pick<T>(this Random random, IList<T> list, int offset, int length)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            int maxOffset = offset + length - 1;
            if (maxOffset >= list.Count)
                throw new ArgumentOutOfRangeException("length");

            return list[random.Next(offset, maxOffset)];
        }

        public static T Pick<T>(this Random random, T[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            return random.Pick(array, 0, array.Length);
        }

        public static T Pick<T>(this Random random, T[] array, int offset, int length)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            int maxOffset = offset + length - 1;
            if (maxOffset >= array.Length)
                throw new ArgumentOutOfRangeException("length");
            return array[random.Next(offset, maxOffset)];
        }

		/// <summary>
		/// Shuffle the specified array INPLACE.
		/// Algo by Donald E. Knuth.
		/// </summary>
		/// <param name="random">Random.</param>
		/// <param name="array">Array.</param>
		/// <typeparam name="T">The type of Array.</typeparam>
		public static void Shuffle<T>(this Random random, T[] array)
		{
			if (array == null)
				throw new ArgumentNullException("array");

			for (int i = array.Length - 1; i > 0; i--)
			{
				int j = random.Next(i);
				T temp = array[i];
				array[i] = array[j];
				array[j] = temp;
			}
		}
   }
}

