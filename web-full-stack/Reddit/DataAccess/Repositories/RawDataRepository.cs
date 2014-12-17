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
    public class RawDataRepository : IRawDataRepository
    {
        private readonly IClock _clock;
        private readonly ISqlDbContext _dbContext;
        private readonly DbSet<RawData> _dbSet;

        public RawDataRepository(
            IClock clock,
            ISqlDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<RawData>();
        }

        public void Save(RawData entity)
        {
            entity.SavedOn = _clock.Now;
            _dbSet.Add(entity);
            _dbContext.Save();
        }

        public RawData GetLatest()
        {
            return _dbSet.OrderByDescending(x => x.Id).FirstOrDefault();
        }
    }
}
