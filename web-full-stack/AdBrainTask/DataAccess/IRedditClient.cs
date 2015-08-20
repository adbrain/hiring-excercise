using System.Collections.Generic;

namespace AdBrainTask.DataAccess
{
    public interface IRedditClient
    {
        IList<AdBrainTask.DataModels.Sport> GetSports();
    }
}