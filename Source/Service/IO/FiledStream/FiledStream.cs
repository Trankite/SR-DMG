using StarRailDamage.Source.Extension;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace StarRailDamage.Source.Service.IO.FiledStream
{
    public static class FiledStream
    {
        [DebuggerStepThrough]
        public static bool TryGetReaderStream(this Stream stream, [NotNullWhen(true)] out StreamReader? streamReader)
        {
            try
            {
                return true.Configure(streamReader = new StreamReader(stream));
            }
            catch
            {
                return false.Configure(streamReader = null);
            }
        }

        [DebuggerStepThrough]
        public static bool TryGetReaderStream(string filePath, [NotNullWhen(true)] out StreamReader? streamReader)
        {
            try
            {
                return true.Configure(streamReader = new StreamReader(filePath));
            }
            catch
            {
                return false.Configure(streamReader = null);
            }
        }

        [DebuggerStepThrough]
        public static bool TryGetWriterStream(this Stream stream, [NotNullWhen(true)] out StreamWriter? streamWriter)
        {
            try
            {
                return true.Configure(streamWriter = new StreamWriter(stream));
            }
            catch
            {
                return false.Configure(streamWriter = null);
            }
        }

        [DebuggerStepThrough]
        public static bool TryGetWriterStream(string filePath, [NotNullWhen(true)] out StreamWriter? streamWriter)
        {
            try
            {
                return true.Configure(streamWriter = new StreamWriter(filePath));
            }
            catch
            {
                return false.Configure(streamWriter = null);
            }
        }
    }
}