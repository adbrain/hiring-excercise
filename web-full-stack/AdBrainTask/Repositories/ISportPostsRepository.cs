using AdBrainTask.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace AdBrainTask.Repositories
{
    public interface ISportPostsRepository
    {
        void DeleteMany(IEnumerable<SportPost> sports);

        void AddMany(IEnumerable<SportPost> sports);

        IQueryable<SportPost> GetSports();
    }
}