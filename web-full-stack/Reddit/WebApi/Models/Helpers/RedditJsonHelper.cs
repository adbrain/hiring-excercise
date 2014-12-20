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
                    CreatedDate = DateTimeFromMillis((long)it["data"]["created_utc"]),
                    Permalink = (string)it["data"]["permalink"]
                };

            return items.ToList();
        }

        private DateTime DateTimeFromMillis(long secs)
        {
            var posixTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
            var time = posixTime.AddSeconds(secs);
            return time;
        }
    }
}