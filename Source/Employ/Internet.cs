using System.IO;
using System.Net.Http;
using System.Text;

namespace SR_DMG.Source.Employ
{
    public class Internet
    {
        private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(10) };

        public HttpMethod Method = HttpMethod.Get;

        public CancellationToken Canceller = new();

        public Dictionary<string, string> Headers = [];

        public string Url = string.Empty;

        public string Body = string.Empty;

        public async Task<string?> Result()
        {
            using HttpResponseMessage? Response = await this.Response();
            return Response == null ? null : await Response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage?> Response()
        {
            try
            {
                HttpRequestMessage Request = new(Method, Url)
                {
                    Content = new StringContent(Body ?? string.Empty, Encoding.UTF8, "application/json")
                };
                foreach (KeyValuePair<string, string> K in Headers) Request.Headers.Add(K.Key, K.Value);
                return await Http.SendAsync(Request, Canceller);
            }
            catch (Exception Except) { Logger.Log(Except); }
            return null;
        }

        public async Task<bool> Download(string FilePath, bool WithOutExtension = true)
        {
            try
            {
                if (string.IsNullOrEmpty(Url)) return true;
                if (WithOutExtension)
                {
                    FilePath += Path.GetExtension(new Uri(Url).AbsolutePath);
                }
                using HttpResponseMessage? Response = await this.Response();
                if (Response == null) return false;
                using Stream Stm = await Response.Content.ReadAsStreamAsync(Canceller);
                using FileStream Fs = File.Create(FilePath);
                await Stm.CopyToAsync(Fs, Canceller);
                return true;
            }
            catch (Exception Except) { Logger.Log(Except); }
            return false;
        }
    }
}