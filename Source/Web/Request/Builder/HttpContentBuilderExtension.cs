using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Request.Builder.Abstraction;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace StarRailDamage.Source.Web.Request.Builder
{
    public static class HttpContentBuilderExtension
    {
        [DebuggerStepThrough]
        public static T SetStringContent<T>(this T builder, string content, Encoding? encoding = null, string? mediaType = null) where T : IHttpContentBuilder
        {
            return builder.Configure(builder.Content = new StringContent(content, encoding, mediaType));
        }

        [DebuggerStepThrough]
        public static T SetFormUrlEncodedContent<T>(this T builder, params KeyValuePair<string, string>[] content) where T : IHttpContentBuilder
        {
            return builder.Configure(builder.Content = new FormUrlEncodedContent(content));
        }
    }
}