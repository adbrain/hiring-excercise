using Adbrain.Reddit.WebApi.Models.Wrappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.UnitTests.WebApi.Models.Helpers
{
    [TestFixture]
    public class AppSettingsHelperTest
    {
        private NameValueCollection _appSettings = new NameValueCollection();
        private IAppSettingsHelper _helper;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            _appSettings.Add("string", "string");
            _helper = new AppSettingsHelper(_appSettings);
        }

        [Test]
        public void GetString_ForNonExistingKey_ReturnsNull()
        {
            // Arrange
            var key = "non-existing key";

            // Act
            var result = _helper.GetString(key);

            // Assert
            Assert.IsNull(result, "The result should be null.");
        }

        [Test]
        public void GetString_ForExistingKey_ReturnsTheValue()
        {
            // Arrange
            var key = "string";
            var expected = "string";

            // Act
            var result = _helper.GetString(key);

            // Assert
            Assert.AreEqual(expected, result, "The result should be the value for the key.");
        }
    }

}
