using Adbrain.Reddit.DataAccess.DbContexts;
using Adbrain.Reddit.DataAccess.Entities;
using Adbrain.Reddit.DataAccess.Repositories;
using Adbrain.Reddit.DataAccess.Wrappers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.UnitTests.DataAccess.Repositories
{
    [TestFixture]
    public class SportsDataRepositoryTest
    {
        private DateTime _now;
        private IQueryable<SportsData> _data;
        private ISportsDataRepository _sportsDataRepository;
        private SportsData _savedSportsData;

        [TestFixtureSetUp]
        public void SetupRepository()
        {
            _now = DateTime.Now;
            var mockClock = new Mock<IClock>();
            mockClock.Setup(x => x.Now).Returns(_now);

            _data = new List<SportsData>
            {
                new SportsData { Id = 2, SavedOn = _now.AddMinutes(-2) },
                new SportsData { Id = 1, SavedOn = _now.AddMinutes(-3) },
                new SportsData { Id = 3, SavedOn = _now.AddMinutes(-1) }
            }.AsQueryable();

            var mockDbContext = new Mock<ISqlDbContext>();
            mockDbContext.Setup(x => x.Set<SportsData>()).Returns(ToDbSet(_data));
            // Save the data passed to the mock context in _savedSportsData
            mockDbContext.Setup(x => x.Set<SportsData>().Add(It.IsAny<SportsData>())).Callback<SportsData>(x => _savedSportsData = x);

            _sportsDataRepository = new SportsDataRepository(mockClock.Object, mockDbContext.Object);
        }

        [Test]
        public void GetLatest_ReturnsTheDataWithHighestId()
        {
            var highestId = _data.OrderByDescending(x => x.Id).First().Id;

            var latest = _sportsDataRepository.GetLatest();
            
            Assert.NotNull(latest, "Latest data object is null.");
            Assert.AreEqual(highestId, latest.Id, "The id of the latest data is not the highest.");
        }

        [Test]
        public void Save_SetsSavedOnToTheDateTimeProvidedByTheClock()
        {
            var notNow = _now.AddDays(-1);
            var dataToSave = new SportsData
            {
                SavedOn = notNow
            };
            _savedSportsData = null;

            _sportsDataRepository.Save(dataToSave);

            Assert.AreSame(dataToSave, _savedSportsData, "Data were not saved by the context.");
            Assert.AreEqual(_savedSportsData.SavedOn, _now, 
                "SavedOn property on data was not set to the one provided by the clock.");
        }

        private DbSet<SportsData> ToDbSet(IQueryable<SportsData> data)
        {
            var mockSet = new Mock<DbSet<SportsData>>();
            mockSet.As<IQueryable<SportsData>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<SportsData>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<SportsData>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<SportsData>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet.Object;
        }
    }
}
