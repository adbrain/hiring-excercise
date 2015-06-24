using Adbrain.FullWebStack.Domain.Entities;
using Adbrain.FullWebStack.Service.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.FullWebStack.Services
{
    public class RedditApiService : IRedditApiService
    {
        public async Task<IEnumerable<Post>> GetTopSportsPosts(int limit)
        {
            var client = new RestClient("http://www.reddit.com/r/sports.json");

            var request = new RestRequest(Method.GET);
            request.AddQueryParameter("limit", limit.ToString());

            var response = await client.ExecuteTaskAsync<RedditResponse>(request);

            return response.Data.Data.Children.Select(c => c.Data);
        }

        class RedditResponse
        {
            public RedditResponseData Data { get; set; }
        }

        class RedditResponseData
        {
            public List<RedditResponseDataItem> Children { get; set; }
        }

        class RedditResponseDataItem
        {
            public Post Data { get; set; }
        }
    }
}
