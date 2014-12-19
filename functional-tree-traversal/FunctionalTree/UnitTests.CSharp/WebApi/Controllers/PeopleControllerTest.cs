using Adbrain.WebApi.Controllers;
using Adbrain.WebApi.Models.Json;
using Adbrain.WebApi.Models.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Adbrain.UnitTests.CSharp.WebApi.Controllers
{
    [TestFixture]
    public class PeopleControllerTest
    {
        [Test]
        public void Get_ForExistingPerson_ReturnsThePerson()
        {
            // Arrange
            var person = new Person { Name = "Name", Age = 18 };
            var mockPersonService = new Mock<IPersonService>();
            mockPersonService.Setup(x => x.Find(person.Name, person.Age)).Returns(person);
            var peopleController = CreateController(mockPersonService.Object);

            // Act
            var response = peopleController.Get(person.Name, person.Age);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Response status code should be OK.");

            Person result;
            Assert.IsTrue(response.TryGetContentValue<Person>(out result), "The response does not contain a Person.");
            Assert.AreEqual(person.Name, result.Name, "Name of person returned is not the expected.");
            Assert.AreEqual(person.Age, result.Age, "Age of person returned is not the expected.");
        }

        public void Get_ForNonExistingPerson_ReturnStatusCodeNotFound()
        {
            // Arrange
            var person = new Person { Name = "Name", Age = 18 };
            var mockPersonService = new Mock<IPersonService>();
            mockPersonService.Setup(x => x.Find(person.Name, person.Age)).Returns((Person)null);
            var peopleController = CreateController(mockPersonService.Object);

            // Act
            var response = peopleController.Get(person.Name, person.Age);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode, "Response status code should be Not Found.");
        }

        public void Post_InsertsTheNewPersonAndReturnsOK()
        {
            // Arrange
            var person = new Person { Name = "Name", Age = 18 };
            var mockPersonService = new Mock<IPersonService>();
            Person insertedPerson = null;
            mockPersonService.Setup(x => x.Insert(person.Name, person.Age))
                .Callback<string, int>((name, age) => insertedPerson = new Person { Name = name, Age = age });
            var peopleController = CreateController(mockPersonService.Object);

            // Act
            var response = peopleController.Post(person.Name, person.Age);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Response status code should be OK.");
            Assert.IsNotNull(insertedPerson, "Person was not saved using the service.");
            Assert.AreEqual(person.Name, insertedPerson.Name, 
                "Name of inserted person returned is not the expected.");
            Assert.AreEqual(person.Age, insertedPerson.Age, 
                "Age of inserted person returned is not the expected.");
        }

        private PeopleController CreateController(IPersonService personService)
        {
            var controller = new PeopleController(personService);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller;
        }
    }
}
