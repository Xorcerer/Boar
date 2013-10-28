using System;

namespace LoganZhou.Boar
{
    public static class StringExtensions
    {
        public static string Format(this string str, params object[] objs)
        {
            return string.Format(str, objs);
        }
    }
}

