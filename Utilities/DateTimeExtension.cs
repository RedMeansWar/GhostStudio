using System;

namespace GhostStudio.Utilities
{
    public static class DateTimeExtension
    {
        public static string TimeElapsed(this DateTime d1, DateTime date2)
        {
            TimeSpan timeSpan = d1 > date2 ? (d1 - date2) : (date2 - d1);
            string[] text = ["Year", "Month", "Day", "Hour", "Minute", "Second"];

            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0: break;
                    case 1: break;
                    case 2: break;
                    case 3: break;
                    case 4: break;
                    case 5: break;
                }
            }

            return string.Format("{0} Year(s), {1} Month(s), {2} Day(s), {3} Hour(s), {4} Minute(s), {5} Second(s)", Math.Truncate(timeSpan.TotalDays / 365), Math.Truncate(timeSpan.TotalDays % 365) / 30, Math.Truncate(timeSpan.TotalDays % 365) % 30, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}
