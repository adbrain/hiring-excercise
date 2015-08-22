using AdBrainTask.DataAccess;
using AdBrainTask.Dtos.Response;
using AdBrainTask.Repositories;
using System;
using System.Linq;
using System.Web.Http;

namespace AdBrainTask.Controllers
{
    public class SportPostsController : ApiController
    {
        public SportPostsController()
            : base()
        {
            this.redditClient = new RedditClient();
            this.sportsRepository = new SportPostsRepository();
        }

        public SportPostsController(IRedditClient redditClient, ISportPostsRepository sportsRepository)
        {
            this.redditClient = redditClient;
            this.sportsRepository = sportsRepository;
        }

        public IHttpActionResult Get(string domain)
        {
            // Remove all current sports from database.
            this.sportsRepository.DeleteMany(this.sportsRepository.GetSports());

            var sportsFromReddit = this.redditClient.GetSports();

            // Add new sports from reddit to database
            this.sportsRepository.AddMany(sportsFromReddit);

            IQueryable<AdBrainTask.DataModels.SportPost> query;

            if(!string.IsNullOrEmpty(domain))
            {
                query = this.sportsRepository.GetSports().Where(s => s.Domain == domain);
            }
            else
            {
                query = this.sportsRepository.GetSports();
            }
            

            // Compose response
            var response = query
                .ToList()
                .Select(s => new SportPostDto() 
                { 
                    Author = s.Author,
                    Domain = s.Domain,
                    DateCreated = new DateTime(1970, 01, 01).AddSeconds(Convert.ToInt64(s.Created)),
                    Id = s.Id,
                    Title = s.Title,
                    Url = s.Permalink
                })
                .GroupBy(s => s.Author);

            return Ok(response);
        }

        private IRedditClient redditClient;
        private ISportPostsRepository sportsRepository;
    }
}
