using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Inbucket.CSharp.Client.Test
{
    /// <summary>
    /// Mock class for the Message handler {sendAsync(...)} for the {HttpClient}.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpMessageHandlerMock<T> : HttpMessageHandler
    {
        private readonly T ReturnData;

        public HttpMessageHandlerMock(T returnData)
            : base()
        {
            this.ReturnData = returnData;
        }

        public HttpMessageHandlerMock()
            : base()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(this.ReturnData))
            };

            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await Task.FromResult(responseMessage);
        }
    }
}
