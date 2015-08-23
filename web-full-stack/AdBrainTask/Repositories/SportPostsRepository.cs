using AdBrainTask.DataAccess;
using AdBrainTask.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace AdBrainTask.Repositories
{
    public class SportPostsRepository : ISportPostsRepository
    {
        public SportPostsRepository()
        {
            this.dbContext = new AdBrainContext();
        }

        public void DeleteMany(IEnumerable<SportPost> sports)
        {
            this.dbContext.Sports.RemoveRange(sports);
            this.dbContext.SaveChanges();
        }

        public void AddMany(IEnumerable<SportPost> sports)
        {
            this.dbContext.Sports.AddRange(sports);
            this.dbContext.SaveChanges();
        }

        public IQueryable<SportPost> GetSports()
        {
            return this.dbContext.Sports.AsQueryable();
        }

        private AdBrainContext dbContext;
    }
}