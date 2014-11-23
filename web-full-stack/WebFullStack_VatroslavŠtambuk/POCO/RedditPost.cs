using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCO
{
    public class RedditPost
    {
        public int RequestId { get; set; }
        public string id { get; set; }
        public string author { get; set; }
        public DateTime createdDate { get; set; }
        public string title { get; set; }
        public string permalink { get; set; }
        public string domain { get; set; }
    }
}
