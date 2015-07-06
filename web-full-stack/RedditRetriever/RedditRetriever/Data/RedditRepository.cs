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
        public async Task SavePostsAsync(IEnumerable<PostJsonModel> posts)
        {
            using(var ctx = new RedditDBDataContext())
            {
                ctx.RedditPostsJson.AddRange(posts);
                await ctx.SaveChangesAsync();
            }
        }
    }
}