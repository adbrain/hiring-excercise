using AdBrainTask.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdBrainTask.DataAccess
{
    public interface IRedditClient
    {
        Task<IList<SportPost>> GetSports();
    }
}