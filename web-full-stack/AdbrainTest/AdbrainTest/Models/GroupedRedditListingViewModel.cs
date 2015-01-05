using System.Collections.Generic;

namespace AdbrainTest.Models
{
    public class GroupedRedditListingViewModel
    {
        public string Author { get; set; }
        public List<RedditListingViewModel> Items { get; set; } 
    }
}