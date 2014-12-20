using Adbrain.Reddit.DataAccess.Entities;
using Adbrain.Reddit.DataAccess.Repositories;
using Adbrain.Reddit.WebApi.Models.Constants;
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
        private readonly IAppSettingsHelper _appSettingsHelper;
        private readonly ISportsDataRepository _sportsDataRepository;
        private readonly IWebClientWrapper _webClient;

        public RedditSportsService(
            IAppSettingsHelper appSettingsHelper,
            ISportsDataRepository sportsDataRepository,
            IWebClientWrapper webClient)
        {
            _appSettingsHelper = appSettingsHelper;
            _sportsDataRepository = sportsDataRepository;
            _webClient = webClient;
        }

        public async Task<string> GetForDomain(string domain)
        {
            var url = _appSettingsHelper.GetString(AppSettingsKey.RedditSportsUrl);
            
            var content = await _webClient.DownloadStringTaskAsync(url);

            var sportsData = new SportsData { RedditResponse = content };
            await _sportsDataRepository.Save(sportsData);

            var latest = await _sportsDataRepository.GetLatest();
            var response = latest.RedditResponse;

            // Todo: filter by domain and group by.
            return response;
        }
    }
}