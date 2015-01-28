using Adbrain.Reddit.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.DataAccess.Repositories
{
    public interface ISportsDataRepository
    {
        Task<int> Save(SportsData entity);

        Task<SportsData> GetLatest();
    }
}
