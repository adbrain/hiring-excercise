using RedditRetriever.Data;
using RedditRetriever.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RedditRetriever.Controllers
{
    public class RedditController : ApiController
    {
        private const string BaseUrl = "/r/sports.json?limit=100";
        private readonly IRedditAccess _reddit;

        public RedditController()
            : this(new RedditAccess())
        {
        }

        public RedditController(IRedditAccess reddit)
        {
            _reddit = reddit;
        }

        private string RetrieveUrl(string domain)
        {
            if(domain == null)
            {
                return BaseUrl;
            }
            var completeDomain = BaseUrl + "&site=" + domain;
            return completeDomain;
        }

        [HttpGet]
        [Route("sports")]
        public async Task<IEnumerable<Post>> GetSubreddit()
        {
            var domain = Request.GetQueryNameValuePairs().SingleOrDefault(x => x.Key == "domain").Value;
            var url = RetrieveUrl(domain);
            var res = await _reddit.GetUrlAsync(url);
            return res;
        }
    }
}
