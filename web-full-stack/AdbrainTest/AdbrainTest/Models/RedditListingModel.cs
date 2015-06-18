using System;
using System.ComponentModel.DataAnnotations;

namespace AdbrainTest.Models
{
    public class RedditListingsModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime RetrievedDate { get; set; }
        public string JsonData { get; set; }
    }
}