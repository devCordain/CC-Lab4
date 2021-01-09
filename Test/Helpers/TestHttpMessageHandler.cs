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

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!messages.ContainsKey(request.RequestUri.ToString()))
            {
                throw new Exception($"The request Uri {request.RequestUri} does not match any of the PreDefined uris {messages.Keys}");
            }
            return Task.FromResult(messages[request.RequestUri.ToString()]);
        }
    }
}
