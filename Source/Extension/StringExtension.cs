using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace StarRailDamage.Source.Extension
{
    public static class StringExtension
    {
        public static string[] FirstSplit(this string value, char separator)
        {
            return value.FirstSplit(separator.ToString());
        }

        public static string[] FirstSplit(this string value, string separator)
        {
            return value.IndexOfTry(separator, out int index) ? value.SplitAtWithOutSelf(index) : [];
        }

        public static string[] LastSplit(this string value, char separator)
        {
            return value.LastSplit(separator.ToString());
        }

        public static string[] LastSplit(this string value, string separator)
        {
            return value.LastIndexOfTry(separator, out int index) ? value.SplitAtWithOutSelf(index, separator.Length) : [];
        }

        public static string[] SplitAt(this string value, int index)
        {
            return [value[..index], value[index..]];
        }

        public static string[] SplitAtWithOutSelf(this string value, int index, int length = 1)
        {
            return [value[..index], value[(index + length)..]];
        }

        public static bool IndexOfTry(this string value, string separator, out int index)
        {
            return (index = value.IndexOf(separator)) != -1;
        }

        public static bool LastIndexOfTry(this string value, string separator, out int index)
        {
            return (index = value.LastIndexOf(separator)) != -1;
        }

        public static char? Index(this string value, int index)
        {
            return index > 0 && index < value.Length ? value[index] : null;
        }

        public static bool IndexTry(this string value, int index, [NotNullWhen(true)] out char? result)
        {
            return index > 0 && index < value.Length ? true.With(result = value[index]) : false.With(result = null);
        }

        public static string Format(this string? value, params object[] args)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            try
            {
                return string.Format(value, args);
            }
            catch
            {
                return value;
            }
        }

        public static string BuildFolder(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Directory.CreateDirectory(value);
            }
            return value;
        }

        public static string BuildFile(this string value)
        {
            string? FolderPath = Path.GetDirectoryName(value);
            if (!string.IsNullOrEmpty(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            return value;
        }
    }
}