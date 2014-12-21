using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.WebApi.Models.Helpers
{
    public interface IDateTimeHelper
    {
        DateTime DateTimeFromSecsUtc(long secs);

        DateTime PosixTime { get;  }
    }
}
