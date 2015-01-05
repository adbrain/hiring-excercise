using System;
using System.IO;
using System.Net;
using AdbrainTest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdbrainTest.Services
{
    public class RedditService : IRedditService
    {
        private const int ResultsLimit = 100;

        public JObject GetListings(string subReddit)
        {
            var response = WebRequest.Create(String.Format("http://www.reddit.com/r/{0}.json?limit={1}", subReddit, ResultsLimit)).GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return JsonConvert.DeserializeObject<JObject>(reader.ReadToEnd());
            }
        }

        public void SaveListings(JObject listings)
        {
            // save response json as a string in the database, so we don't have to create a model for it (may want to do that if this was a real task)
            // also, a nosql db would be more suited for this, but the task specified sql server
            using (var db = new EntityModel())
            {
                db.SportListings.Add(new RedditListingsModel
                {
                    RetrievedDate = DateTime.Now,
                    JsonData = listings.ToString()
                });
                db.SaveChanges();
            }
        }
    }
}