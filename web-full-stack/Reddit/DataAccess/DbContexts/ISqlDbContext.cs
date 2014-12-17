using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.DataAccess.DbContexts
{
    public interface ISqlDbContext
    {
        Database Database { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int Save();
    }
}
