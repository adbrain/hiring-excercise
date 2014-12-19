using Adbrain.Reddit.DataAccess.Wrappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.UnitTests.DataAccess.Wrappers
{
    [TestFixture]
    public class SystemClockTest
    {
        [Test]
        public void Now_GivesTheActualSystemTime()
        {
            var clock = new SystemClock();

            var systemTimeBefore = DateTime.Now;
            var clockTime = clock.Now;
            var systemTimeAfter = DateTime.Now;

            Assert.IsTrue(systemTimeBefore <= clockTime, "Clock time must be after the measurement taken before.");
            Assert.IsTrue(clockTime <= systemTimeAfter, "Clock time must be before the measurement taken after.");
        }
    }
}
