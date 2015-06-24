using Adbrain.FullWebStack.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Adbrain.FullWebStack.Tests.Integration.Services
{
    public class RedditApiServiceTests
    {
        private readonly RedditApiService _service;
        private readonly int _limit;

        public RedditApiServiceTests()
        {
            _service = new RedditApiService();
            _limit = 100;
        }

        [Fact]
        public async Task GetFirstTopSportsPostsShouldReturnCorrectNumberOfResults()
        {
            var results = await _service.GetTopSportsPosts(_limit);

            Assert.Equal(_limit, results.Count());
        }

        [Fact]
        public async Task GetTopSportsPostsShouldAllHaveIds()
        {
            var results = await _service.GetTopSportsPosts(_limit);

            Assert.True(results.All(r => !string.IsNullOrEmpty(r.Id)));
        }
    }
}
