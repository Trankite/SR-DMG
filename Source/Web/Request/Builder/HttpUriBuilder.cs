using StarRailDamage.Source.Extension;
using System.Collections.Specialized;
using System.Web;

namespace StarRailDamage.Source.Web.Request.Builder
{
    public class HttpUriBuilder
    {
        public UriBuilder UriBuilder { get; }

        public NameValueCollection Query { get; }

        public Uri Uri => UriBuilder.Configure(UriBuilder.Query = Query.ToString()).Uri;

        public HttpUriBuilder()
        {
            UriBuilder = new UriBuilder();
            Query = HttpUtility.ParseQueryString(UriBuilder.Query);
        }

        public HttpUriBuilder(string uri) : this(new Uri(uri)) { }

        public HttpUriBuilder(Uri uri)
        {
            UriBuilder = new UriBuilder(uri);
            Query = HttpUtility.ParseQueryString(UriBuilder.Query);
        }

        public override string ToString() => Uri.ToString();
    }
}