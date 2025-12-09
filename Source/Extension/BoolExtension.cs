using System.Diagnostics;

namespace StarRailDamage.Source.Extension
{
    public static class BoolExtension
    {
        [DebuggerStepThrough]
        public static T Capture<T>(this bool _, T result) => result;
    }
}