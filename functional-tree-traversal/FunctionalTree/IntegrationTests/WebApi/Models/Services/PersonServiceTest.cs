using Adbrain.DataAccess.DbContexts;
using Adbrain.DataAccess.Repositories;
using Adbrain.WebApi.Models.Json;
using Adbrain.WebApi.Models.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.IntegrationTests.WebApi.Models.Services
{
    [TestFixture]
    public class PersonServiceTest : BaseIntegrationTest
    {
        private PersonService _personService1;
        private PersonService _personService2;

        [SetUp]
        public void TestSetUp()
        {
            _personService1 = CreatePersonService();
            _personService2 = CreatePersonService();
        }

        [Test]
        public async Task AllPeopleInsertedAreReturnedWhenQueried()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person { Name = "Ten", Age = 10 },
                new Person { Name = "Ten'", Age = 10 },
                new Person { Name = "Five", Age = 5 },
                new Person { Name = "Thirty", Age = 30 },
                new Person { Name = "Twenty", Age = 20 },
                new Person { Name = "Fourty", Age = 40 }
            };

            foreach (var p in people)
            {
                await _personService1.Insert(p.Name, p.Age);
            }

            foreach (var p in people)
            {
                // Act
                var retrievedPerson = await _personService2.Find(p.Name, p.Age);

                // Assert
                string messageStart =
                    String.Format("When quering for name {0} and age {1} ", p.Name, p.Age);
                Assert.NotNull(retrievedPerson, messageStart + " no person found.");
                Assert.AreEqual(p.Name, retrievedPerson.Name,
                    messageStart + " person returned has wrong name.");
                Assert.AreEqual(p.Age, retrievedPerson.Age,
                    messageStart + " person returned has wrong age.");
            }
        }

        [Test]
        public async Task InsertAHundredPeopleAndQueryExistingAndNonExistingPerson()
        {
            // Arrange
            var allPeople = Enumerable.Range(1, 102).Select(i => new Person { Name = i.ToString(), Age = i }).ToList();
            var missingPerson = allPeople.Single(x => x.Age == 40);
            var existingPerson = allPeople.Single(x => x.Age == 60);
            var peopleToInsert = allPeople.Where(x => x.Age != missingPerson.Age && x.Age != existingPerson.Age).ToList();

            var randomSeed = 9797;
            var random = new Random(randomSeed);
            // I insert people in a random order so that the tree is more balanced.
            foreach (var p in Randomize(peopleToInsert, random))
            {
                await _personService1.Insert(p.Name, p.Age);
            }
            // I insert the person I will look up last
            await _personService1.Insert(existingPerson.Name, existingPerson.Age);

            // Act
            var missingPersonQueryResult = await _personService2.Find(missingPerson.Name, missingPerson.Age);
            var existingPersonQueryResult = await _personService2.Find(existingPerson.Name, existingPerson.Age);

            // Assert
            Assert.IsNull(missingPersonQueryResult, "Querying a missing person should return null.");
            Assert.IsNotNull(existingPersonQueryResult, "Querying an existin person should return non-null value.");
            Assert.AreEqual(existingPerson.Name, existingPersonQueryResult.Name,
                "Name of existing person returned is not the expected.");
            Assert.AreEqual(existingPerson.Age, existingPersonQueryResult.Age,
                "Age of existing person returned is not the expected.");
        }

        private List<Person> Randomize(List<Person> people, Random random)
        {
            return people
                .Select(x => new { P = x, R = random.Next() })
                .OrderBy(x => x.R)
                .Select(x => x.P)
                .ToList();
        }

        private PersonService CreatePersonService()
        {
            var dbContext = new SqlDbContext();
            var repository = new PersonNodeRepository(dbContext);
            var personService = new PersonService(dbContext, repository);
            return personService;
        }
    }
}
