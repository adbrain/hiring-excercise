using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AdBrainTask.Tests.Mocks
{
    public class HttpMessageHandlerMock : HttpMessageHandler
    {
        public HttpMessageHandlerMock(HttpResponseMessage response)
        {
            this.response = response;
        }         

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            this.response.RequestMessage = request;
            responseTask.SetResult(this.response);

            return responseTask.Task;
        }

        private HttpResponseMessage response;
    }
}
