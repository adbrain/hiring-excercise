using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using Adbrain.WebFullStack.Models;

namespace Adbrain.WebFullStack.DataAccessLayer
{
    public class PostRepository : IRepository<Post>
    {
        public async Task<int> SaveAll(IEnumerable<Post> posts)
        {
            using (var ctx = new Context())
            {
                ctx.Posts.AddRange(posts);
                return await ctx.SaveChangesAsync();
            }
        }

        public IList<Post> FromBatchNumber(string batchNumber)
        {
            using (var ctx = new Context())
            {
                return ctx.Posts.Where(x => x.BatchNumber == batchNumber).ToList();
            }
        }
    }
}