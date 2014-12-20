using Adbrain.Reddit.WebApi.Models.JsonObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.WebApi.Models.Services
{
    public interface IRedditSportsService
    {
        Task<IList<RedditAuthor>> GetForDomainByAuthor(string domain);
    }
}
