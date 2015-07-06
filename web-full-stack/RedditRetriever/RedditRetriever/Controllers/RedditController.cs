using RedditRetriever.Data;
using RedditRetriever.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RedditRetriever.Controllers
{
    public class RedditController : ApiController
    {
        private readonly IRedditAccess _reddit;
        private readonly IRedditRepository _repository;

        public RedditController()
            : this(new RedditAccess(), new RedditRepository())
        {
        }

        public RedditController(IRedditAccess reddit, IRedditRepository repository)
        {
            _reddit = reddit;
            _repository = repository;
        }

        [HttpGet]
        [Route("sports")]
        public async Task<IEnumerable<IGrouping<string, Post>>> GetSubreddit(string domain)
        {
            var url = _reddit.RetrieveUrlWithDomain(domain);
            var posts = await _reddit.GetPathAsync(url);
            await _repository.SavePostsAsync(posts);
            return posts.GroupBy(x => x.Author);
        }
    }
}
