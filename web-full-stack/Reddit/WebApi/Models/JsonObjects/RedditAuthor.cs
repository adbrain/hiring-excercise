using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adbrain.Reddit.WebApi.Models.JsonObjects
{
    public class RedditAuthor
    {
        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("items")]
        public List<RedditAuthorItem> Items { get; set; }
    }
}