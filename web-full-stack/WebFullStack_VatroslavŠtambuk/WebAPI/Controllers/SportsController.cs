using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Sample controller documentation.
    /// </summary>
    public class SportsController : ApiController
    {
        /// <summary>
        /// Returns top 100 posts in the /r/sports subreddit filtered by the specified domain and grouped by author.
        /// </summary>
        /// <param name="domain">Domain by which to filter the results.</param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable<RedditPostAuthorDTO>))]
        [HandleException(Type = typeof(ArgumentOutOfRangeException), Status = HttpStatusCode.BadRequest)]
        [HandleException(Type = typeof(EndpointUnresponsiveException), Status = HttpStatusCode.InternalServerError)]
        [HandleException(Type = typeof(Exception), Status = HttpStatusCode.InternalServerError)]
        public async Task<IEnumerable<dynamic>> Get(string domain = "")
        {
            var result = await Sports.GetPosts(domain);
            return result;
        }
    }
}
