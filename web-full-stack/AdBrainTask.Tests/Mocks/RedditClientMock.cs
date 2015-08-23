using AdBrainTask.DataAccess;
using AdBrainTask.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdBrainTask.Tests.Mocks
{
    public class RedditClientMock : IRedditClient
    {
        public RedditClientMock(IList<SportPost> mockedSports)
        {
            this.mockedSports = mockedSports;
        }

        public async Task<IList<SportPost>> GetSports()
        {
            return await Task.Run(() => this.mockedSports);
        }

        private IList<SportPost> mockedSports;
    }
}
