using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedditRetriever.Models
{
    public class Item
    {
        public string Kind { get; set; }
        public Post data { get; set; }
    }
}
