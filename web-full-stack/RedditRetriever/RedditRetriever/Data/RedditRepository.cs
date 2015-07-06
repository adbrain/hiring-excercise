using RedditRetriever.Data.Database;
using RedditRetriever.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RedditRetriever.Data
{
    public class RedditRepository : IRedditRepository
    {
        public async Task SavePostsAsync(IEnumerable<Post> posts)
        {
            using(var ctx = new RedditDBDataContext())
            {
                ctx.Posts.AddRange(posts);
                await ctx.SaveChangesAsync();
            }
        }
    }
}