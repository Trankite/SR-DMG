using System.IO;

namespace StarRailDamage.Source.Service.IO.FiledStream
{
    public class FiledReader : IDisposable
    {
        protected readonly StreamReader? _Reader;

        public bool Success { get; }

        public FiledReader(string filePath)
        {
            Success = FiledStream.TryGetReaderStream(filePath, out _Reader);
        }

        public FiledReader(Stream stream)
        {
            Success = FiledStream.TryGetReaderStream(stream, out _Reader);
        }

        public FiledReader(StreamReader reader)
        {
            Success = (_Reader = reader) is not null;
        }

        public void Dispose()
        {
            _Reader?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}