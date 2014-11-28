using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devTest_Web.Utils
{
    public class DateTimeUtil
    {
        public static DateTime FromUnixTime(long unxTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return epoch.AddSeconds(unxTime).ToLocalTime();
        }
    }
}