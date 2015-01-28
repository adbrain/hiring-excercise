using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adbrain.Reddit.WebApi.Models.Helpers
{
    public class DateTimeHelper : IDateTimeHelper
    {
        private readonly DateTime _posixTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);

        public DateTime DateTimeFromSecsUtc(long secs)
        {
            var time = _posixTime.AddSeconds(secs);
            return time;
        }

        public DateTime PosixTime { get { return _posixTime; } }
    }
}