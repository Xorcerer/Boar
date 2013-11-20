using System;
using System.Collections.Generic;
using System.Linq;

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
		/// ref: http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
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
				int j = random.Next(i); // Yes, maybe i == j.
				T temp = array[i];
				array[i] = array[j];
				array[j] = temp;
			}
		}

		/// <summary>
		/// Shuffle the specified list INPLACE.
		/// Algo by Donald E. Knuth.
		/// ref: http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
		/// </summary>
		/// <param name="random">Random.</param>
		/// <param name="list">List.</param>
		/// <typeparam name="T">The type of IList.</typeparam>
		public static void Shuffle<T>(this Random random, IList<T> list)
		{
			if (list == null)
				throw new ArgumentNullException("list");

			for (int i = list.Count - 1; i > 0; i--)
			{
				int j = random.Next(i); // Yes, maybe i == j.
				T temp = list[i];
				list[i] = list[j];
				list[j] = temp;
			}
		}

		public static IEnumerable<T> PickN<T>(this Random random, T[] array, int n)
		{
			if (array == null)
				throw new ArgumentNullException("array");

			if (n > array.Length)
				throw new ArgumentOutOfRangeException("n");

			const int empiricFactor = 5;
			if (n < array.Length * empiricFactor)
			{
				return PickNForSmallSet<T>(random, array, n);
			}

			return PickNForBigSet(random, array, n);
		}

		static IEnumerable<T> PickNForSmallSet<T>(Random random, T[] array, int n)
		{
			int nToSelect = n;
			for (int i = 0; i < array.Length; i++)
			{
				if (nToSelect > random.Next(1, array.Length - i))
				{
					yield return array[i];
					nToSelect--;
				}
			}
		}

		static IEnumerable<T> PickNForBigSet<T>(Random random, T[] array, int n)
		{
			var indexes = new HashSet<int>();
			while (indexes.Count < n)
				indexes.Add(random.Next(array.Length - 1));

			return indexes.Select(i => array[i]);
		}

		public static IEnumerable<T> PickN<T>(this Random random, IList<T> list, int n)
		{
			if (list == null)
				throw new ArgumentNullException("list");

			if (n > list.Count)
				throw new ArgumentOutOfRangeException("n");

			const int empiricFactor = 5;
			if (n < list.Count * empiricFactor)
			{
				return PickNForSmallSet<T>(random, list, n);
			}

			return PickNForBigSet(random, list, n);
		}

		static IEnumerable<T> PickNForSmallSet<T>(Random random, IList<T> list, int n)
		{
			int nToSelect = n;
			for (int i = 0; i < list.Count; i++)
			{
				if (nToSelect > random.Next(1, list.Count - i))
				{
					yield return list[i];
					nToSelect--;
				}
			}
		}

		static IEnumerable<T> PickNForBigSet<T>(Random random, IList<T> list, int n)
		{
			var indexes = new HashSet<int>();
			while (indexes.Count < n)
				indexes.Add(random.Next(list.Count - 1));

			return indexes.Select(i => list[i]);
		}

	}
}

