using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Helpers
{
    public static class DateHelper
    {
        public static string GetPrettyTimeSpan(TimeSpan timespan)
        {
            var dayDiff = (int)timespan.TotalDays;
            var secDiff = (int)timespan.TotalSeconds;
            if (dayDiff < 0)
            {
                return null;
            }

            return dayDiff == 0 ? secDiff < 60 ? "just now" : secDiff < 120 ? "1 minute ago" : secDiff < 3600 ? $"{Math.Floor((double)secDiff / 60)} minutes ago" : secDiff < 7200 ? "1 hour ago" : $"{Math.Floor((double)secDiff / 3600)} hours ago" : dayDiff == 1 ? "yesterday" : dayDiff < 7 ? $"{dayDiff} days ago" : $"{Math.Ceiling((double)dayDiff / 7)} weeks ago";
        }

    }
}
