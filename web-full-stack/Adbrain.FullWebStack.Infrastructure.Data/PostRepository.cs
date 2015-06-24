using Adbrain.FullWebStack.Domain.Entities;
using Adbrain.FullWebStack.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EntityFramework.Extensions;

namespace Adbrain.FullWebStack.Infrastructure.Data
{
    public class PostRepository : IPostRepository, IDisposable
    {
        private readonly IDbContext _dbcontext;
        private bool _disposed;

        public PostRepository(IDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<IGrouping<string, Post>> GetGroupedByAuthor(string domain)
        {
            var results = _dbcontext.Posts
                .Where(post => post.Domain == domain)
                .GroupBy(post => post.Author);

            return results;
        }

        public void InsertAll(IEnumerable<Post> posts)
        {
            foreach (var post in posts)
            {
                _dbcontext.SetAsAdded(post);
            }
        }

        public void DeleteAll()
        {
            _dbcontext.Posts.Delete();
        }

        public int SaveChanges()
        {
            return _dbcontext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _dbcontext.Dispose();
            }
            _disposed = true;
        }
    }
}
