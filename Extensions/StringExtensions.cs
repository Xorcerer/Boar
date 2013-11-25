using System;

namespace LoganZhou.Boar
{
    public static class StringExtensions
    {
        public static string Format(this string str, params object[] objs)
        {
            return string.Format(str, objs);
        }

        public static T ToEnum<T>(this string str)
        {
            return (T)Enum.Parse(typeof(T), str, ignoreCase: false);
        }
    }
}

