using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.Reddit.WebApi.Models.Wrappers
{
    public interface IWebClientWrapper
    {
        Task<string> DownloadStringTaskAsync(string address);
    }
}
