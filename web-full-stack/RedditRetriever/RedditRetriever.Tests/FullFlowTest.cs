using NUnit.Framework;
using RedditRetriever.Controllers;
using RedditRetriever.Data;
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
            var controller = new RedditController(new MockRedditAccess(), new MockRepository("bbc.com", 2));
            await controller.GetSubreddit("bbc.com");
        }

    }
}
