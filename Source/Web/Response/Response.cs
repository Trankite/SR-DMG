using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Response
{
    public class Response<TData> : DataWrapper<TData>, IResponseMessage, IResponseValidator
    {
        [JsonPropertyName("retcode")]
        public int ReturnCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        public bool TryValidate()
        {
            return ReturnCode == 0;
        }
    }
}