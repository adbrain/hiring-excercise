using Adbrain.DataAccess.DbContexts;
using Adbrain.DataAccess.Entities;
using Adbrain.DataAccess.Repositories;
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
                var currentNode = new PersonNode { LeftChild = _repository.GetHead() };
                bool goLeft = true;
                while ((goLeft && currentNode.LeftChild != null) ||
                       (!goLeft && currentNode.RightChild != null))
                {
                    currentNode = goLeft ? currentNode.LeftChild : currentNode.RightChild;
                    goLeft = person.Age <= currentNode.Age;
                }

                _repository.Add(person);
                
                // I attach the new node to its parent. 
                if (goLeft)
                {
                    currentNode.LeftChild = person;
                }
                else
                {
                    currentNode.RightChild = person;
                }

                _dbContext.Save();
            }


            _dbContext.Save();
        }
    }
}