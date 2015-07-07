using NUnit.Framework;
using RedditRetriever.Controllers;
using RedditRetriever.Data;
using RedditRetriever.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditRetriever.Tests
{
    [TestFixture]
    public class FullFlowTest
    {
        [Test]
        public async Task Controller_UsingADomain_AllDBSavesHaveDomain()
        {
            var controller = new RedditController(new MixedDomainRedditAccess(), new MockRepository());
            var posts = await controller.GetSubreddit("bbc.com");

            var expected = new List<UserPostsModel>
            {
                new UserPostsModel
                {
                    Author = "Account1",
                    Posts = new List<PostDto>
                    {
                        new PostDto
                        {
                            Id = "1",
                            Title = "1",
                            CreatedDate = DateTimeUnixConverter.FromUnixTimestamp(1436282396.0),
                            PermaLink = "link"
                        },
                        new PostDto
                        {
                            Id = "2",
                            Title = "2",
                            CreatedDate = DateTimeUnixConverter.FromUnixTimestamp(1436282396.0),
                            PermaLink = "link"
                        }
                    }
                },
                new UserPostsModel
                {
                    Author = "Account2",
                    Posts = new List<PostDto>
                    {
                        new PostDto
                        {
                            Id = "3",
                            Title = "3",
                            CreatedDate = DateTimeUnixConverter.FromUnixTimestamp(1436282396.0),
                            PermaLink = "link"
                        },
                        new PostDto
                        {
                            Id = "4",
                            Title = "4",
                            CreatedDate = DateTimeUnixConverter.FromUnixTimestamp(1436282396.0),
                            PermaLink = "link"
                        }
                    }
                },
                new UserPostsModel
                {
                    Author = "Account3",
                    Posts = new List<PostDto>
                    {
                        new PostDto
                        {
                            Id = "5",
                            Title = "5",
                            CreatedDate = DateTimeUnixConverter.FromUnixTimestamp(1436282396.0),
                            PermaLink = "link"
                        }
                    }
                }
            };

            var actualList = posts.ToList();

            Assert.AreEqual(expected, actualList);
        }

    }
}
