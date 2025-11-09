using System.IO;

namespace StarRailDamage.Source.Service.IO.FileStream
{
    public class FileReader : IDisposable
    {
        protected readonly StreamReader? _Reader;

        public bool Success { get; }

        public FileReader(string filePath)
        {
            Success = FileStream.TryGetReaderStream(filePath, out _Reader);
        }

        public FileReader(Stream stream)
        {
            Success = FileStream.TryGetReaderStream(stream, out _Reader);
        }

        public FileReader(StreamReader reader)
        {
            Success = (_Reader = reader) != null;
        }

        public void Dispose()
        {
            _Reader?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}