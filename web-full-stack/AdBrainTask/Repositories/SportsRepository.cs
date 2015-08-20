using AdBrainTask.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace AdBrainTask.Repositories
{
    public class SportsRepository : ISportsRepository
    {
        public SportsRepository()
        {
            this.dbContext = new AdBrainContext();
        }

        public void DeleteMany(IEnumerable<AdBrainTask.DataModels.Sport> sports)
        {
            this.dbContext.Sports.RemoveRange(sports);
            this.dbContext.SaveChanges();
        }

        public void AddMany(IEnumerable<AdBrainTask.DataModels.Sport> sports)
        {
            this.dbContext.Sports.AddRange(sports);
            this.dbContext.SaveChanges();
        }

        public IQueryable<AdBrainTask.DataModels.Sport> GetSports()
        {
            return this.dbContext.Sports.AsQueryable();
        }

        private AdBrainContext dbContext;
    }
}