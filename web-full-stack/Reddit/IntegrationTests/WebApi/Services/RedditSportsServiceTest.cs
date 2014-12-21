using Adbrain.Reddit.DataAccess.DbContexts;
using Adbrain.Reddit.DataAccess.Repositories;
using Adbrain.Reddit.DataAccess.Wrappers;
using Adbrain.Reddit.WebApi.Models.Constants;
using Adbrain.Reddit.WebApi.Models.Helpers;
using Adbrain.Reddit.WebApi.Models.Services;
using Adbrain.Reddit.WebApi.Models.Wrappers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.IntegrationTests.WebApi.Services
{
    [TestFixture]
    public class RedditSportsServiceTest : BaseIntegrationTest
    {
        private string redditResponse = @"{
    ""kind"": ""Listing"",
    ""data"": {
        ""modhash"": """",
        ""children"": [
            {
                ""kind"": ""t3"",
                ""data"": {
                    ""domain"": ""domainA"",
                    ""id"": ""id-A-1-a"",
                    ""author"": ""author1"",
                    ""permalink"": ""permalink-A-1-a"",
                    ""title"": ""title-A-1-a"",
                    ""created_utc"": 0,
                 }
            },
            {
                ""kind"": ""t3"",
                ""data"": {
                    ""domain"": ""domainA"",
                    ""id"": ""id-A-1-b"",
                    ""author"": ""author1"",
                    ""permalink"": ""permalink-A-1-b"",
                    ""title"": ""title-A-1-b"",
                    ""created_utc"": 0,
                 }
            },
            {
                ""kind"": ""t3"",
                ""data"": {
                    ""domain"": ""domainB"",
                    ""id"": ""id-B-2-a"",
                    ""author"": ""author2"",
                    ""permalink"": ""permalink-B-2-a"",
                    ""title"": ""title-B-2-a"",
                    ""created_utc"": 0,
                 }
            },
            {
                ""kind"": ""t3"",
                ""data"": {
                    ""domain"": ""domainA"",
                    ""id"": ""id-A-2-b"",
                    ""author"": ""author2"",
                    ""permalink"": ""permalink-A-2-b"",
                    ""title"": ""title-A-2-b"",
                    ""created_utc"": 0,
                 }
            }],
        ""after"": ""after"",
        ""before"": null
      }
}";

        private IRedditSportsService _redditSportsService;
        // I use this to verify that the data has been saved in the database.
        private ISportsDataRepository _independentSportsDataRepository;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            var appSettingsHelper = new AppSettingsHelper(ConfigurationManager.AppSettings);
            var url = appSettingsHelper.GetString(AppSettingsKey.RedditSportsUrl);
            var mockWebClient = new Mock<IWebClientWrapper>();
            mockWebClient.Setup(x => x.DownloadStringTaskAsync(url)).ReturnsAsync(redditResponse);
            var dateTimeHelper = new DateTimeHelper();
            var redditJsonHelper = new RedditJsonHelper(dateTimeHelper);
            var dbContext = new SqlDbContext();
            var clock = new SystemClock();
            var sportsDataRepository = new SportsDataRepository(clock, dbContext);
            _redditSportsService = new RedditSportsService(
                appSettingsHelper, 
                redditJsonHelper,
                sportsDataRepository,
                mockWebClient.Object);

            var independentDbContext = new SqlDbContext();
            _independentSportsDataRepository = new SportsDataRepository(clock, independentDbContext);
        }

        [Test]
        public async Task GetForDomainByAuthor_StoresResponseAndFiltersGroupsData()
        {
            var domain = "domainA";

            var authors = await _redditSportsService.GetForDomainByAuthor(domain);

            // Check that the data has been persisted in the database.
            var latest = await _independentSportsDataRepository.GetLatest();
            Assert.IsNotNull(latest, "No data were returned by the repository");
            Assert.AreEqual(redditResponse, latest.RedditResponse, "Response was not saved in RedditResponse field.");

            // Check that filtered and grouped data are right.
            var expectedAuthorCount = 2;
            Assert.AreEqual(expectedAuthorCount, authors.Count(), "Number of authors is not the expected.");

            var author1 = authors.OrderBy(x => x.Author).First();
            var author2 = authors.OrderByDescending(x => x.Author).First();

            // Both items for author1 are for domainA.
            Assert.AreEqual(2, author1.Items.Count(), "Number of items for author1 is not the expected.");
            // Author author2 has one item for domainA only.
            Assert.AreEqual(1, author2.Items.Count(), "Number of items for author2 is not the expected.");

        }

    }
}
