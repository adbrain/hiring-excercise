using AdBrainTask.DataAccess;
using AdBrainTask.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace AdBrainTask.Tests.Mocks
{
    public class RedditClientMock : IRedditClient
    {
        public RedditClientMock(IList<SportPost> mockedSports)
        {
            this.mockedSports = mockedSports;
        }

        public IList<SportPost> GetSports()
        {
            return this.mockedSports.ToList();
        }

        private IList<SportPost> mockedSports;
    }
}
