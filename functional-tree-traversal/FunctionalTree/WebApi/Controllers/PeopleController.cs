using Adbrain.WebApi.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Adbrain.WebApi.Controllers
{
    public class PeopleController : ApiController
    {
        private readonly IPersonService _service;

        public PeopleController(IPersonService service)
        {
            _service = service;
        }

        // GET api/people
        public HttpResponseMessage Get(string name, int age)
        {
            var person = _service.Find(name, age);
            if (person != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, person);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // POST api/people
        public HttpResponseMessage Post(string name, int age)
        {
            _service.Insert(name, age);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
