using System.IO;

namespace StarRailDamage.Source.Service.IO.FileStream
{
    public class FileWriter : IDisposable
    {
        protected readonly StreamWriter? _Writer;

        public bool Success { get; }

        public FileWriter(string filePath)
        {
            Success = FileStream.TryGetWriterStream(filePath, out _Writer);
        }

        public FileWriter(Stream stream)
        {
            Success = FileStream.TryGetWriterStream(stream, out _Writer);
        }

        public FileWriter(StreamWriter writer)
        {
            Success = (_Writer = writer) != null;
        }

        public void Dispose()
        {
            _Writer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}