using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RestSharp;
using Adbrain.FullWebStack.Web.Models;

namespace Adbrain.FullWebStack.Tests.Integration.Api
{
    public class PostApiTests
    {
        [Fact]
        public async Task ApiShouldReturnResponseInCorrectFormat()
        {
            var client = new RestClient("http://localhost:58477/sports");

            var request = new RestRequest(Method.GET);
            request.AddQueryParameter("domain", "youtube.com");

            var response = await client.ExecuteTaskAsync<List<PostsGroup>>(request);

            Assert.NotEmpty(response.Data);
        }
    }
}
