using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Request.Builder.Abstraction;
using System.Diagnostics;

namespace StarRailDamage.Source.Web.Request.Builder
{
    public static class HttpRequestUriBuilderExtension
    {
        [DebuggerStepThrough]
        public static T SetRequestUri<T>(this T builder, string? requestUri, UriKind uriKind = UriKind.RelativeOrAbsolute) where T : IHttpRequestUriBuilder
        {
            return builder.SetRequestUri(string.IsNullOrEmpty(requestUri) ? null : new Uri(requestUri, uriKind));
        }

        [DebuggerStepThrough]
        public static T SetRequestUri<T>(this T builder, Uri? requestUri) where T : IHttpRequestUriBuilder
        {
            return builder.Configure(builder.RequestUri = requestUri);
        }
    }
}