using Adbrain.FullWebStack.Domain.Entities;
using Adbrain.FullWebStack.Infrastructure.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.FullWebStack.Infrastructure.Data
{
    public class EFDbContext : DbContext, IDbContext
    {
        public EFDbContext() : base("Posts")
        {
            Database.SetInitializer<EFDbContext>(new DropCreateDatabaseAlways<EFDbContext>());
        }

        public DbSet<Post> Posts { get; set; }

        public void SetAsAdded(Post post)
        {
            UpdateEntityState(post, EntityState.Added);
        }

        public void SetAsModified(Post post)
        {
            UpdateEntityState(post, EntityState.Modified);
        }

        private void UpdateEntityState(Post post, EntityState entityState)
        {
            var dbEntityEntry = GetDbEntityEntrySafely(post);
            dbEntityEntry.State = entityState;
        }

        private DbEntityEntry GetDbEntityEntrySafely(Post post)
        {
            var dbEntityEntry = Entry<Post>(post);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set<Post>().Attach(post);
            }
            return dbEntityEntry;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PostConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
