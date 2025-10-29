using System.Net.Http;

namespace StarRailDamage.Source.Web
{
    public class HttpContent : IDisposable
    {
        public required HttpClient HttpClient { get; init; }

        public required CancellationToken Canceller { get; init; }

        public HttpCompletionOption CompletionOption { get; init; } = HttpCompletionOption.ResponseContentRead;

        public HttpRequestMessage? Request { get; set; }

        public HttpResponseMessage? Response { get; set; }

        public void Dispose()
        {
            Request?.Dispose();
            Response?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}