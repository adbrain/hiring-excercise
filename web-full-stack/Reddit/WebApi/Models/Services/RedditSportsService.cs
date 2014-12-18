using Adbrain.Reddit.DataAccess.Entities;
using Adbrain.Reddit.DataAccess.Repositories;
using Adbrain.Reddit.WebApi.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Adbrain.Reddit.WebApi.Models.Services
{
    public class RedditSportsService : IRedditSportsService
    {
        private readonly IRawDataRepository _rawDataRepository;
        private readonly IWebClientWrapper _webClient;

        public RedditSportsService(
            IRawDataRepository rawDataRepository,
            IWebClientWrapper webClient)
        {
            _rawDataRepository = rawDataRepository;
            _webClient = webClient;
        }

        public async Task<string> GetForDomain(string domain)
        {
            // Todo: place this in the config
            var url = @"http://www.reddit.com/r/sports.json?limit=100";
            
            var content = await _webClient.DownloadStringTaskAsync(url);

            var rawData = new RawData { RedditResponse = content };
            _rawDataRepository.Save(rawData);

            var latest = _rawDataRepository.GetLatest().RedditResponse;

            // Todo: filter by domain and group by.
            return latest;
        }
    }
}