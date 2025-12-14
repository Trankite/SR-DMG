using System.Diagnostics;

namespace StarRailDamage.Source.Extension
{
    public static class BoolExtension
    {
        [DebuggerStepThrough]
        public static TResult Captured<TResult, TNone>(this TNone _, TResult result) => result;
    }
}