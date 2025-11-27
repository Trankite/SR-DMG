using StarRailDamage.Source.Extension;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace StarRailDamage.Source.Service.IO.FileStream
{
    public static class FileStream
    {
        public static bool TryGetReaderStream(Stream stream, [NotNullWhen(true)] out StreamReader? streamReader)
        {
            try
            {
                return true.With(streamReader = new StreamReader(stream));
            }
            catch
            {
                return false.With(streamReader = null);
            }
        }

        public static bool TryGetReaderStream(string filePath, [NotNullWhen(true)] out StreamReader? streamReader)
        {
            try
            {
                return true.With(streamReader = new StreamReader(filePath));
            }
            catch
            {
                return false.With(streamReader = null);
            }
        }

        public static bool TryGetWriterStream(Stream stream, [NotNullWhen(true)] out StreamWriter? streamWriter)
        {
            try
            {
                return true.With(streamWriter = new StreamWriter(stream));
            }
            catch
            {
                return false.With(streamWriter = null);
            }
        }

        public static bool TryGetWriterStream(string filePath, [NotNullWhen(true)] out StreamWriter? streamWriter)
        {
            try
            {
                return true.With(streamWriter = new StreamWriter(filePath));
            }
            catch
            {
                return false.With(streamWriter = null);
            }
        }
    }
}