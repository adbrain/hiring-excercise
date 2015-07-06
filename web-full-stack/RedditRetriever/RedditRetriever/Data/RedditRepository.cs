using RedditRetriever.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditRetriever.Data
{
    public class RedditRepository : IRedditRepository
    {
        public void SavePosts(IEnumerable<Post> posts)
        {
            throw new NotImplementedException();
        }
    }
}