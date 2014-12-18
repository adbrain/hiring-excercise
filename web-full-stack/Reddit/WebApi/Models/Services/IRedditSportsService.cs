using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.WebApi.Models.Services
{
    public interface IRedditSportsService
    {
        Task<string> GetForDomain(string domain);
    }
}
