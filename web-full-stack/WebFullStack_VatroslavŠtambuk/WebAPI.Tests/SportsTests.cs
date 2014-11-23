using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using WebAPI.Controllers;
using BLL;
using System.Web.Script.Serialization;

namespace WebAPI.Tests
{
    [TestFixture]
    public class SportsTests
    {
        [Test]
        public static async Task Get_All_Posts_Unfiltered()
        {
            var result = await GetPosts("");
            Assert.That(result.Count() <= 100);
        }

        [Test]
        public static async Task Get_All_Posts_filtered()
        {
            var result = await GetPosts("youtube.com");
            Assert.That(result.Count() <= 100);
        }

        // helper method to simulate GET request
        static async Task<IEnumerable<RedditPostAuthorDTO>> GetPosts(string domain)
        {
            var result = new List<RedditPostAuthorDTO>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:31413");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(@"api/sports?domain=" + domain);
                if (response.IsSuccessStatusCode)
                {
                    var successText = response.Content.ReadAsStringAsync().Result;
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    dynamic items = serializer.Deserialize<object>(successText);

                    foreach (var item in items)
                    {
                        var redditPostAuthorDTO = new RedditPostAuthorDTO() { author = item["author"], items = new List<RedditPostItemDTO>() };
                        foreach (var postItem in item["items"])
                        {
                            var redditPostItemDTO = new RedditPostItemDTO()
                            {
                                id = postItem["id"],
                                createdDate = DateTime.Parse(postItem["createdDate"]),
                                title = postItem["title"],
                                permalink = postItem["permalink"]
                            };
                            redditPostAuthorDTO.items.Add(redditPostItemDTO);
                        }
                    }
                }
                else
                {
                    throw new Exception("Cannot fetch posts!");
                }
            }

            return result;
        }
    }
}
