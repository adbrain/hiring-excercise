using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.DataAccess.Entities
{
    public class RawData
    {
        public virtual int Id { get; set; }

        public virtual string RedditResponse { get; set; }

        public virtual DateTime SavedOn { get; set; }
    }
}
