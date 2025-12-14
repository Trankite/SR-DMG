using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Service.IO.FiledStream.Abstraction;
using System.IO;
using System.Runtime.ExceptionServices;

namespace StarRailDamage.Source.Service.IO.FiledStream
{
    public class FiledWriter : IFiledStreamStatus, IDisposable
    {
        public readonly StreamWriter? Writer;

        public bool Success { get; }

        public ExceptionDispatchInfo? Exception { get; set; }

        public FiledWriter(string path)
        {
            Success = StreamExtension.TryGetStreamWriter(path, out Writer, this);
        }

        public FiledWriter(Stream stream)
        {
            Success = StreamExtension.TryGetStreamWriter(stream, out Writer, this);
        }

        public FiledWriter(StreamWriter writer)
        {
            Success = ObjectExtension.IsNotNull(Writer = writer);
        }

        public void Dispose()
        {
            Writer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}