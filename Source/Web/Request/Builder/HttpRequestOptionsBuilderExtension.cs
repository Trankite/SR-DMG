using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Web.Request.Builder.Abstraction;
using System.Net.Http;

namespace StarRailDamage.Source.Web.Request.Builder
{
    public static class HttpRequestOptionsBuilderExtension
    {
        public static TBuilder SetOptions<TBuilder, TValue>(this TBuilder builder, HttpRequestOptionsKey<TValue> key, TValue value) where TBuilder : IHttpRequestOptionsBuilder
        {
            return builder.Configure(builder => builder.Options.Set(key, value));
        }
    }
}