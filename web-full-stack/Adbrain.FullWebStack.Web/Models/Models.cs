using System;
using System.Collections.Generic;
namespace Adbrain.FullWebStack.Web.Models
{
    public class PostsGroup
    {
        public string Author { get; set; }
        public List<Post> Items { get; set; }
    }

    public class Post
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Permalink { get; set; }
    }
}