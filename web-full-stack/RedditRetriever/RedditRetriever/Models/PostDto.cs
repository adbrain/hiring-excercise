using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditRetriever.Models
{
    public class PostDto
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string PermaLink { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as PostDto;
            if(other == null)
            {
                return false;
            }
            else
            {
                return Id == other.Id && CreatedDate == other.CreatedDate && Title == other.Title && PermaLink == other.PermaLink;
            }
        }
    }
}