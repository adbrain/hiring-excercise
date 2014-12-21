using Adbrain.Reddit.WebApi.Models.JsonObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adbrain.Reddit.WebApi.Models.Helpers
{
    public class RedditJsonHelper : IRedditJsonHelper
    {
        private readonly IDateTimeHelper _dateTimeHelper;

        public RedditJsonHelper(IDateTimeHelper dateTimeHelper)
        {
            _dateTimeHelper = dateTimeHelper;
        }

        public List<RedditItem> ExtractItems(string redditResponse)
        {
            var json = JObject.Parse(redditResponse);

            var items =
                from it in json["data"]["children"]
                select new RedditItem
                {
                    Domain = (string)it["data"]["domain"],
                    Author = (string)it["data"]["author"],
                    Id = (string)it["data"]["id"],
                    Title = (string)it["data"]["title"],
                    CreatedDate = _dateTimeHelper.DateTimeFromSecsUtc((long)it["data"]["created_utc"]),
                    Permalink = (string)it["data"]["permalink"]
                };

            return items.ToList();
        }
    }
}