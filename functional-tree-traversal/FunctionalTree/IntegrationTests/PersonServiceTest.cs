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

        private List<Person> people = new List<Person>
        {
            new Person { Name = "Ten", Age = 10 },
            new Person { Name = "Ten'", Age = 10 },
            new Person { Name = "Five", Age = 5 },
            new Person { Name = "Thirty", Age = 30 },
            new Person { Name = "Twenty", Age = 20 },
            new Person { Name = "Fourty", Age = 40 }
        };

        [Test]
        public void AllPeopleInsertedAreReturnedWhenQueried()
        {
            people.ForEach(p => _personService.Insert(p.Name, p.Age));

            foreach(var p in people)
            {
                var actualPerson = _personService.Find(p.Name, p.Age);
                string messageStart = 
                    String.Format("When quering for name {0} and age {1} ", p.Name, p.Age);
                Assert.NotNull(actualPerson, messageStart + " no person found.");
                Assert.AreEqual(p.Name, actualPerson.Name, 
                    messageStart + " person returned has wrong name.");
                Assert.AreEqual(p.Age, actualPerson.Age, 
                    messageStart + " person returned has wrong age.");
            }
        }
    }
}
