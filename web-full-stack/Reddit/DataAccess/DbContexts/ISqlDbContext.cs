using Adbrain.Reddit.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.DataAccess.DbContexts
{
    public interface ISqlDbContext
    {
        Database Database { get; }

        IDbSet<TEntity> GetSet<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync();
    }
}
