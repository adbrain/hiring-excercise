using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.DataAccess.Entities
{
    public class PersonNode
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int Age { get; set; }

        public virtual int? LeftChildId { get; set; }

        public virtual int? RightChildId { get; set; }

        public virtual PersonNode LeftChild { get; set; }

        public virtual PersonNode RightChild { get; set; }

        [Timestamp]
        public virtual Byte[] Timestamp { get; set; } 
    }
}