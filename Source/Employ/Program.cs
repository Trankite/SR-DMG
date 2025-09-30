using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            if (string.IsNullOrEmpty(Str)) return 0;
            if (float.TryParse(Str, out float Value) || float.TryParse(FloatRegex().Match(Str).Value, out Value))
            {
                return Str.Contains('%') ? Value /= 100 : Value;
            }
            return 0;
        }

        public static string GetPath(params string[] FileName)
        {
            return Path.Combine([Simple.App_Folder, .. FileName]);
        }

        public static ImageSource GetImage(string FileName)
        {
            return (ImageSource)Application.Current.Resources[FileName];
        }

        public static bool FileWrite(string FilePath, string Contents)
        {
            try
            {
                File.WriteAllText(FilePath, Contents);
                return true;
            }
            catch (Exception Exception)
            {
                Logger.Log(Exception);
            }
            return false;
        }

        public static void SetField<T>(ref T Field, T Value, string PropertyName, Action<string> OnChanged)
        {
            if (EqualityComparer<T>.Default.Equals(Field, Value)) return;
            Field = Value;
            OnChanged?.Invoke(PropertyName);
        }

        public static T FormJson<T>(string Json) where T : new()
        {
            return JsonSerializer.Deserialize<T>(Json, Simple.JsonOptions) ?? new();
        }

        public static JsonSerializerOptions GetJsonOptions(params JsonConverter[] Converters)
        {
            JsonSerializerOptions Options = new()
            {
                IncludeFields = true,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            foreach (JsonConverter Converter in Converters) Options.Converters.Add(Converter);
            return Options;
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
            return Html.Trim();
        }
    }
}