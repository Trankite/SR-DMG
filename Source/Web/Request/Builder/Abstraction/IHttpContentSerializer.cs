using System.Net.Http;
using System.Text;

namespace StarRailDamage.Source.Web.Request.Builder.Abstraction
{
    public interface IHttpContentSerializer
    {
        HttpContent? Serialize<TContent>(TContent? content, Encoding? encoding = default);

        ValueTask<TResult?> DeserializeAsync<TResult>(HttpContent? httpContent, CancellationToken cancellationToken = default);
    }
}