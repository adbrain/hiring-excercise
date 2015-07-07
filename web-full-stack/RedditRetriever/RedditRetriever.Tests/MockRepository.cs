using NUnit.Framework;
using RedditRetriever.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditRetriever.Tests
{
    public class MockRepository : IRedditRepository
    {
        private List<Models.Post> _posts = new List<Models.Post>();

        public MockRepository()
        {
        }

        public async Task SavePostsAsync(IEnumerable<Models.Post> posts)
        {
            _posts.AddRange(posts);
        }


        public IEnumerable<Models.Post> GetPosts(string callId)
        {
            return _posts.Where(x => x.CallId == callId);
        }
    }
}
