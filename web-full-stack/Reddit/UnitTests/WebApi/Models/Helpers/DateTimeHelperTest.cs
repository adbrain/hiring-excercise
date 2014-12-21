using Adbrain.Reddit.WebApi.Models.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.UnitTests.WebApi.Models.Helpers
{
    [TestFixture]
    public class DateTimeHelperTest
    {
        private IDateTimeHelper _dateTimeHelper;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            _dateTimeHelper = new DateTimeHelper();
        }

        [Test]
        public void DateTimeFromSecsUtc_For0Secs_ReturnsThePosixTime()
        {
            var posixTime = _dateTimeHelper.PosixTime;
            var timeFor0secs = _dateTimeHelper.DateTimeFromSecsUtc(0);

            Assert.AreEqual(posixTime, timeFor0secs);
        }

        [Test]
        public void DateTimeFromSecsUtc_For1Sec_Adds1SecToThePosixTime()
        {
            var seconds = 1;
            var expected = _dateTimeHelper.PosixTime.AddSeconds(seconds);
            var actual = _dateTimeHelper.DateTimeFromSecsUtc(seconds);

            Assert.AreEqual(expected, actual);
        }

    }
}
