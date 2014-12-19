using Adbrain.Reddit.DataAccess.DbContexts;
using Adbrain.Reddit.DataAccess.Entities;
using Adbrain.Reddit.DataAccess.Wrappers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.DataAccess.Repositories
{
    public class SportsDataRepository : ISportsDataRepository
    {
        private readonly IClock _clock;
        private readonly ISqlDbContext _dbContext;
        private readonly DbSet<SportsData> _dbSet;

        public SportsDataRepository(
            IClock clock,
            ISqlDbContext dbContext)
        {
            _clock = clock;
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<SportsData>();
        }

        public void Save(SportsData entity)
        {
            entity.SavedOn = _clock.Now;
            _dbSet.Add(entity);
            _dbContext.Save();
        }

        public SportsData GetLatest()
        {
            return _dbSet.OrderByDescending(x => x.Id).FirstOrDefault();
        }
    }
}
