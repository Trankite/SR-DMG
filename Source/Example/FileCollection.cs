using SR_DMG.Source.Employ;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SR_DMG.Source.Example
{
    public class FileCollection
    {
        public List<FileInfo> Contents = [];

        public string Basicpath = string.Empty;

        private const int BufferSize = 1024 * 1024;

        private Dictionary<string, string> CacheDictionary = [];

        public class FileInfo
        {
            public string Url = string.Empty;

            public string File = string.Empty;

            public string Hash = string.Empty;
        }

        public bool IsHashValid(FileInfo FileInfo)
        {
            return FileInfo.Hash.Equals(HashFile(FileInfo), StringComparison.OrdinalIgnoreCase);
        }

        public string GetPath(FileInfo FileInfo)
        {
            return Program.GetPath(Basicpath, FileInfo.File);
        }

        public void Initialize(FileCollection? FileCollection = null)
        {
            Directory.CreateDirectory(Program.GetPath(Basicpath));
            foreach (FileInfo Item in Contents)
            {
                if (string.IsNullOrWhiteSpace(Item.Url)) continue;
                Item.File += Path.GetExtension(new Uri(Item.Url).AbsolutePath);
            }
            if (FileCollection == null) return;
            CacheDictionary = FileCollection.CacheDictionary;
        }

        public void Initialize(FileInfo FileInfo)
        {
            if (string.IsNullOrEmpty(FileInfo.Hash)) FileInfo.Hash = HashFile(FileInfo);
        }

        private string HashFile(FileInfo FileInfo)
        {
            string FilePath = GetPath(FileInfo);
            if (CacheDictionary.TryGetValue(FilePath, out string? Result)) return Result;
            try
            {
                using SHA256 Sha256 = SHA256.Create();
                using BufferedStream Stream = new(File.OpenRead(FilePath), BufferSize);
                byte[] HashBytes = Sha256.ComputeHash(Stream);
                string HashString = BitConverter.ToString(HashBytes).Replace("-", string.Empty);
                CacheDictionary[FilePath] = HashString;
                return HashString;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static readonly JsonSerializerOptions JsonOptions = Program.GetJsonOptions(new FileInfoConverter());

        public string ToJson()
        {
            return JsonSerializer.Serialize(this, GetType(), JsonOptions);
        }

        public class FileInfoConverter : JsonConverter<FileInfo>
        {
            public override FileInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                using JsonDocument Document = JsonDocument.ParseValue(ref reader);
                JsonElement RootElement = Document.RootElement;
                if (RootElement.TryGetProperty(nameof(Role.Name), out _))
                {
                    return JsonSerializer.Deserialize<Skill>(RootElement.GetRawText(), options);
                }
                else
                {
                    return Program.FormJson<FileInfo>(RootElement.GetRawText());
                }
            }

            public override void Write(Utf8JsonWriter writer, FileInfo value, JsonSerializerOptions options)
            {
                Type InputType = value.GetType();
                if (InputType == typeof(FileInfo))
                {
                    JsonSerializer.Serialize(writer, value, Simple.JsonOptions);
                }
                else
                {
                    JsonSerializer.Serialize(writer, value, InputType, options);
                }
            }
        }
    }
}