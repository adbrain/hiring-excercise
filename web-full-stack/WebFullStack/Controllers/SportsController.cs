using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Adbrain.WebFullStack.DataAccessLayer;
using Adbrain.WebFullStack.Models;

namespace Adbrain.WebFullStack.Controllers
{
    public class SportsController : ApiController
    {
        private IRepository<Post> mPostRepository;

        public SportsController()
        {
            mPostRepository = new PostRepository();
        }
        public SportsController(IRepository<Post> postRepo)
        {
            mPostRepository = postRepo;
        }


        public async Task<IEnumerable<dynamic>> Get(string domain)
        {
            // 0. Validation
            if (string.IsNullOrEmpty(domain))
                throw new ArgumentException("Domain name is not valid");
            if(!domain.Contains("."))
                throw new ArgumentException("Domain name is not valid");


            // 1. Retrieve latest 100 /sports posts
            Post[] posts;
            try
            {
                posts = await RedditHandler.GetLatestPosts("sports", 100);
            }
            catch (WebException ex)
            {
                throw new Exception("Reddit Service is not available right now. Please try again later.");
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FormatException();
            }


            // 2. Set the batch number for retrieve later based on it
            string batch = Guid.NewGuid().ToString();
            Parallel.For(0, posts.Length, i =>
                {
                    posts[i].BatchNumber = batch;
                });


            // 3. Save all the batch in database
            await mPostRepository.SaveAll(posts);


            // 4. Retrieve batch posts from db
            IList<Post> allPosts = mPostRepository.FromBatchNumber(batch);


            // 5. group by and make result
            var query = from post in allPosts
                        where post.Domain == domain
                        group post by post.Author
                        into g
                            select new
                            {
                                author = g.Key,
                                items = g.OrderByDescending(p=>p.DateTime).Select(p => PostDTO.FromPost(p))
                            };

            return query;
        }
    }
}
