using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Response
{
    public class ResponseWrapper : IResponseMessage, IResponseValidator
    {
        [JsonPropertyName("retcode")]
        public int ReturnCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        public virtual bool IsSuccess() => ReturnCode == 0;
    }

    public class ResponseWrapper<TData> : ResponseWrapper where TData : new()
    {
        [JsonPropertyName("data")]
        public TData Data { get; set; } = new();
    }
}