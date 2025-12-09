using System.Net.Http;

namespace StarRailDamage.Source.Web.Request.Builder.Abstraction
{
    public interface IHttpRequestOptionsBuilder
    {
        HttpRequestOptions Options { get; }
    }
}