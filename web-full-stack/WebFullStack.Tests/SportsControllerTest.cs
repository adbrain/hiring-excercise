using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adbrain.WebFullStack.Controllers;
using Adbrain.WebFullStack.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebFullStack.Tests
{
    [TestClass]
    public class SportsControllerTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task DomainValidation()
        {
            SportsController controller = new SportsController();
            await controller.Get("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task DomainValidation_2()
        {
            SportsController controller = new SportsController();
            await controller.Get(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task DomainValidation_3()
        {
            SportsController controller = new SportsController();
            await controller.Get("asdasd");
        }

        [TestMethod]
        public async Task AllPostsShouldHaveSameDomain()
        {
            SportsController controller = new SportsController(new PostRepositoryMocked());
            IEnumerable<dynamic> posts = await controller.Get("imgur.com");

            foreach (dynamic postGroup in posts)
            {
                foreach (PostDTO post in postGroup.items)
                {
                    if (!post.url.Contains("imgur.com"))
                    {
                        Assert.Fail("domain filtering is wrong");
                    }
                }
            }
        }

        [TestMethod]
        public async Task AllGroupsShouldHaveAtLeastOneItem()
        {
            SportsController controller = new SportsController(new PostRepositoryMocked());
            IEnumerable<dynamic> result = await controller.Get("imgur.com");

            foreach (dynamic postGroup in result)
            {
                if(!(postGroup.items as IEnumerable<PostDTO>).Any())                
                    Assert.Fail("grouping is wrong");
            }
        }

    }
}
