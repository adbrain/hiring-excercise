using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using devTest_Web.API.Model;
using devTest_Web.API.Services;
using devTest_Web.Utils;
using Newtonsoft.Json;

namespace devTest_Web
{
    public class RootController : ApiController
    {
        private RedditService service = new RedditService();

        public string Get(string domain = "youtube.com")
        {
            var requestId = service.SaveApiData();

            var resList = service.GetApiData(requestId).Where(a => a.domain.Trim().ToLower() == domain.Trim().ToLower());

            var responseModel = resList.GroupBy(a => a.author)
                .Select(b => new
                {
                    author = b.Key,
                    items = b.ToList()
                }).ToList();

            return JsonConvert.SerializeObject(responseModel);
        }

    }
}