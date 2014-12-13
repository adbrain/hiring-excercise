using Adbrain.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adbrain.WebApi.Models.Json
{
    public class Person
    {
        public Person(PersonNode node)
        {
            Name = node.Name;
            Age = node.Age;
        }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}