using System.Net.Http.Headers;
using System.Runtime.ExceptionServices;

namespace StarRailDamage.Source.Web.Response
{
    public class FinalizeResponse<TResult>
    {
        public TResult? Body { get; }

        public HttpResponseHeaders? Headers { get; }

        public ExceptionDispatchInfo? Exception { get; }

        public FinalizeResponse() { }

        public FinalizeResponse(HttpResponseHeaders? headers)
        {
            Headers = headers;
        }

        public FinalizeResponse(HttpResponseHeaders? headers, TResult? body) : this(headers)
        {
            Body = body;
        }

        public FinalizeResponse(HttpResponseHeaders? headers, ExceptionDispatchInfo? exception) : this(headers)
        {
            Exception = exception;
        }
    }
}