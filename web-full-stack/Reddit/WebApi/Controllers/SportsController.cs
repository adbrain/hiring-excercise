using Adbrain.Reddit.WebApi.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Adbrain.Reddit.WebApi.Controllers
{
    public class SportsController : ApiController
    {
        private IRedditSportsService _redditSportsService;

        public SportsController(IRedditSportsService redditSportsService)
        {
            _redditSportsService = redditSportsService;
        }

        // GET api/sports?domain='youtube.com'
        public async Task<HttpResponseMessage> Get(string domain)
        {
            var filteredContent = await _redditSportsService.GetForDomain(domain);

            var response = Request.CreateResponse(HttpStatusCode.OK, filteredContent);

            return response;
        }
    }
}