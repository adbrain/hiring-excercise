using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.DataAccess.Wrappers
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}
