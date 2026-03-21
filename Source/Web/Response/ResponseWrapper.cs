using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Response
{
    public class ResponseWrapper : IResponseMessage, IResponseValidator
    {
        [JsonPropertyName("retcode")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        public virtual bool IsSuccess() => Code == 0;

        public override string ToString()
        {
            return $"{Message} ({Code})";
        }
    }

    public class ResponseWrapper<TContent> : ResponseWrapper
    {
        [JsonPropertyName("data")]
        public TContent? Content { get; set; }
    }
}