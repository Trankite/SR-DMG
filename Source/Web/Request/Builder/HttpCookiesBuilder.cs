using System.Net;

namespace StarRailDamage.Source.Web.Request.Builder
{
    public class HttpCookiesBuilder
    {
        public CookieCollection Collection { get; } = [];

        public override string ToString()
        {
            return string.Join(';', Collection.Select(x => $"{x.Name}={x.Value}"));
        }
    }
}