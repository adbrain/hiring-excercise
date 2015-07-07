using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditRetriever.Models
{
    public class PostDto
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string PermaLink { get; set; }
    }
}