using System.Net.Http;

namespace StarRailDamage.Source.Web.Request.Builder.Abstraction
{
    public interface IHttpMethodBuilder
    {
        HttpMethod Method { get; set; }
    }
}