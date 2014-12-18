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
    public class RawDataRepositoryTest
    {
        private DateTime _now;
        private IQueryable<RawData> _data;
        private IRawDataRepository _rawDataRepository;
        private RawData _savedRawData;

        [TestFixtureSetUp]
        public void SetupRepository()
        {
            _now = DateTime.Now;
            var mockClock = new Mock<IClock>();
            mockClock.Setup(x => x.Now).Returns(_now);

            _data = new List<RawData>
            {
                new RawData { Id = 2 },
                new RawData { Id = 1 },
                new RawData { Id = 3 }
            }.AsQueryable();

            var mockDbContext = new Mock<ISqlDbContext>();
            mockDbContext.Setup(x => x.Set<RawData>()).Returns(CreateDbSet(_data));
            // Save the data passed to the mock context in _savedRawData
            mockDbContext.Setup(x => x.Set<RawData>().Add(It.IsAny<RawData>())).Callback<RawData>(x => _savedRawData = x);

            _rawDataRepository = new RawDataRepository(mockClock.Object, mockDbContext.Object);
        }

        [Test]
        public void GetLatest_ReturnsTheDataWithHighestId()
        {
            var highestId = _data.OrderByDescending(x => x.Id).First().Id;
            
            var latest = _rawDataRepository.GetLatest();
            
            Assert.NotNull(latest, "Latest data object is null.");
            Assert.AreEqual(highestId, latest.Id, "The id of the latest data is not the highest.");
        }

        [Test]
        public void Save_SetsSavedOnToTheDateTimeProvidedByTheClock()
        {
            var notNow = _now.AddDays(-1);
            var dataToSave = new RawData
            {
                SavedOn = notNow
            };
            _savedRawData = null;

            _rawDataRepository.Save(dataToSave);

            Assert.AreSame(dataToSave, _savedRawData, "Data were not saved by the context.");
            Assert.AreEqual(_savedRawData.SavedOn, _now, 
                "SavedOn property on data was not set to the one provided by the clock.");
        }

        private DbSet<RawData> CreateDbSet(IQueryable<RawData> data)
        {
            var mockSet = new Mock<DbSet<RawData>>();
            mockSet.As<IQueryable<RawData>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<RawData>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<RawData>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<RawData>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet.Object;
        }
    }
}
