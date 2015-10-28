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

            if (dayDiff == 0)
            {
                if (secDiff < 60)
                {
                    return "just now";
                }
                else if (secDiff < 120)
                {
                    return "1 minute ago";
                }
                else if (secDiff < 3600)
                {
                    return string.Format("{0} minutes ago",
                        Math.Floor((double)secDiff / 60));
                }
                else if (secDiff < 7200)
                {
                    return "1 hour ago";
                }
                else
                {
                    return string.Format("{0} hours ago",
                        Math.Floor((double)secDiff / 3600));
                }
            }
            else if (dayDiff == 1)
            {
                return "yesterday";
            }
            else if (dayDiff < 7)
            {
                return string.Format("{0} days ago",
                dayDiff);
            }
            else
            {
                return string.Format("{0} weeks ago",
                Math.Ceiling((double)dayDiff / 7));
            }
        }

    }
}
