using AdBrainTask.DataModels;
using System.Collections.Generic;

namespace AdBrainTask.DataAccess
{
    public interface IRedditClient
    {
        IList<SportPost> GetSports();
    }
}