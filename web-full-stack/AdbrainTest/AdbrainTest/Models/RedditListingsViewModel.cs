using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdbrainTest.Models
{
    public class RedditListingViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string PermaLink { get; set; }

        [JsonIgnore]
        public string Domain { get; set; }

        public string Author { get; set; }
        public DateTime Created { get; set; }

        public static IEnumerable<RedditListingViewModel> FromJson(JObject json)
        {
            return json["data"]["children"]
                .Select(item => item["data"])
                .Select(data => new RedditListingViewModel
                {
                    Id = data["id"].ToString(),
                    Created = new DateTime(1970, 1, 1).AddSeconds(long.Parse(data["created"].ToString())),
                    Title = data["title"].ToString(),
                    PermaLink = data["permalink"].ToString(),
                    Domain = data["domain"].ToString(),
                    Author = data["author"].ToString()
                });
        }
    }
}