using AdBrainTask.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace AdBrainTask.Tests.Mocks
{
    public class RedditClientMock : IRedditClient
    {
        public RedditClientMock(IList<AdBrainTask.DataModels.Sport> mockedSports)
        {
            this.mockedSports = mockedSports;
        }

        public IList<AdBrainTask.DataModels.Sport> GetSports()
        {
            return this.mockedSports.ToList();
        }

        private IList<AdBrainTask.DataModels.Sport> mockedSports;
    }
}
