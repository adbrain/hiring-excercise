using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adbrain.WebFullStack.DataAccessLayer;
using Adbrain.WebFullStack.Models;

namespace WebFullStack.Tests
{
    public class PostRepositoryMocked : IRepository<Post>
    {
        private readonly List<Post> mPost;

        public PostRepositoryMocked()
        {
            mPost = new List<Post>();
        }

        public async Task<int> SaveAll(IEnumerable<Post> posts)
        {
            await Task.Run(() => mPost.AddRange(posts));
            return posts.Count();
        }

        public IList<Post> FromBatchNumber(string batchNumber)
        {
            return mPost.Where(x => x.BatchNumber == batchNumber).ToList();
        }
    }
}
