using Adbrain.Reddit.DataAccess.Entities;
using Adbrain.Reddit.DataAccess.Repositories;
using Adbrain.Reddit.WebApi.Models.Constants;
using Adbrain.Reddit.WebApi.Models.Helpers;
using Adbrain.Reddit.WebApi.Models.JsonObjects;
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
        private readonly IRedditJsonHelper _redditJsonHelper;
        private readonly ISportsDataRepository _sportsDataRepository;
        private readonly IWebClientWrapper _webClient;

        public RedditSportsService(
            IAppSettingsHelper appSettingsHelper,
            IRedditJsonHelper redditJsonHelper,
            ISportsDataRepository sportsDataRepository,
            IWebClientWrapper webClient)
        {
            _appSettingsHelper = appSettingsHelper;
            _redditJsonHelper = redditJsonHelper;
            _sportsDataRepository = sportsDataRepository;
            _webClient = webClient;
        }

        public async Task<IList<RedditAuthor>> GetForDomainByAuthor(string domain)
        {
            var url = _appSettingsHelper.GetString(AppSettingsKey.RedditSportsUrl);
            
            var content = await _webClient.DownloadStringTaskAsync(url);

            var sportsData = new SportsData { RedditResponse = content };
            await _sportsDataRepository.Save(sportsData);

            var latest = await _sportsDataRepository.GetLatest();

            var redditItems = _redditJsonHelper.ExtractItems(latest.RedditResponse);

            var domainLowerCase = domain.ToLower();
            var filterdRedditItems =
                redditItems
                .Where(it => it.Domain.ToLower() == domainLowerCase);

            var result =
                filterdRedditItems
                .GroupBy(it => it.Author)
                .Select(gr => new RedditAuthor
                {
                    Author = gr.Key,
                    Items = gr.Select(it =>
                        new RedditAuthorItem
                        {
                            Id = it.Id,
                            Title = it.Title,
                            CreatedDate = it.CreatedDate,
                            Permalink = it.Permalink
                        }).ToList()
                }).ToList();

            return result;
        }
    }
}