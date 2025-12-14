using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Service.IO.FiledStream.Abstraction;
using System.IO;
using System.Runtime.ExceptionServices;

namespace StarRailDamage.Source.Service.IO.FiledStream
{
    public class FiledReader : IFiledStreamStatus, IDisposable
    {
        public readonly StreamReader? Reader;

        public bool Success { get; }

        public ExceptionDispatchInfo? Exception { get; set; }

        public FiledReader(string path)
        {
            Success = StreamExtension.TryGetStreamReader(path, out Reader, this);
        }

        public FiledReader(Stream stream)
        {
            Success = StreamExtension.TryGetStreamReader(stream, out Reader, this);
        }

        public FiledReader(StreamReader reader)
        {
            Success = ObjectExtension.IsNotNull(Reader = reader);
        }

        public void Dispose()
        {
            Reader?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}