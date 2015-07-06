using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedditRetriever.Models
{
    public class PostJsonModel
    {
        [Key]
        public string Key { get; set; }
        public string Json { get; set; }
    }
}