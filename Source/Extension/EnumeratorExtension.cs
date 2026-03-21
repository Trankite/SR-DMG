using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Extension
{
    public static class EnumeratorExtension
    {
        [DebuggerStepThrough]
        public static bool TryGetNext<T>(this IEnumerator<T> enumerator, [NotNullWhen(true)] out T? value)
        {
            return enumerator.MoveNext() ? true.Configure(value = enumerator.Current) : false.Configure(value = default);
        }
    }
}