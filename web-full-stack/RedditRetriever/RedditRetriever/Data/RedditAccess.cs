using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedditRetriever.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace RedditRetriever.Data
{
    public class RedditAccess : IRedditAccess
    {
        private readonly HttpClient _client = new HttpClient();

        public RedditAccess()
        {
            _client.BaseAddress = new Uri("https://www.reddit.com");
        }

        public async Task<IEnumerable<Post>> GetUrlAsync(string url)
        {
            var subredditJson = await _client.GetStringAsync(url);
            //var jobject = JObject.Parse(subredditJson);
            //var a = jobject["data"]["children"].ToString();
            var posts = JsonConvert.DeserializeObject<SubredditSearch>(subredditJson);
            return posts.Data.Children.Select(x => x.data);
        }
    }
}