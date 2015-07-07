using RedditRetriever.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RedditRetriever.Data
{
    public interface IRedditRepository
    {
        Task SavePostsAsync(IEnumerable<Post> posts);
        IEnumerable<Post> GetPosts(string callId);
    }
}