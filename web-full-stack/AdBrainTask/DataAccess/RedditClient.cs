using AdBrainTask.DataModels;
using AdBrainTask.Dtos.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace AdBrainTask.DataAccess
{
    public class RedditClient : IRedditClient
    {
        public IList<AdBrainTask.DataModels.SportPost> GetSports()
        {
            IList<SportPost> sports = new List<SportPost>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.reddit.com/");
                HttpResponseMessage redditResponse = client.GetAsync("r/sports.json?limit=100").Result;

                if (redditResponse.IsSuccessStatusCode)
                {
                    var jsonObjectFromRedit = redditResponse.Content.ReadAsAsync<JObject>();
                    var serializer = JsonSerializer.Create();
                    sports = serializer
                        .Deserialize<ICollection<RedditSportPost>>(jsonObjectFromRedit.Result["data"]["children"].CreateReader())
                        .Select(rs => rs.Data)
                        .ToList();
                }
            }

            return sports;
        }
    }
}