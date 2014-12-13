using Adbrain.DataAccess.DbContexts;
using Adbrain.DataAccess.Entities;
using Adbrain.DataAccess.Repositories;
using Adbrain.WebApi.Models.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.UnitTests.CSharp
{
    [TestFixture]
    public class PersonServiceTest
    {
        private IPersonService _personService;
        private Mock<ISqlDbContext> _mockSqlDbContext;
        private Mock<IPersonNodeRepository> _mockPersonNodeRepository;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _mockSqlDbContext = new Mock<ISqlDbContext>();
            _mockPersonNodeRepository = new Mock<IPersonNodeRepository>();
            _personService = new PersonService(
                _mockSqlDbContext.Object, 
                _mockPersonNodeRepository.Object);
            SetupTestData();
        }

        [Test]
        [TestCase("Five", 5, true)]
        [TestCase("Six", 6, false)]
        [TestCase("Ten", 10, true)]
        [TestCase("Ten'", 10, true)]
        [TestCase("Eleven", 11, false)]
        [TestCase("Twenty", 20, true)]
        [TestCase("Thirty", 30, true)]
        [TestCase("Forty", 40, true)]
        [TestCase("Fifty", 50, false)]
        public void FindReturnsTheCorrectNodeOrNullIfNonExistent(string name, int age, bool shouldExist)
        {
            var person = _personService.Find(name, age);

            Assert.IsTrue(shouldExist ^ (person == null), 
                shouldExist ? "Person should be null." : "Person should not be null");
            
            if (shouldExist)
            {
                Assert.AreEqual(name, person.Name, "Name is not the expected.");
                Assert.AreEqual(age, person.Age, "Age is not the expectd.");
            }
        }

        private void SetupTestData()
        {
            var tree = new PersonNode
            {
                Name = "Ten",
                Age = 10,
                LeftChild = new PersonNode
                {
                    Name = "Ten'",
                    Age = 10,
                    LeftChild = new PersonNode { Name = "Five", Age = 5 }
                },
                RightChild = new PersonNode
                {
                    Name = "Thirty",
                    Age = 30,
                    LeftChild = new PersonNode { Name = "Twenty", Age = 20 },
                    RightChild = new PersonNode { Name = "Forty", Age = 40 }
                }
            };

            _mockPersonNodeRepository.Setup(x => x.GetHead()).Returns(tree);
            _mockPersonNodeRepository.Setup(x => x.IsEmpty()).Returns(false);
        }
    }
}
