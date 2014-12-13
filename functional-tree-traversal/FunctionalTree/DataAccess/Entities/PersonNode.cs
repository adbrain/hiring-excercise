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
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int? LeftChildId { get; set; }

        public int? RightChildId { get; set; }

        [ForeignKey("LeftChildId")]
        public PersonNode LeftChild { get; set; }

        [ForeignKey("RightChildId")]
        public PersonNode RightChild { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; } 
    }
}