using System.Diagnostics;

namespace StarRailDamage.Source.Extension
{
    public static class StackExtension
    {
        [DebuggerStepThrough]
        public static T? PopOrDefault<T>(this Stack<T> value)
        {
            return value.TryPop(out T? result) ? result : default;
        }
    }
}