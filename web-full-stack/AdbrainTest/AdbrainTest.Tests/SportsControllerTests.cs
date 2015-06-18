using System.Linq;
using AdbrainTest.Controllers;
using AdbrainTest.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;

namespace AdbrainTest.Tests
{
    [TestClass]
    public class SportsControllerTests
    {
        private SportsController _controller;

        private readonly Mock<IRedditService> _mockService = new Mock<IRedditService>();

        // mock response from reddit api
        private readonly JObject _mockData = JObject.FromObject(new
        {
            data = new
            {
                children = Enumerable.Range(0,200).Select(i =>
                    new{
                        data = new
                        {
                            domain = "test" + (i / 5),
                            title = "title",
                            author = "me" + (i / 10),
                            id=i,
                            permalink = "/r/sports/comments/2rawls/stuart_scott_has_died/",
                            created = 1420411704.0
                        }
                    })
            }
        });

        [TestInitialize]
        public void Setup()
        {
            // use mock data rather than really calling api
            // if this was a real program, would definitely want some tests that actually call api, but these would be integration not unit tests
            _mockService.Setup(s => s.GetListings(It.IsAny<string>())).Returns(_mockData);
            _mockService.Setup(s => s.SaveListings(It.IsAny<JObject>()));
            _controller = new SportsController(_mockService.Object);
        }
        [TestMethod]
        public void VerifyServiceMethodsCalled()
        {
            _controller.GetSports();
            _mockService.Verify(s => s.GetListings("sports"), Times.Once);
            _mockService.Verify(s => s.SaveListings(_mockData), Times.Once);
        }

        [TestMethod]
        public void Verify100ResultsReturned()
        {
            var results = _controller.GetSports();
            Assert.AreEqual(results.Sum(r => r.Items.Count()), 100);
        }

        [TestMethod]
        public void VerifyResultsGroupedByAuthor()
        {
            var results = _controller.GetSports();
            
            // mock data has 10 posts for each user, so should be in groups of 10
            Assert.IsTrue(results.All(g => g.Items.Count == 10));
            Assert.IsTrue(results.All(g => g.Items.All(i => i.Author == g.Author)));
        }

        [TestMethod]
        public void TestDomainFilter()
        {
            var results = _controller.GetSports("test1");

            // mock data has 5 posts per domain
            Assert.AreEqual(results.Sum(g => g.Items.Count()), 5);
            Assert.IsTrue(results.All(g => g.Items.All(i => i.Domain == "test1")));
        }
    }
}
