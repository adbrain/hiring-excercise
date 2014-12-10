using System;
using DataAccessLayer;

namespace Adbrain.WebFullStack.Models
{
    public class Post : IDbObject
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Url { get; set; }
        public string Domain { get; set; }
        public string PermanentLink { get; set; }
        public string Subreddit { get; set; }
        public string RedditId { get; set; }
        public string BatchNumber { get; set; }
    }
}