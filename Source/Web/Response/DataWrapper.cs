using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Response
{
    public class DataWrapper<T>
    {
        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }
}