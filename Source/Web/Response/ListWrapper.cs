using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace StarRailDamage.Source.Web.Response
{
    public class ListWrapper<T>
    {
        [JsonPropertyName("list")]
        public ImmutableArray<T> List { get; set; }
    }
}