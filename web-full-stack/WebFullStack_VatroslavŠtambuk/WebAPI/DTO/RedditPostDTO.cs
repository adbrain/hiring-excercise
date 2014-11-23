using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAPI.Controllers
{
    public class RedditPostAuthorDTO
    {
        public string author { get; set; }

        public List<RedditPostItemDTO> items { get; set; }
    }

    public class RedditPostItemDTO
    {
        public string id { get; set; }
        public DateTime createdDate { get; set; }
        public string title { get; set; }
        public string permalink { get; set; }
    }
}
