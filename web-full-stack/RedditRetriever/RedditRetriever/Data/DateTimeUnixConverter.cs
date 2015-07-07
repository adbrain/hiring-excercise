using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditRetriever.Data
{
    public static class DateTimeUnixConverter
    {
        public static DateTime FromUnixTimestamp(double timeStamp)
        {
            return new DateTime(1970, 1, 1).AddSeconds(timeStamp);
        }
    }
}