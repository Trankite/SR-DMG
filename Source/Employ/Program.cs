using SR_DMG.Source.Example;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace SR_DMG.Source.Employ
{
    public partial class Program
    {
        [GeneratedRegex(@"-?\d+.?\d+")]
        private static partial Regex FloatRegex();
        public static float GetFloat(string? Str)
        {
            if (!float.TryParse(Str, out float Value))
            {
                Str = FloatRegex().Match(Str ?? string.Empty).Value;
                if (!float.TryParse(Str, out Value)) return 0;
            }
            return Value;
        }

        public static string GetPath(params string[] FileName)
        {
            return Path.Combine([Simple.App_Folder, .. FileName]);
        }

        public static ImageSource GetImage(string FileName)
        {
            return (ImageSource)Application.Current.Resources[FileName];
        }

        public static bool FileWrite(FileCollection.UrlInfo UrlInfo)
        {
            try
            {
                File.WriteAllText(UrlInfo.Path, UrlInfo.Url);
                return true;
            }
            catch (Exception Excep) { Logger.Log(Excep); }
            return false;
        }

        public static void SetField<T>(ref T Field, T Value, string PropertyName, Action<string> OnChanged)
        {
            if (EqualityComparer<T>.Default.Equals(Field, Value)) return;
            Field = Value; OnChanged?.Invoke(PropertyName);
        }

        public static T FormJson<T>(string Json) where T : new()
        {
            return JsonSerializer.Deserialize<T>(Json, Simple.JsonOptions) ?? new();
        }

        [GeneratedRegex(@"(?<=>|^)(.*?)(?=<|$)")]
        private static partial Regex HtmlRegex();
        public static string Get_Html_Text(string Html)
        {
            MatchCollection Matchs = HtmlRegex().Matches(Html);
            Html = string.Empty;
            foreach (Match M in Matchs)
            {
                Html += M.Groups[1].Value;
            }
            return Html;
        }
    }
}