using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

using Adbrain.FullWebStack.Services;
using Adbrain.FullWebStack.Domain.Interfaces;
using Adbrain.FullWebStack.Service.Interfaces;
using Xunit;
using FizzWare.NBuilder;
using Adbrain.FullWebStack.Domain.Entities;

namespace Adbrain.FullWebStack.Tests.Unit
{
    public class PostServiceTests
    {
        private readonly PostService _postService;
        private readonly IRedditApiService _redditApiService;
        private readonly IPostRepository _postRepository;
        private readonly IEnumerable<Post> _posts;
        private const string _domain = "youtube.com";
        private const int _listsize = 100;

        public PostServiceTests()
        {
            _redditApiService = Substitute.For<IRedditApiService>();
            _postRepository = Substitute.For<IPostRepository>();
            _postService = new PostService(_redditApiService, _postRepository);
            _posts = GetTestPosts();

            _redditApiService.GetTopSportsPosts(_listsize).Returns(Task.FromResult(_posts));
        }

        [Fact]
        public void ShouldGetLatestPostsFromRedditApi()
        {
            _postService.GetGroupedByAuthor(_domain);

            _redditApiService.Received().GetTopSportsPosts(_listsize);
        }

        [Fact]
        public void ShouldDeleteExistingPostsFromRepositoryBeforeInsertingNewOnes()
        {
            _postService.GetGroupedByAuthor(_domain);

            _postRepository.Received().DeleteAll();
        }

        [Fact]
        public void ShouldCallRepositoryToInsertNewPosts()
        {
            _postService.GetGroupedByAuthor(_domain);

            _postRepository.Received().InsertAll(_posts);
            _postRepository.Received().SaveChanges();
        }

        private IEnumerable<Post> GetTestPosts()
        {
            return Builder<Post>
                .CreateListOfSize(_listsize)
                .Build();
        }
    }
}
