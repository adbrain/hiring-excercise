using POCO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BLL
{
    public class Sports
    {
        public static async Task<IEnumerable<dynamic>> GetPosts(string domain = "")
        {
            if (domain == null)
            {
                domain = "";
            }

            #region Validation
            // Check for a valid domain name
            if (!String.IsNullOrEmpty(domain))
            {
                if (Uri.CheckHostName(domain) == UriHostNameType.Unknown)
                {
                    throw new ArgumentOutOfRangeException("domain", String.Format("Invalid domain: '{0}'", domain));
                }
            }
            #endregion

            #region Business logic
            const string REDDIT_BASE_ADDRESS = @"http://www.reddit.com/";
            const string REDDIT_ENDPOINT = @"r/sports.json?limit=100";

            #region Fetch top 100 posts from reddit
            dynamic top100PostsDto;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(REDDIT_BASE_ADDRESS);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var result = await client.GetAsync(REDDIT_ENDPOINT);

                if (result.IsSuccessStatusCode)
                {
                    var successText = result.Content.ReadAsStringAsync().Result;
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    top100PostsDto = serializer.Deserialize<object>(successText);
                }
                else
                {
                    var errorText = result.Content.ReadAsStringAsync().Result;
                    throw new EndpointUnresponsiveException(errorText);
                }
            }
            #endregion

            #region Parse results from reddit into POCO objects and store them to the database
            var posts = new List<RedditPost>();
            foreach (var jsonPost in top100PostsDto["data"]["children"])
            {
                var newPost = new RedditPost()
                    {
                        author = jsonPost["data"]["author"],
                        createdDate = UnixTimeStampToDateTime((long)jsonPost["data"]["created_utc"]),
                        id = jsonPost["data"]["id"],
                        permalink = jsonPost["data"]["permalink"],
                        title = jsonPost["data"]["title"],
                        domain = jsonPost["data"]["domain"]
                    };
                posts.Add(newPost);
            }

            var postRepo = new RedditPostRepository();
            await postRepo.AddRedditPosts(posts);
            await postRepo.Save();
            #endregion

            var requestId = posts[0].RequestId;
            var results = await postRepo.Get(requestId, domain);
            return results;
            #endregion
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }
    }
}
