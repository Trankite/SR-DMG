using System.Net.Http;
using System.Runtime.ExceptionServices;

namespace StarRailDamage.Source.Web
{
    public sealed class HttpContext : IDisposable
    {
        public required HttpClient HttpClient { get; init; }

        public CancellationToken Canceller { get; init; }

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