using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditRetriever.Models
{
    public class UserPostsModel
    {
        public string Author { get; set; }
        public IEnumerable<PostDto> Posts { get; set; }
    }
}