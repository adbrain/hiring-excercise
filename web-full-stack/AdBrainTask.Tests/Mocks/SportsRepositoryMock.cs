using AdBrainTask.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace AdBrainTask.Tests.Mocks
{
    public class SportsRepositoryMock : ISportsRepository
    {
        public void DeleteMany(IEnumerable<AdBrainTask.DataModels.Sport> sports)
        {
            this.mockSportsCollection = null;
        }

        public void AddMany(IEnumerable<AdBrainTask.DataModels.Sport> sports)
        {
            this.mockSportsCollection = sports;
        }

        public IQueryable<DataModels.Sport> GetSports()
        {
            if (this.mockSportsCollection == null)
            {
                this.mockSportsCollection = new List<AdBrainTask.DataModels.Sport>();
            }

            return this.mockSportsCollection.AsQueryable();
        }

        private IEnumerable<AdBrainTask.DataModels.Sport> mockSportsCollection; 
    }
}
