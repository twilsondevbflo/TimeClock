using System;

namespace TimeClock
{
    public static class DateTimeConverter
    {
        public static string ConvertDateToString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd hh:mm:ss tt");
        }
        public static DateTime ConvertStringToDate(this string date)
        {
            return DateTime.Parse(date);
        }
    }
}
