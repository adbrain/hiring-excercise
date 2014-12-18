using Adbrain.DataAccess.DbContexts;
using Adbrain.DataAccess.Repositories;
using Adbrain.WebApi.Controllers;
using Adbrain.WebApi.Models.Json;
using Adbrain.WebApi.Models.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.IntegrationTests
{
    [TestFixture]
    public class PersonServiceTest : BaseIntegrationTest
    {
        private PersonService _personService;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var dbContext = new SqlDbContext();
            var repository = new PersonNodeRepository(dbContext);
            _personService = new PersonService(dbContext, repository);
        }

        [Test]
        public void AllPeopleInsertedAreReturnedWhenQueried()
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
            people.ForEach(p => _personService.Insert(p.Name, p.Age));

            foreach(var p in people)
            {
                // Act
                var actualPerson = _personService.Find(p.Name, p.Age);
                
                // Assert
                string messageStart = 
                    String.Format("When quering for name {0} and age {1} ", p.Name, p.Age);
                Assert.NotNull(actualPerson, messageStart + " no person found.");
                Assert.AreEqual(p.Name, actualPerson.Name, 
                    messageStart + " person returned has wrong name.");
                Assert.AreEqual(p.Age, actualPerson.Age, 
                    messageStart + " person returned has wrong age.");
            }
        }

        [Test]
        public void InsertAThousandPeopleAndQueryExistingAndNonExistingPerson()
        {
            // Arrange
            var allPeople = Enumerable.Range(1, 1002).Select(i => new Person { Name = i.ToString(), Age = i }).ToList();
            var missingPerson = allPeople.Single(x => x.Age == 400);
            var existingPerson = allPeople.Single(x => x.Age == 600);
            var peopleToInsert = allPeople.Where(x => x.Age != missingPerson.Age && x.Age != existingPerson.Age).ToList();
            
            var randomSeed = 9797;
            var random = new Random(randomSeed);
            // I insert people in a random order so that the tree is more balanced.
            Randomize(peopleToInsert, random).ForEach(p => _personService.Insert(p.Name, p.Age));
            // I insert the person I will look up last
            _personService.Insert(existingPerson.Name, existingPerson.Age);

            // Act
            var missingPersonQueryResult = _personService.Find(missingPerson.Name, missingPerson.Age);
            var existingPersonQueryResult = _personService.Find(existingPerson.Name, existingPerson.Age);

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
    }
}
