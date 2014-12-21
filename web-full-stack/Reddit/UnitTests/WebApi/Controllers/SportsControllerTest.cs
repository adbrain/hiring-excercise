using Adbrain.Reddit.WebApi.Controllers;
using Adbrain.Reddit.WebApi.Models.Helpers;
using Adbrain.Reddit.WebApi.Models.JsonObjects;
using Adbrain.Reddit.WebApi.Models.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Adbrain.Reddit.UnitTests.WebApi.Controllers
{
    [TestFixture]
    public class SportsControllerTest
    {
        private IDateTimeHelper _dateTimeHelper = new DateTimeHelper();

        [Test]
        public async Task Get_TrimsQuotesOnDomain()
        {
            // Arrange
            var domain = "'domain'";
            var trimmed = "domain";
            string domainUsed = null;
            var mockRedditSportsService = new Mock<IRedditSportsService>();
            mockRedditSportsService
                .Setup(x => x.GetForDomainByAuthor(It.IsAny<string>()))
                .Callback<string>(x => domainUsed = x)
                .ReturnsAsync(new List<RedditAuthor>());
            
            var controller = CreateController(mockRedditSportsService.Object);

            // Act
            await controller.Get(domain);

            // Todo
            Assert.AreEqual(trimmed, domainUsed);
        }

        [Test]
        public async Task Get_TrimsDoubleQuotesOnDomain()
        {
            // Arrange
            var domain = "\"domain\"";
            var trimmed = "domain";
            string domainUsed = null;
            var mockRedditSportsService = new Mock<IRedditSportsService>();
            mockRedditSportsService
                .Setup(x => x.GetForDomainByAuthor(It.IsAny<string>()))
                .Callback<string>(x => domainUsed = x)
                .ReturnsAsync(new List<RedditAuthor>());

            var controller = CreateController(mockRedditSportsService.Object);

            // Act
            await controller.Get(domain);

            // Todo
            Assert.AreEqual(trimmed, domainUsed);
        }


        [Test]
        public async Task Get_ReturnsTheDataGivenByTheService()
        {
            // Arrange
            var domain = "domain";
            var data = new List<RedditAuthor> {
                new RedditAuthor {
                    Author = "author1",
                    Items = new List<RedditAuthorItem> 
                    {
                        new RedditAuthorItem {
                            Id = "id1",
                            CreatedDate = _dateTimeHelper.PosixTime,
                            Permalink = "permalink1",
                            Title = "title1"
                        }
                    }
                }
            };

            var mockRedditSportsService = new Mock<IRedditSportsService>();
            mockRedditSportsService
                .Setup(x => x.GetForDomainByAuthor(domain))
                .ReturnsAsync(data);

            var controller = CreateController(mockRedditSportsService.Object);

            // Act
            var response = await controller.Get(domain);

            // Todo
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Response status code should be OK.");

            List<RedditAuthor> authors;
            Assert.IsTrue(response.TryGetContentValue<List<RedditAuthor>>(out authors), "The response does not contain RedditAuthor's.");
            Assert.AreEqual(data.Count(), authors.Count(), "The number of authors is not the expected.");
            Assert.AreEqual(data.Single().Author, authors.Single().Author, "The name of the author is not the expected.");
            Assert.AreEqual(data.Single().Items.Single().Title, authors.Single().Items.Single().Title,
                "The title on the sinlge item is not the expected.");
        }

        
        private SportsController CreateController(IRedditSportsService redditSportsService)
        {
            var controller = new SportsController(redditSportsService);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller;
        }
    }
}
