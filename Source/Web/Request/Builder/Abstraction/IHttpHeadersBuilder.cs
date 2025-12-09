using System.Net.Http.Headers;

namespace StarRailDamage.Source.Web.Request.Builder.Abstraction
{
    public interface IHttpHeadersBuilder<out T> where T : HttpHeaders
    {
        T Headers { get; }
    }
}