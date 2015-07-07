using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditRetriever.Models
{
    public class UserPostsModel
    {
        public string Author { get; set; }
        public IEnumerable<PostDto> Posts { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as UserPostsModel;
            if(other == null)
            {
                return false;
            }
            else
            {
                return Author == other.Author && Posts.SequenceEqual(other.Posts);
            }
        }
    }
}