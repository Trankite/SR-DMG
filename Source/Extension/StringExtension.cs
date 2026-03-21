using StarRailDamage.Source.Model.DataStruct;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace StarRailDamage.Source.Extension
{
    public static class StringExtension
    {
        [DebuggerStepThrough]
        public static FrozenSpan<char, char> FirstSplit(this ReadOnlySpan<char> value, char separator)
        {
            return value.FirstSplit(MemoryMarshal.CreateSpan(ref separator, 1));
        }

        [DebuggerStepThrough]
        public static FrozenSpan<char, char> FirstSplit(this ReadOnlySpan<char> value, ReadOnlySpan<char> separator)
        {
            return value.IndexOfTry(separator, out int index) ? value.SplitAtWithOutSelf(index, separator) : new FrozenSpan<char, char>(value, string.Empty);
        }

        [DebuggerStepThrough]
        public static FrozenSpan<char, char> LastSplit(this ReadOnlySpan<char> value, char separator)
        {
            return value.LastSplit(MemoryMarshal.CreateSpan(ref separator, 1));
        }

        [DebuggerStepThrough]
        public static FrozenSpan<char, char> LastSplit(this ReadOnlySpan<char> value, ReadOnlySpan<char> separator)
        {
            return value.LastIndexOfTry(separator, out int index) ? value.SplitAtWithOutSelf(index, separator) : new FrozenSpan<char, char>(value, string.Empty);
        }

        [DebuggerStepThrough]
        public static FrozenSpan<char, char> SplitAtWithOutSelf(this ReadOnlySpan<char> value, int index, ReadOnlySpan<char> separator)
        {
            return new FrozenSpan<char, char>(value[..index], value[(index + separator.Length)..]);
        }

        [DebuggerStepThrough]
        public static bool IndexOfTry(this ReadOnlySpan<char> value, ReadOnlySpan<char> separator, out int index)
        {
            return (index = value.IndexOf(separator)) != -1;
        }

        [DebuggerStepThrough]
        public static bool LastIndexOfTry(this ReadOnlySpan<char> value, ReadOnlySpan<char> separator, out int index)
        {
            return (index = value.LastIndexOf(separator)) != -1;
        }

        [DebuggerStepThrough]
        public static char? Index(this ReadOnlySpan<char> value, int index)
        {
            return index >= 0 && index < value.Length ? value[index] : null;
        }

        [DebuggerStepThrough]
        public static bool IndexTry(this ReadOnlySpan<char> value, int index, [NotNullWhen(true)] out char result)
        {
            return index >= 0 && index < value.Length ? true.Configure(result = value[index]) : false.Configure(result = default);
        }

        [DebuggerStepThrough]
        public static string Format(string value, params object[] args)
        {
            try
            {
                return string.Format(value, args);
            }
            catch
            {
                return value;
            }
        }
    }
}