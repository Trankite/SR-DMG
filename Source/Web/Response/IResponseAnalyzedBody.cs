using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Web.Response
{
    public interface IResponseAnalyzedBody<TContent>
    {
        bool TryGetAnalyzedBody([NotNullWhen(true)] out TContent? analyedBody);
    }
}