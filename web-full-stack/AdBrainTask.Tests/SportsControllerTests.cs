using AdBrainTask.Controllers;
using AdBrainTask.DataAccess;
using AdBrainTask.Repositories;
using AdBrainTask.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

namespace AdBrainTask.Tests
{
    [TestClass]
    public class SportsControllerTests
    {
        [TestMethod]
        public void Get_ShouldReturn_FilteredByDomain_IfDomainProvided()
        {
            IList<AdBrainTask.DataModels.Sport> mockedSports = new List<AdBrainTask.DataModels.Sport>();
            mockedSports.Add(new AdBrainTask.DataModels.Sport()
            {
                Id = "abc",
                Author = "author1",
                Domain = "domain1",
                Title = "sport1",
                Permalink = "/sport1",
                Created = "1439766987"
            });
            mockedSports.Add(new AdBrainTask.DataModels.Sport()
            {
                Id = "dsa",
                Author = "author1",
                Domain = "domain2",
                Title = "sport2",
                Permalink = "/sport2",
                Created="1439766987"
            });
            mockedSports.Add(new AdBrainTask.DataModels.Sport()
            {
                Id = "jkl",
                Author = "author2",
                Domain = "domain1",
                Title = "sport3",
                Permalink = "/sport3",
                Created = "1439766987"
            });

            IRedditClient redditClient = new RedditClientMock(mockedSports);
            ISportsRepository sportsRepository = new SportsRepositoryMock();
            SportsController controller = new SportsController(redditClient, sportsRepository);

            var actionResult = controller.Get("domain1");
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<IGrouping<string, AdBrainTask.Dtos.Response.Sport>>>;

            Assert.IsNotNull(response.Content);

            var sportsFromController = response.Content;
            var mockedSportsFiltered = mockedSports.Where(ms => ms.Domain == "domain1")
                .Select(s => new AdBrainTask.Dtos.Response.Sport()
                {
                    Author = s.Author,
                    Domain = s.Domain,
                    DateCreated = new DateTime(1970, 01, 01).AddSeconds(Convert.ToInt64(s.Created)),
                    Id = s.Id,
                    Title = s.Title,
                    Url = s.Permalink
                })
                .GroupBy(s => s.Author);

            int index = 0;
            foreach (var mockedSportsGroup in mockedSportsFiltered.ToList())
            {
                Assert.AreEqual<string>(mockedSportsGroup.Key, sportsFromController.ToList()[index].Key);                
                Assert.IsTrue(mockedSportsGroup.ToList().SequenceEqual(sportsFromController.ToList()[index].ToList(), new SportDtoComparer()));
                index = index + 1;
            }
        }

        [TestMethod]
        public void Get_ShouldReturn_All_IfDomainNotProvided()
        {
            IList<AdBrainTask.DataModels.Sport> mockedSports = new List<AdBrainTask.DataModels.Sport>();
            mockedSports.Add(new AdBrainTask.DataModels.Sport()
            {
                Id = "abc",
                Author = "author1",
                Domain = "domain1",
                Title = "sport1",
                Permalink = "/sport1",
                Created = "1439766987"
            });
            mockedSports.Add(new AdBrainTask.DataModels.Sport()
            {
                Id = "dsa",
                Author = "author1",
                Domain = "domain2",
                Title = "sport2",
                Permalink = "/sport2",
                Created = "1439766987"
            });
            mockedSports.Add(new AdBrainTask.DataModels.Sport()
            {
                Id = "jkl",
                Author = "author2",
                Domain = "domain1",
                Title = "sport3",
                Permalink = "/sport3",
                Created = "1439766987"
            });

            IRedditClient redditClient = new RedditClientMock(mockedSports);
            ISportsRepository sportsRepository = new SportsRepositoryMock();
            SportsController controller = new SportsController(redditClient, sportsRepository);

            var actionResult = controller.Get("");
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<IGrouping<string, AdBrainTask.Dtos.Response.Sport>>>;

            Assert.IsNotNull(response.Content);

            var sportsFromController = response.Content;
            var mockedSportsFiltered = mockedSports
                .Select(s => new AdBrainTask.Dtos.Response.Sport()
                {
                    Author = s.Author,
                    Domain = s.Domain,
                    DateCreated = new DateTime(1970, 01, 01).AddSeconds(Convert.ToInt64(s.Created)),
                    Id = s.Id,
                    Title = s.Title,
                    Url = s.Permalink
                })
                .GroupBy(s => s.Author);

            int index = 0;
            foreach (var mockedSportsGroup in mockedSportsFiltered.ToList())
            {
                Assert.AreEqual<string>(mockedSportsGroup.Key, sportsFromController.ToList()[index].Key);
                Assert.IsTrue(mockedSportsGroup.ToList().SequenceEqual(sportsFromController.ToList()[index].ToList(), new SportDtoComparer()));
                index = index + 1;
            }
        }
    }
}
