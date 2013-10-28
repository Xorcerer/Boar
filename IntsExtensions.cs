using System;

namespace LoganZhou.Boar
{
    public static class IntExtensions
    {
        public static bool Between(this int x, int lowerBound, int upperBound)
        {
            return lowerBound <= x && x <= upperBound;
        }

        public static int Clamp(this int x, int lowerBound, int upperBound)
        {
            return Math.Max(lowerBound, Math.Min(x, upperBound));
        }
    }
}

