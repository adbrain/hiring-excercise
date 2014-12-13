using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.DataAccess.Entities
{
    public class PersonNode
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int? LeftChildId { get; set; }

        public int? RightChildId { get; set; }

        public PersonNode LeftChild { get; set; }

        public PersonNode RightChild { get; set; }
    }
}