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
        public async Task<IEnumerable<UserPostsModel>> GetSubreddit(string domain)
        {
            var url = _reddit.RetrieveUrlWithDomain(domain);
            var posts = await _reddit.GetPathAsync(url);
            var callId = Guid.NewGuid().ToString();
            var postsWithCallId = posts.Select(x => 
                {
                    x.CallId = callId;
                    return x;
                });
            await _repository.SavePostsAsync(postsWithCallId);
            var retrievedPosts = _repository.GetPosts(callId);
            return retrievedPosts.GroupBy(x => x.Author).Select(x => new UserPostsModel { Author = x.Key, Posts = x.AsEnumerable() });
        }
    }
}
