using RedditRetriever.Data;
using RedditRetriever.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditRetriever.Tests
{
    public class MixedDomainRedditAccess : IRedditAccess
    {
        public string RetrieveUrlWithDomain(string domain)
        {
            return "";
        }

        public Task<IEnumerable<Models.Post>> GetPathAsync(string path)
        {
            var posts = new List<Post>
            {
                new Post
                {
                    Author = "Account1",
                    Id = "1",
                    Title = "1",
                    Created = 1436282396.0,
                    Domain = "youtube.com",
                    Permalink = "link"
                },
                new Post
                {
                    Author = "Account1",
                    Id = "2",
                    Title = "2",
                    Created = 1436282396.0,
                    Domain = "imgur.com",
                    Permalink = "link"
                },
                new Post
                {
                    Author = "Account2",
                    Id = "3",
                    Title = "3",
                    Created = 1436282396.0,
                    Domain = "google.com",
                    Permalink = "link"
                },
                new Post
                {
                    Author = "Account2",
                    Id = "4",
                    Title = "4",
                    Created = 1436282396.0,
                    Domain = "bbc.com",
                    Permalink = "link"
                },
                new Post
                {
                    Author = "Account3",
                    Id = "5",
                    Title = "5",
                    Created = 1436282396.0,
                    Domain = "youtube.com",
                    Permalink = "link"
                }
            };
            return Task.FromResult((IEnumerable<Post>)posts);
        }
    }
}
