using System;

namespace LoganZhou.Boar
{
    public static class DateTimeExtensions
    {
        static readonly DateTime s_unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);

        public static int ToUnixTime(this DateTime datetime)
        {
            return (int)(datetime - s_unixEpoch).TotalSeconds;
        }

        public static int ToUnixTime(this TimeSpan timeSpan)
        {
            return (int)timeSpan.TotalSeconds;
        }

        public static DateTime ToDateTime(this int unixTime)
        {
            return s_unixEpoch + TimeSpan.FromSeconds(unixTime);
        }

        public static TimeSpan ToTimeSpan(this int unixTimeSpan)
        {
            return TimeSpan.FromSeconds(unixTimeSpan);
        }

        public static TimeSpan Multiply(this TimeSpan timeSpan, float multiplier)
        {
            return TimeSpan.FromMilliseconds(timeSpan.TotalMilliseconds * multiplier);
        }
    }
}

