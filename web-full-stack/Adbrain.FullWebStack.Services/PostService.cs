using Adbrain.FullWebStack.Domain.Entities;
using Adbrain.FullWebStack.Domain.Interfaces;
using Adbrain.FullWebStack.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.FullWebStack.Services
{
    public class PostService : IPostService
    {
        private readonly IRedditApiService _redditApiService;
        private readonly IPostRepository _postRepository;

        public PostService(IRedditApiService redditApiService, IPostRepository postRepository)
        {
            _redditApiService = redditApiService;
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<IGrouping<string, Post>>> GetGroupedByAuthor(string domain)
        {
            var posts = await _redditApiService.GetTopSportsPosts(100);

            _postRepository.DeleteAll();
            _postRepository.InsertAll(posts);
            _postRepository.SaveChanges();

            return _postRepository.GetGroupedByAuthor(domain);
        }
    }
}
