using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Request.Builder.Abstraction;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace StarRailDamage.Source.Web.Request.Builder
{
    public static class HttpHeadersBuilderExtension
    {
        [DebuggerStepThrough]
        public static T AddHeader<T>(this T builder, string name, string? value = null) where T : IHttpHeadersBuilder<HttpHeaders>
        {
            return builder.Configure(builder => builder.Headers.Add(name, value));
        }

        [DebuggerStepThrough]
        public static T SetHeader<T>(this T builder, string name, string? value = null) where T : IHttpHeadersBuilder<HttpHeaders>
        {
            return builder.RemoveHeader(name).AddHeader(name, value);
        }

        [DebuggerStepThrough]
        public static T RemoveHeader<T>(this T builder, params string?[] names) where T : IHttpHeadersBuilder<HttpHeaders>
        {
            return builder.Configure(builder => names.Foreach(name => builder.RemoveHeader(name)));
        }

        [DebuggerStepThrough]
        public static T ClearHeaders<T>(this T builder) where T : IHttpHeadersBuilder<HttpHeaders>
        {
            return builder.Configure(builder => builder.Headers.Clear());
        }
    }
}