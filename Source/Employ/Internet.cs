using SR_DMG.Source.Material;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SR_DMG.Source.Employ
{
    public class Internet
    {
        private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(10) };

        public static async Task<string?> GetResult(HttpMethod Method, string Url, string? Body = null, Dictionary<string, string>? Headers = null)
        {
            using HttpResponseMessage? Response = await GetResponse(Method, Url, Body, Headers);
            return Response == null ? null : await Response.Content.ReadAsStringAsync();
        }
        public static async Task<HttpResponseMessage?> GetResponse(HttpMethod Method, string Url, string? Body = null, Dictionary<string, string>? Headers = null)
        {
            try
            {
                HttpRequestMessage Request = new(Method, Url)
                {
                    Content = new StringContent(Body ?? string.Empty, Encoding.UTF8, "application/json")
                };
                foreach (KeyValuePair<string, string> K in Headers ?? []) Request.Headers.Add(K.Key, K.Value);
                return await Http.SendAsync(Request);
            }
            catch (Exception Exp)
            {
                Logger.Log(Exp); return null;
            }
        }

        public static async Task<bool> Download(HttpMethod Method, string Url, string FilePath, string? Body = null, Dictionary<string, string>? Headers = null)
        {
            try
            {
                FilePath += Path.GetExtension(new Uri(Url).AbsolutePath);
                using HttpResponseMessage? Response = await GetResponse(Method, Url, Body, Headers);
                if (Response == null) return false;
                using Stream Stm = await Response.Content.ReadAsStreamAsync();
                using FileStream Fs = File.Create(FilePath);
                await Stm.CopyToAsync(Fs); return true;
            }
            catch (Exception Exp)
            {
                Logger.Log(Exp); return false;
            }
        }

        public static async Task<Color?> GetColor(HttpMethod Method, string Url, string? Body = null, Dictionary<string, string>? Headers = null)
        {
            using HttpResponseMessage? Response = await GetResponse(Method, Url, Body, Headers);
            if (Response == null) return null;
            try
            {
                BitmapImage Bmp = new();
                Bmp.BeginInit();
                Bmp.CacheOption = BitmapCacheOption.OnLoad;
                Bmp.StreamSource = await Response.Content.ReadAsStreamAsync();
                Bmp.EndInit();
                CroppedBitmap Cmp = new(Bmp, new System.Windows.Int32Rect(
                    Bmp.PixelWidth / 2, Bmp.PixelHeight / 2, 1, 1));
                byte[] Pixels = new byte[4];
                Cmp.CopyPixels(Pixels, 4, 0);
                return Color.FromRgb(Pixels[2], Pixels[1], Pixels[0]);
            }
            catch (Exception Exp)
            {
                Logger.Log(Exp); return null;
            }
        }
    }
}
