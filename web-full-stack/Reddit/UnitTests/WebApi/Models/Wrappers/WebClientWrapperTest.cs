using Adbrain.Reddit.WebApi.Models.Wrappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.UnitTests.WebApi.Models.Wrappers
{
    [TestFixture]
    public class WebClientWrapperTest
    {
        [Test]
        public void CanAccessGoogleHomePage()
        {
            var url = @"http://www.google.com";
            var webClient = new WebClientWrapper();

            var content = webClient.DownloadStringTaskAsync(url).Result;

            Assert.IsNotEmpty(content, "Content downloaded is empty.");
        }
    }
}
