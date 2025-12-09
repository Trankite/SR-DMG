using StarRailDamage.Source.Web.Request.Builder.Abstraction;
using System.Net.Http;
using System.Net.Http.Headers;

namespace StarRailDamage.Source.Web.Request
{
    public class HttpRequestMessageBuilder : IHttpRequestUriBuilder, IHttpMethodBuilder, IHttpRequestOptionsBuilder, IHttpHeadersBuilder<HttpHeaders>, IHttpContentBuilder
    {
        public Uri? RequestUri
        {
            get => HttpRequestMessage.RequestUri;
            set => HttpRequestMessage.RequestUri = value;
        }

        public HttpMethod Method
        {
            get => HttpRequestMessage.Method;
            set => HttpRequestMessage.Method = value;
        }

        public HttpRequestOptions Options { get => HttpRequestMessage.Options; }

        public HttpHeaders Headers { get => HttpRequestMessage.Headers; }

        public HttpContent? Content
        {
            get => HttpRequestMessage.Content;
            set => HttpRequestMessage.Content = value;
        }

        public HttpRequestMessage HttpRequestMessage { get; set; } = new();

        public IHttpContentSerializer HttpContentSerializer { get; }

        public HttpRequestMessageBuilder()
        {
            HttpContentSerializer = new JsonHttpContentSerializer();
        }

        public HttpRequestMessageBuilder(IHttpContentSerializer httpContentSerializer)
        {
            HttpContentSerializer = httpContentSerializer;
        }

        public HttpRequestMessageBuilder(IHttpContentSerializer httpContentSerializer, HttpRequestMessage httpRequestMessage) : this(httpContentSerializer)
        {
            HttpRequestMessage = httpRequestMessage;
        }

        public override string ToString()
        {
            return HttpRequestMessage.ToString();
        }
    }
}