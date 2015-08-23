using AdBrainTask.DataModels;
using AdBrainTask.Dtos.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdBrainTask.DataAccess
{
    public class RedditClient : IRedditClient
    {
        public RedditClient()
        {
            this.handler = new HttpClientHandler();
        }

        public RedditClient(HttpMessageHandler handler)
        {
            this.handler = handler;
        }

        public async Task<IList<SportPost>> GetSports()
        {
            IList<SportPost> sports = new List<SportPost>();
            var redditResponse = await this.GetSportsRedditResponse();

            if (redditResponse.IsSuccessStatusCode)
            {
                var jsonObjectFromRedit = redditResponse.Content.ReadAsAsync<JObject>();
                var serializer = JsonSerializer.Create();
                sports = serializer
                    .Deserialize<ICollection<RedditSportPost>>(jsonObjectFromRedit.Result["data"]["children"].CreateReader())
                    .Select(rs => rs.Data)
                    .ToList();
            }

            return sports;
        }

        internal async Task<HttpResponseMessage> GetSportsRedditResponse()
        {
            using (var client = new HttpClient(this.handler))
            {
                client.BaseAddress = new Uri("http://www.reddit.com/");
                HttpResponseMessage redditResponse = await client.GetAsync("r/sports.json?limit=100");

                return redditResponse;
            }
        }

        private HttpMessageHandler handler;
    }
}