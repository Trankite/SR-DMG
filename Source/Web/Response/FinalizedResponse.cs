using System.Net.Http.Headers;
using System.Runtime.ExceptionServices;

namespace StarRailDamage.Source.Web.Response
{
    public class FinalizedResponse<TResult>
    {
        public TResult? Body { get; }

        public HttpResponseHeaders? Headers { get; }

        public ExceptionDispatchInfo? Exception { get; }

        public FinalizedResponse() { }

        public FinalizedResponse(HttpResponseHeaders? headers)
        {
            Headers = headers;
        }

        public FinalizedResponse(HttpResponseHeaders? headers, TResult? body) : this(headers)
        {
            Body = body;
        }

        public FinalizedResponse(HttpResponseHeaders? headers, ExceptionDispatchInfo? exception) : this(headers)
        {
            Exception = exception;
        }
    }
}