using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Request.Builder.Abstraction;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace StarRailDamage.Source.Web.Request
{
    public class JsonHttpContentSerializer : IHttpContentSerializer
    {
        public JsonSerializerOptions? JsonSerializerOptions { get; }

        public JsonHttpContentSerializer() { }

        public JsonHttpContentSerializer(JsonSerializerOptions? jsonSerializerOptions)
        {
            JsonSerializerOptions = jsonSerializerOptions;
        }

        public HttpContent? Serialize<TContent>(TContent? content, Encoding? encoding = null)
        {
            return new StringContent(JsonSerializer.Serialize(content, JsonSerializerOptions), encoding, MediaTypeNames.Application.Json);
        }

        public async ValueTask<TResult?> DeserializeAsync<TResult>(HttpContent? httpContent, CancellationToken cancellationToken = default)
        {
            string Json;
            if (httpContent.IsNull() || string.IsNullOrEmpty(Json = await httpContent.ReadAsStringAsync(cancellationToken).ConfigureAwait(false)))
            {
                return default;
            }
            return JsonSerializer.Deserialize<TResult>(Json, JsonSerializerOptions);
        }
    }
}