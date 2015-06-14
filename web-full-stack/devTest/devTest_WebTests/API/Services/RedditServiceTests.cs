using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devTest_Web.API.Services;
using NUnit.Framework;
namespace devTest_Web.API.Services.Tests
{
    [TestFixture()]
    public class RedditServiceTests
    {
        private RedditService service = new RedditService();

        [Test()]
        public void SaveApiDataTest()
        {
            var requestId = service.SaveApiData();
            Assert.Greater(requestId, 0);
        }

        [Test()]
        public void GetApiDataTest()
        {
            var requestId = service.SaveApiData();
            var result = service.GetApiData(requestId);

            Assert.Greater(result.Count, 0);
        }
    }
}
