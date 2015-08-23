using AdBrainTask.DataAccess;
using AdBrainTask.DataModels;
using AdBrainTask.Dtos.Response;
using AdBrainTask.Tests.Comparers;
using AdBrainTask.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AdBrainTask.Tests
{
    [TestClass]
    public class RedditClientTest
    {
        [TestMethod]
        public void GetSportsRedditResponse_ShouldUse_TheCorrectEndpoint()
        {
            string expectedRequestUri = "http://www.reddit.com/r/sports.json?limit=100";

            HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            responseMessage.Content = new StringContent(RedditClientTest.SportPostsJson);
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(@"application/json");
            
            var httpMessageHandler = new HttpMessageHandlerMock(responseMessage);
            RedditClient redditCleint = new RedditClient(httpMessageHandler);
            var redditResponse = redditCleint.GetSportsRedditResponse();
            
            Assert.AreEqual<string>(expectedRequestUri, redditResponse.RequestMessage.RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public void GetSports_ShouldReturn_ProperlyDeserielizedReditResponse()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            responseMessage.Content = new StringContent(RedditClientTest.SportPostsJson);
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(@"application/json");

            var httpMessageHandler = new HttpMessageHandlerMock(responseMessage);
            RedditClient redditCleint = new RedditClient(httpMessageHandler);
            var redditResponse = redditCleint.GetSports().ToList();

            var jsonObjectFromRedit = new StringContent(RedditClientTest.SportPostsJson, Encoding.UTF8, new MediaTypeHeaderValue(@"application/json").ToString()).ReadAsAsync<JObject>();
            var serializer = JsonSerializer.Create();
            var expectedResult = serializer
                .Deserialize<ICollection<RedditSportPost>>(jsonObjectFromRedit.Result["data"]["children"].CreateReader())
                .Select(rs => rs.Data)
                .ToList();

            Assert.IsTrue(expectedResult.SequenceEqual(redditResponse, new SportPostComparer()));
        }

        [TestMethod]
        public void GetSports_ShouldNot_EmptyCollection_IfHttpStatusNotOk()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            var httpMessageHandler = new HttpMessageHandlerMock(responseMessage);
            RedditClient redditCleint = new RedditClient(httpMessageHandler);
            var redditResponse = redditCleint.GetSports().ToList();

            Assert.IsInstanceOfType(redditResponse, typeof(List<SportPost>));
            Assert.IsTrue(redditResponse.Count == 0);
        }

        // Sample JSON response from Reddit
        private const string SportPostsJson = "{    \"kind\": \"Listing\",    \"data\": {        \"modhash\": \"\",        \"children\": [            {                \"kind\": \"t3\",                \"data\": {                    \"domain\": \"self.sports\",                    \"id\": \"3fm4jd\",                    \"author\": \"lucdespo\",                    \"permalink\": \"/r/sports/comments/3fm4jd/meta_a_reminder_concerning_our_rules/\",                    \"created\": 1438609850.0,                    \"url\": \"http://www.reddit.com/r/sports/comments/3fm4jd/meta_a_reminder_concerning_our_rules/\",                    \"title\": \"[Meta] A reminder concerning our rules\",                    \"created_utc\": 1438606250.0                }            },            {                \"kind\": \"t3\",                \"data\": {                    \"domain\": \"self.sports\",                    \"id\": \"3hayj6\",                    \"author\": \"lucdespo\",                    \"permalink\": \"/r/sports/comments/3hayj6/meta_rsports_is_looking_to_add_more_people_to_its/\",                    \"created\": 1439839943.0,                    \"url\": \"http://www.reddit.com/r/sports/comments/3hayj6/meta_rsports_is_looking_to_add_more_people_to_its/\",                    \"title\": \"[Meta] A reminder concerning our rules\",                    \"created_utc\": 1439811143.0                }            },            {                \"kind\": \"t3\",                \"data\": {                    \"domain\": \"i.imgur.com\",                    \"id\": \"3hujoh\",                    \"author\": \"Meunderwears\",                    \"permalink\": \"/r/sports/comments/3hujoh/theres_something_wrong_with_this_ball/\",                    \"created\": 1440193548.0,                    \"url\": \"http://i.imgur.com/D7QxhUJ.gifv\",                    \"title\": \"MOCK: Mike Friers threw a no-no tonight....looks like he got some help\",                    \"created_utc\": 1440164748.0                }            },            {                \"kind\": \"t3\",                \"data\": {                    \"domain\": \"i.imgur.com\",                    \"id\": \"3hujil\",                    \"author\": \"j0be\",                    \"permalink\": \"/r/sports/comments/3hujil/a_boy_walks_onto_a_rugby_field/\",                    \"created\": 1440193475.0,                    \"url\": \"https://i.imgur.com/OcqiLHa.gifv\",                    \"title\": \"MOCK: Mike Friers threw a no-no tonight....looks like he got some help\",                    \"created_utc\": 1440164675.0,                }            },            {                \"kind\": \"t3\",                \"data\": {                   \"domain\": \"imgur.com\",                    \"id\": \"3hxnwn\",                    \"author\": \"SlipperySerpent\",                    \"permalink\": \"/r/sports/comments/3hxnwn/mike_friers_threw_a_nono_tonightlooks_like_he_got/\",                    \"created\": 1440245991.0,                    \"url\": \"http://imgur.com/cXsGCwX\",                    \"title\": \"Mike Friers threw a no-no tonight....looks like he got some help\",                    \"created_utc\": 1440217191.0,                }            }        ],        \"after\": \"t3_3hxnwn\",        \"before\": null    }}";
    }
}
