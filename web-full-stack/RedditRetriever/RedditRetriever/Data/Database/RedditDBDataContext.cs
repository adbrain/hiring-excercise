using RedditRetriever.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RedditRetriever.Data.Database
{
    public class RedditDBDataContext : DbContext
    {
        public RedditDBDataContext() : base() { }
        public DbSet<PostJsonModel> RedditPostsJson { get; set; }
    }
}