using AdBrainTask.DataModels;
using AdBrainTask.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace AdBrainTask.Tests.Mocks
{
    public class SportPostsRepositoryMock : ISportPostsRepository
    {
        public void DeleteMany(IEnumerable<SportPost> sports)
        {
            this.mockSportsCollection = null;
        }

        public void AddMany(IEnumerable<SportPost> sports)
        {
            this.mockSportsCollection = sports;
        }

        public IQueryable<SportPost> GetSports()
        {
            if (this.mockSportsCollection == null)
            {
                this.mockSportsCollection = new List<SportPost>();
            }

            return this.mockSportsCollection.AsQueryable();
        }

        private IEnumerable<SportPost> mockSportsCollection; 
    }
}
