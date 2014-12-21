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
        private IDateTimeHelper _dateTimeHelper;
        private IRedditJsonHelper _redditJsonHelper;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _dateTimeHelper = new DateTimeHelper();
            _redditJsonHelper = new RedditJsonHelper(_dateTimeHelper);
        }

        [Test]
        public void SingleItemExampleResponse_AllFieldsArePopulatedWithTheValuesOnTheResponse()
        {
            var items = _redditJsonHelper.ExtractItems(RedditResponse.SingleItemExample);

            Assert.AreEqual(1L, items.Count(), "A single item was expected.");

            var item = items.Single();

            var expectedDate = _dateTimeHelper.DateTimeFromSecsUtc(1);

            Assert.AreEqual("domain", item.Domain, "Domain is not the expectd.");
            Assert.AreEqual("author", item.Author, "Author is not the expectd.");
            Assert.AreEqual("id", item.Id, "Id is not the expectd.");
            Assert.AreEqual("title", item.Title, "Title is not the expectd.");
            Assert.AreEqual(expectedDate.Date, item.CreatedDate.Date, "CreatedDate Date is not the expectd.");
            Assert.AreEqual(expectedDate.Hour, item.CreatedDate.Hour, "CreatedDate Hours are not the expectd.");
            Assert.AreEqual(expectedDate.Minute, item.CreatedDate.Minute, "CreatedDate Minutes are not the expectd.");
            Assert.AreEqual(expectedDate.Second, item.CreatedDate.Second, "CreatedDate Date Seconds are not the expectd.");
            Assert.AreEqual("permalink", item.Permalink, "Permalink is not the expectd.");
        }

        [Test]
        public void RealExampleResponse_AllFieldsArePopulated()
        {
            var items = _redditJsonHelper.ExtractItems(RedditResponse.RealExample);

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
        public void RealExampleResponse_CountYoutubeItems()
        {
            var items = _redditJsonHelper.ExtractItems(RedditResponse.RealExample);
            var domain = "youtube.com";
            var expected = 17;
            var count = items.Where(x => x.Domain == domain).Count();

            Assert.AreEqual(expected, count);
        }
    }
}
