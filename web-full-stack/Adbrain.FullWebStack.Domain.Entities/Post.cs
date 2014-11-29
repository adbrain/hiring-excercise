using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.FullWebStack.Domain.Entities
{
    public class Post
    {
        public string Id { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Domain { get; set; }
        public string Permalink { get; set; }
    }
}
