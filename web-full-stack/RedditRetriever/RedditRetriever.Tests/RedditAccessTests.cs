using NUnit.Framework;
using RedditRetriever.Controllers;
using RedditRetriever.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditRetriever.Tests
{
    [TestFixture]
    public class RedditAccessTests
    {
        [Test]
        public void RedditSearchUrl_EmptyDomain_RootDomainOnly()
        {
            var access = new RedditAccess();
            var expected = "/r/sports.json?limit=100";
            var actual = access.RetrieveUrlWithDomain("");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RedditSearchUrl_ValidDomain_SearchUrlWithDomain()
        {
            var access = new RedditAccess();
            var expected = "/r/sports.json?limit=100&site=imgur.com";
            var actual = access.RetrieveUrlWithDomain("imgur.com");
            Assert.AreEqual(expected, actual);
        }
    }
}
