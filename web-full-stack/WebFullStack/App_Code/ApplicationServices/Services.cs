using System;

namespace Adbrain.WebFullStack
{
    public static class Services
    {
        public static DateTime ConvertToDateTime(long unixtime)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1);
            return unixEpoch.AddSeconds(unixtime);
        }
    }
}