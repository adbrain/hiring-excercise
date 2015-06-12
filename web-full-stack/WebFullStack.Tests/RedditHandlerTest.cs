using System;
using System.Threading.Tasks;
using Adbrain.WebFullStack;
using Adbrain.WebFullStack.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebFullStack.Tests
{
    [TestClass]
    public class RedditHandlerTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SubredditParamEmptyValidation()
        {
            await RedditHandler.GetLatestPosts("", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SubredditParamNullValidation()
        {
            await RedditHandler.GetLatestPosts(null, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task CountParamZeroValidation()
        {
            await RedditHandler.GetLatestPosts("sports", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task CountParamNegativeValidation()
        {
            await RedditHandler.GetLatestPosts("sports", -1);
        }

        [TestMethod]
        public async Task CountTest()
        {
            Post[] posts = await RedditHandler.GetLatestPosts("sports", 100);
            Assert.AreEqual(100, posts.Length);
        }
    }
}
