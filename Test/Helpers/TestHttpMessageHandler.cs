using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test.Helpers
{
    class TestHttpMessageHandler : HttpMessageHandler
    {
        private readonly IDictionary<string, HttpResponseMessage> messages;

        public TestHttpMessageHandler(IDictionary<string, HttpResponseMessage> messages) {
            this.messages = messages;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            if (messages.ContainsKey(request.RequestUri.ToString()))
                response = messages[request.RequestUri.ToString()] ?? new HttpResponseMessage(HttpStatusCode.NoContent);
            response.RequestMessage = request;
            return Task.FromResult(response);
        }
    }
}
