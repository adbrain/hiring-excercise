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

        private const string BasePath = "/r/sports.json?limit=100";

        public RedditAccess()
        {
            _client.BaseAddress = new Uri("https://www.reddit.com");
        }

        public async Task<IEnumerable<Post>> GetPathAsync(string url)
        {
            var subredditJson = await _client.GetStringAsync(url);
            var posts = JsonConvert.DeserializeObject<SubredditSearch>(subredditJson);
            return posts.Data.Children
                             .Select(x => x.data);
        }

        public string RetrieveUrlWithDomain(string domain)
        {

            if (String.IsNullOrWhiteSpace(domain))
            {
                return BasePath;
            }
            var completeDomain = BasePath + "&site=" + domain;
            return completeDomain;
        }
    }
}