using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adbrain.Reddit.WebApi.Models.JsonObjects
{
    public class RedditItem
    {
        public string Domain { get; set; }

        public string Author { get; set; }

        public string Id { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string Title { get; set; }
        
        public string Permalink { get; set; }
    }
}