using Adbrain.DataAccess.DbContexts;
using Adbrain.DataAccess.Entities;
using Adbrain.DataAccess.Repositories;
using Adbrain.FunctionalTree.Engine;
using Adbrain.WebApi.Models.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adbrain.WebApi.Models.Services
{
    public class PersonService : IPersonService
    {
        private readonly ISqlDbContext _dbContext;
        private readonly IPersonNodeRepository _repository;

        public PersonService(
            ISqlDbContext dbContext,
            IPersonNodeRepository repository)
        {
            _dbContext = dbContext;
            _repository = repository;
        }

        public void Insert(string name, int age)
        {
            var person = new PersonNode
            {
                Name = name,
                Age = age
            };

            if (_repository.IsEmpty())
            {
                // This is the first node, I simply add it.
                _repository.Add(person);
            }
            else
            {
                // I traverse the tree to find the node that will become 
                // the direct parent of the new node.
                var currentNode = _repository.GetHead();
                bool goLeft = person.Age <= currentNode.Age;
                while ( (goLeft && currentNode.LeftChildId.HasValue) ||
                        (!goLeft && currentNode.RightChildId.HasValue) )
                {
                    currentNode = goLeft ? currentNode.LeftChild : currentNode.RightChild;
                    goLeft = person.Age <= currentNode.Age;
                }

                // I add the new node and attach it to its parent. 
                _repository.Add(person);
                if (goLeft)
                {
                    currentNode.LeftChild = person;
                }
                else
                {
                    currentNode.RightChild = person;
                }
            }

            _dbContext.Save();
        }

        public Person Find(string name, int age)
        {
            var head = _repository.GetHead();
            var personNode = PersonTree.Find(name, age, head);
            if (personNode == null)
                return null;
            return new Person(personNode);
        }
    }
}