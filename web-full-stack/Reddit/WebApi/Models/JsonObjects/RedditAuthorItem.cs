using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adbrain.Reddit.WebApi.Models.JsonObjects
{
    public class RedditAuthorItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdDate")]
        public DateTimeOffset CreatedDate { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("permalink")]
        public string Permalink { get; set; }
    }
}