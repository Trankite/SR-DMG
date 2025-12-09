using System.IO;

namespace StarRailDamage.Source.Service.IO.FiledStream
{
    public class FiledWriter : IDisposable
    {
        protected readonly StreamWriter? _Writer;

        public bool Success { get; }

        public FiledWriter(string filePath)
        {
            Success = FiledStream.TryGetWriterStream(filePath, out _Writer);
        }

        public FiledWriter(Stream stream)
        {
            Success = FiledStream.TryGetWriterStream(stream, out _Writer);
        }

        public FiledWriter(StreamWriter writer)
        {
            Success = (_Writer = writer) is not null;
        }

        public void Dispose()
        {
            _Writer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}