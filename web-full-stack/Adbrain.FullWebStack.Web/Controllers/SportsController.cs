using Adbrain.FullWebStack.Service.Interfaces;
using Adbrain.FullWebStack.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Adbrain.FullWebStack.Web.Controllers
{
    public class SportsController : ApiController
    {
        private readonly IPostService _postService;

        public SportsController(IPostService postService)
        {
            _postService = postService;
        }

        [Route("sports")]
        public async Task<IEnumerable<PostsGroup>> GetSports([FromUri] string domain)
        {
            var posts = await _postService.GetGroupedByAuthor(domain);
            
            return posts.Select(grouping => new PostsGroup {
                Author = grouping.Key,
                Items = grouping.Select(post => new Post {
                    Id = post.Id,
                    Title = post.Title,
                    CreatedDate = post.CreatedUtc,
                    Permalink = post.Permalink
                }).ToList()
            });
        }
    }
}
