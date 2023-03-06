namespace AttendanceManagement.Api.Utils
{
    public static class CommonUtils
    {
        public static long GetUtcTimestamp(DateTime dateTime)
        {
            return (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }

        public static DateTime GetDateTimeFromTimestamp(long timestamp)
        {
            DateTime dateTime = new DateTime(1970,1,1,0,0,0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(timestamp);
            return dateTime;
        }
    }
}
