using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POCO;

namespace DAL
{

    #region Base context class definition
    public class BaseContext<TContext> : DbContext where TContext : DbContext
    {
        protected BaseContext()
            : base("name=LocalHostDB")
        {
            Database.SetInitializer<TContext>(new CreateDatabaseIfNotExists<TContext>());
        }
    }
    #endregion

    #region Custom contexts
    public class SportsContext : BaseContext<SportsContext>
    {
        public DbSet<RedditPost> RedditPosts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("sports");
            modelBuilder.Configurations.Add(new RedditPostConfiguration());
        }
    }
    #endregion
}