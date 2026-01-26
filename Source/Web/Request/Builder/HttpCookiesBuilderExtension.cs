using StarRailDamage.Source.Extension;
using System.Net;

namespace StarRailDamage.Source.Web.Request.Builder
{
    public static class HttpCookiesBuilderExtension
    {
        public static HttpCookiesBuilder AddCookie(this HttpCookiesBuilder builder, string name, string value)
        {
            return builder.AddCookie(new Cookie(name, value));
        }

        public static HttpCookiesBuilder AddCookie(this HttpCookiesBuilder builder, Cookie cookie)
        {
            return builder.Configure(builder => builder.Collection.Add(cookie));
        }

        public static HttpCookiesBuilder SetCookie(this HttpCookiesBuilder builder, string name, string value)
        {
            return builder.SetCookie(new Cookie(name, value));
        }

        public static HttpCookiesBuilder SetCookie(this HttpCookiesBuilder builder, Cookie cookie)
        {
            return builder.Configure(builder.RemoveCookie(cookie).AddCookie(cookie));
        }

        public static HttpCookiesBuilder RemoveCookie(this HttpCookiesBuilder builder, string name, string value)
        {
            return builder.RemoveCookie(new Cookie(name, value));
        }

        public static HttpCookiesBuilder RemoveCookie(this HttpCookiesBuilder builder, Cookie cookie)
        {
            return builder.Configure(builder.Collection.Remove(cookie));
        }
    }
}