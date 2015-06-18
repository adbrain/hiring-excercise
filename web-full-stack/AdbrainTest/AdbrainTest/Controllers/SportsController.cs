using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AdbrainTest.Models;
using AdbrainTest.Services;

namespace AdbrainTest.Controllers
{
    public class SportsController : ApiController
    {
        private readonly IRedditService _redditService;

        public SportsController(IRedditService redditService)
        {
            _redditService = redditService;
        }

        [Route("Sports")]
        [HttpGet]
        public IEnumerable<GroupedRedditListingViewModel> GetSports([FromUri] string domain = null)
        {
            var listings = _redditService.GetListings("sports");

            _redditService.SaveListings(listings);

            var results = RedditListingViewModel.FromJson(listings);
            return results
                // make sure we only return 100 results, should only get 100 back from reddit api anyway
                .Take(100)
                .Where(l => domain == null || l.Domain == domain)
                .GroupBy(l => l.Author)
                .Select(g => new GroupedRedditListingViewModel
                {
                    Author = g.Key,
                    Items = g.ToList()
                });
        }
    }
}