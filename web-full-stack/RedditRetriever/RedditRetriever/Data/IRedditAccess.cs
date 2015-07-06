using RedditRetriever.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditRetriever.Data
{
    public interface IRedditAccess
    {
        string RetrieveUrlWithDomain(string domain);
        Task<IEnumerable<Post>> GetPathAsync(string path);
    }
}
