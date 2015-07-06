using RedditRetriever.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditRetriever.Data
{
    public interface IRedditRepository
    {
        void SavePosts(IEnumerable<PostJsonModel> posts);
    }
}