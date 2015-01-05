using System.Data.Entity;

namespace AdbrainTest.Models
{
    public class EntityModel : DbContext
    {
        public virtual DbSet<RedditListingsModel> SportListings { get; set; }

        public EntityModel()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EntityModel>());
        }
    }
}