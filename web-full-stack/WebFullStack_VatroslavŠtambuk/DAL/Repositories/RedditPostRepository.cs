using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POCO;
using System.Data.Entity;

namespace DAL
{

    public class RedditPostRepository : IDisposable
    {
        private readonly SportsContext context;
        private bool disposed = false;
        private int counter = 0;

        public RedditPostRepository()
        {
                this.context = new SportsContext();
        }

        public async Task AddRedditPost(RedditPost post)
        {
            post.RequestId = await context.RedditPosts.AsNoTracking().DefaultIfEmpty().MaxAsync(x => (int?)x.RequestId ?? 0) + 1 + counter++;
            context.RedditPosts.Add(post);
        }

        public async Task AddRedditPosts(IEnumerable<RedditPost> posts)
        {
            // all of the posts saved in a batch share the same request ID
            var requestId = await context.RedditPosts.AsNoTracking().DefaultIfEmpty().MaxAsync(x => (int?)x.RequestId ?? 0) + 1 + counter++;
            foreach (var post in posts) {
                post.RequestId = requestId;
            }

            context.RedditPosts.AddRange(posts);
        }

        public async Task<IEnumerable<dynamic>> Get(int requestId, string domain = "")
        {
            var posts = await (from x in context.RedditPosts
                               where x.domain.EndsWith(domain) && x.RequestId == requestId
                               group x by x.author into g
                        select new {
                            author = g.Key,
                            items = g.Select(y => new { id = y.id, 
                                                        createdDate = y.createdDate,
                                                        title = y.title,
                                                        permalink = y.permalink
                            }).OrderByDescending(t => t.createdDate)
                        })
                        .AsNoTracking()
                        .ToListAsync();
            return posts;
        }

        public async Task<int> Save()
        {
            return await context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Remove(RedditPost o)
        {
            context.RedditPosts.Remove(o);
        }
    }
}
