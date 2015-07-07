using NUnit.Framework;
using RedditRetriever.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditRetriever.Tests
{
    [TestFixture]
    public class DateTimeUnixTimestampConverterTests
    {
        [Test]
        public void Converter_Zero_Epoch()
        {
            var expected = new DateTime(1970, 1, 1);
            var actual = DateTimeUnixConverter.FromUnixTimestamp(0);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Converter_Current_CurrentDate()
        {
            var expected = DateTime.Parse("2015-07-07T14:19:56+00:00");
            var actual = DateTimeUnixConverter.FromUnixTimestamp(1436282396);
            Assert.AreEqual(expected, actual);
        }
    }
}
