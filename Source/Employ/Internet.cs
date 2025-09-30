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
            if (Response == null) return null;
            return await Response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage?> Response()
        {
            try
            {
                HttpRequestMessage Request = new(Method, Url)
                {
                    Content = new StringContent(Body ?? string.Empty, Encoding.UTF8, "application/json")
                };
                foreach (KeyValuePair<string, string> Item in Headers)
                {
                    Request.Headers.Add(Item.Key, Item.Value);
                }
                return await Http.SendAsync(Request, Canceller);
            }
            catch (Exception Exception)
            {
                Logger.Log(Exception);
            }
            return null;
        }

        private static readonly HashSet<string> HostWhitelist = ["act-upload.mihoyo.com"];

        public async Task<bool> Download(string FilePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Url)) return true;
                if (!HostWhitelist.Contains(new Uri(Url).Host)) return true;
                using HttpResponseMessage? Response = await this.Response();
                if (Response == null) return false;
                using Stream Stm = await Response.Content.ReadAsStreamAsync(Canceller);
                using FileStream Fs = File.Create(FilePath);
                await Stm.CopyToAsync(Fs, Canceller);
                return true;
            }
            catch (Exception Exception)
            {
                Logger.Log(Exception);
            }
            return false;
        }
    }
}