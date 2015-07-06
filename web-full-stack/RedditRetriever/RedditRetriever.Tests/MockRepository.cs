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
        private string _commonDomain;
        private int _elementsLength;

        public MockRepository(string commonDomain, int elementsLength)
        {
            _elementsLength = elementsLength;
            _commonDomain = commonDomain;
        }

        public async Task SavePostsAsync(IEnumerable<Models.Post> posts)
        {
            if(_commonDomain != null)
            {
                var commonDomain = posts.All(x => x.Domain == _commonDomain);
                var domainNotNull = posts.All(x => x.Domain != null);
                Assert.IsTrue(commonDomain);
                Assert.IsTrue(domainNotNull);
            }
            else
            {
                Assert.AreEqual(_elementsLength, posts.Count());
            }
        }
    }
}
