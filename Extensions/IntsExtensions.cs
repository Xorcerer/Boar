using System;

namespace LoganZhou.Boar
{
    public static class IntExtensions
    {
        public static bool Between(this int x, int lowerBound, int upperBound)
        {
            return lowerBound <= x && x <= upperBound;
        }

		public static bool Between(this uint x, uint lowerBound, uint upperBound)
		{
			return lowerBound <= x && x <= upperBound;
		}

		public static bool Between(this long x, long lowerBound, long upperBound)
		{
			return lowerBound <= x && x <= upperBound;
		}

		public static bool Between(this ulong x, ulong lowerBound, ulong upperBound)
		{
			return lowerBound <= x && x <= upperBound;
		}

        public static int Clamp(this int x, int lowerBound, int upperBound)
        {
            return Math.Max(lowerBound, Math.Min(x, upperBound));
        }

		public static uint Clamp(this uint x, uint lowerBound, uint upperBound)
		{
			return Math.Max(lowerBound, Math.Min(x, upperBound));
		}

		public static long Clamp(this long x, long lowerBound, long upperBound)
		{
			return Math.Max(lowerBound, Math.Min(x, upperBound));
		}

		public static ulong Clamp(this ulong x, ulong lowerBound, ulong upperBound)
		{
			return Math.Max(lowerBound, Math.Min(x, upperBound));
		}

    }
}

