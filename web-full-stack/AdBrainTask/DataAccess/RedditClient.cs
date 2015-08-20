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
        public IList<AdBrainTask.DataModels.Sport> GetSports()
        {
            IList<AdBrainTask.DataModels.Sport> sports = new List<AdBrainTask.DataModels.Sport>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.reddit.com/");
                HttpResponseMessage redditResponse = client.GetAsync("r/sports.json?limit=100").Result;

                if (redditResponse.IsSuccessStatusCode)
                {
                    var jsonObjectFromRedit = redditResponse.Content.ReadAsAsync<JObject>();
                    var serializer = JsonSerializer.Create();
                    sports = serializer
                        .Deserialize<ICollection<AdBrainTask.DataModels.ReditSport>>(jsonObjectFromRedit.Result["data"]["children"].CreateReader())
                        .Select(rs => rs.Data)
                        .ToList();
                }
            }

            return sports;
        }
    }
}