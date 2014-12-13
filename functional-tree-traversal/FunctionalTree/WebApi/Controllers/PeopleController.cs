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
        public string Get(string name, int age)
        {
            throw new NotImplementedException();
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
