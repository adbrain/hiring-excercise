using AdBrainTask.DataModels;
using System.Data.Entity;

namespace AdBrainTask.DataAccess
{
    public class AdBrainContext : DbContext
    {
        /// <summary>
        /// Static constructor - initialize the database
        /// </summary>
        static AdBrainContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AdBrainContext>());
        }

        /// <summary>
        /// Initializes a new instance of of the AdBrain db context
        /// </summary>
        public AdBrainContext() : base("AdBrainContext") { }

        public DbSet<Sport> Sports { get; set; }
    }
}