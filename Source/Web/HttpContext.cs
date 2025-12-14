using StarRailDamage.Source.Core.Abstraction;
using System.Net.Http;
using System.Runtime.ExceptionServices;

namespace StarRailDamage.Source.Web
{
    public sealed class HttpContext : IExceptionCapture, IDisposable
    {
        public required HttpClient HttpClient { get; init; }

        public CancellationToken Cancellation { get; init; }

        public HttpCompletionOption CompletionOption { get; init; }

        public HttpRequestMessage? Request { get; set; }

        public HttpResponseMessage? Response { get; set; }

        public ExceptionDispatchInfo? Exception { get; set; }

        public void Dispose()
        {
            Request?.Dispose();
            Response?.Dispose();
        }
    }
}