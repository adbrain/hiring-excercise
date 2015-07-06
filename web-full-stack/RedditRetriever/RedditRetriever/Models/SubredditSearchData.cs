using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditRetriever.Models
{
    public class SubredditSearchData
    {
        public string Modhash { get; set; }
        public List<Item> Children { get; set; }
        public string After { get; set; }
        public object Before { get; set; }
    }
}