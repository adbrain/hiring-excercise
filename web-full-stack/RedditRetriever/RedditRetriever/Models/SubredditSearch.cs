using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditRetriever.Models
{
    public class SubredditSearch
    {
        public string Kind { get; set; }
        public SubredditSearchData Data { get; set; }
    }
}