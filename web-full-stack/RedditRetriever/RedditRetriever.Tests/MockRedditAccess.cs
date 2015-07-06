using RedditRetriever.Data;
using RedditRetriever.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditRetriever.Tests
{
    public class MockRedditAccess : IRedditAccess
    {
        string IRedditAccess.RetrieveUrlWithDomain(string domain)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Models.Post>> IRedditAccess.GetPathAsync(string path)
        {
            string domain = "google.com";
            string domain2 = "imgur.com";

            if(path.Contains("domain"))
            {
                domain = domain2 = path.Substring(path.IndexOf("domain") + 1);
            }
            var posts = new List<Post>
            {
                new Post
                {
                    Domain = domain,
                    Author = "AccountTest",
                    Id = "ajshf",
                    Title = "A test link"
                },

                new Post
                {
                    Domain = domain2,
                    Author = "TestAccount",
                    Id = "qeitu",
                    Title = "A second test link"
                }
            };
            return Task.FromResult((IEnumerable<Post>)posts);
        }
    }
}
