using System.Net.Http;

namespace StarRailDamage.Source.Web.Request.Builder.Abstraction
{
    public interface IHttpContentBuilder
    {
        HttpContent? Content { get; set; }

        IHttpContentSerializer HttpContentSerializer { get; }
    }
}