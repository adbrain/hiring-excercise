using Adbrain.Reddit.WebApi.Models.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.UnitTests.WebApi.Models.Helpers
{
    [TestFixture]
    public class RedditJsonHelperTest
    {
        private IRedditJsonHelper _redditJsonHelper;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _redditJsonHelper = new RedditJsonHelper();
        }

        [Test]
        public void ExampleResponse_AllFieldsArePopulated()
        {
            var items = _redditJsonHelper.ExtractItems(RedditResponse.Example);

            foreach (var item in items)
            {
                Assert.IsFalse(String.IsNullOrWhiteSpace(item.Domain), "Domain is empty for id: " + item.Id);
                Assert.IsFalse(String.IsNullOrWhiteSpace(item.Author), "Author is empty for id: " + item.Id);
                Assert.IsFalse(String.IsNullOrWhiteSpace(item.Id), "Id is empty for title: " + item.Title);
                Assert.IsFalse(String.IsNullOrWhiteSpace(item.Title), "Title is empty for id: " + item.Id);
                Assert.AreEqual(2014, item.CreatedDate.Year, "Year is not the expected for id: " + item.Id);
                Assert.IsFalse(String.IsNullOrWhiteSpace(item.Permalink), "Permalink is empty for id: " + item.Id);
            }
        }

        [Test]
        public void ExampleResponse_CountYoutubeItems()
        {
            var items = _redditJsonHelper.ExtractItems(RedditResponse.Example);
            var domain = "youtube.com";
            var expected = 17;
            var count = items.Where(x => x.Domain == domain).Count();

            Assert.AreEqual(expected, count);
        }
    }
}
