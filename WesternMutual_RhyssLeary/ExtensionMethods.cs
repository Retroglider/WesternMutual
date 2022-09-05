namespace WesternMutual_RhyssLeary;

public static class DateExtensionMethods
{
    /// <summary>
    /// Convert from Unix epoch date-time to DateTime.
    /// </summary>
    /// <param name="epochDateTime"></param>
    /// <returns></returns>
    public static DateTime ToDate(this long epochDateTime)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0); //from start epoch time
        dateTime = dateTime.AddSeconds(epochDateTime); //add the seconds to the start DateTime
        return dateTime;
    }
    /// <summary>
    /// Convert from DateTime to Unix epoch date-time.
    /// </summary>
    /// <returns></returns>
    public static long ToEpochDateTime(this DateTime dateTime)
    {
        DateTimeOffset dateTimeOffSet = DateTimeOffset.Parse(dateTime.ToString());

        long date = dateTimeOffSet.ToUnixTimeMilliseconds();
        return date;
        //TimeSpan t = DateTime.Now - dateTime;
        //int secondsSinceEpoch = (int)t.TotalSeconds;
        //return secondsSinceEpoch;
    }
}
