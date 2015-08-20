using System.Collections.Generic;
using System.Linq;

namespace AdBrainTask.Repositories
{
    public interface ISportsRepository
    {
        void DeleteMany(IEnumerable<AdBrainTask.DataModels.Sport> sports);

        void AddMany(IEnumerable<AdBrainTask.DataModels.Sport> sports);

        IQueryable<AdBrainTask.DataModels.Sport> GetSports();
    }
}