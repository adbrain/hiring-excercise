using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Adbrain.Reddit.WebApi.Models.Wrappers
{
    public class WebClientWrapper : IWebClientWrapper
    {
        private readonly WebClient _client = new WebClient();

        public async Task<string> DownloadStringTaskAsync(string address)
        {
            var task = _client.DownloadStringTaskAsync(address);

            await task;

            return task.Result;
        }
    }
}