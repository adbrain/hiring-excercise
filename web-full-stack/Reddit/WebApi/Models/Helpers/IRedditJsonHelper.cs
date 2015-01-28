using Adbrain.Reddit.WebApi.Models.JsonObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.WebApi.Models.Helpers
{
    public interface IRedditJsonHelper
    {
        List<RedditItem> ExtractItems(string redditResponse);
    }
}
