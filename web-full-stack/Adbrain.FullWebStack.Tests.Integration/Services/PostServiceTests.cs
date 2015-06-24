using Adbrain.FullWebStack.Domain.Entities;
using Adbrain.FullWebStack.Domain.Interfaces;
using Adbrain.FullWebStack.Infrastructure.Data;
using Adbrain.FullWebStack.Service.Interfaces;
using Adbrain.FullWebStack.Services;
using Autofac;
using Autofac.Core;
using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Adbrain.FullWebStack.Tests.Integration.Services
{
    public class PostServiceTests
    {
        private readonly IContainer _container;
        private const string Domain = "youtube.com";

        public PostServiceTests()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EFDbContext>().As<IDbContext>();
            builder.RegisterType<PostRepository>().As<IPostRepository>();
            builder.RegisterType<FakeRedditApiService>().As<IRedditApiService>();
            builder.RegisterType<PostService>().As<IPostService>();

            _container = builder.Build(); 
        }

        [Fact]
        public async void ShouldGroupPostsByAuthorCorrectly()
        {
            var redditApiService = _container.Resolve<IRedditApiService>();
            var posts = await redditApiService.GetTopSportsPosts(10);
            var expected = posts.GroupBy(post => post.Author).ToList();

            var postService = _container.Resolve<IPostService>();
            var actual = (await postService.GetGroupedByAuthor(Domain)).ToList();

            Assert.Equal(expected.Count, actual.Count);
            foreach (var expectedGroup in expected)
            {
                var actualGroup = actual.FirstOrDefault(item => item.Key == expectedGroup.Key);
                Assert.NotNull(actualGroup);
                Assert.Equal(expectedGroup.Count(), actualGroup.Count());
                foreach (var expectedPost in expectedGroup)
                {
                    Assert.NotNull(actualGroup.FirstOrDefault(ap => ap.Id == expectedPost.Id));
                }
            }
        }

        class FakeRedditApiService : IRedditApiService
        {
            public Task<IEnumerable<Post>> GetTopSportsPosts(int limit)
            {
                return Task.FromResult(Builder<Post>
                    .CreateListOfSize(10)
                    .All().With(post => post.Domain = Domain)
                    .TheFirst(3).With(post => post.Author = "John Lennon")
                    .TheNext(2).With(post => post.Author = "Paul McCartney")
                    .TheNext(4).With(post => post.Author = "George Harrison")
                    .TheLast(1).With(post => post.Author = "Ringo Starr")
                    .Build().AsEnumerable());
            }
        }
    }
}
