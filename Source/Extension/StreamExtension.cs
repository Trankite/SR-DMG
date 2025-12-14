using StarRailDamage.Source.Core.Abstraction;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.ExceptionServices;

namespace StarRailDamage.Source.Extension
{
    public static class StreamExtension
    {
        [DebuggerStepThrough]
        public static bool TryGetStreamReader(this Stream stream, [NotNullWhen(true)] out StreamReader? streamReader, IExceptionCapture? exceptionCapture = null)
        {
            try
            {
                return true.Configure(streamReader = new StreamReader(stream));
            }
            catch (Exception Exception)
            {
                return false.Configure(exceptionCapture?.Exception = ExceptionDispatchInfo.Capture(Exception)).Configure(streamReader = null);
            }
        }

        [DebuggerStepThrough]
        public static bool TryGetStreamReader(string path, [NotNullWhen(true)] out StreamReader? streamReader, IExceptionCapture? exceptionCapture = null)
        {
            try
            {
                return true.Configure(streamReader = new StreamReader(path));
            }
            catch (Exception Exception)
            {
                return false.Configure(exceptionCapture?.Exception = ExceptionDispatchInfo.Capture(Exception)).Configure(streamReader = null);
            }
        }

        [DebuggerStepThrough]
        public static bool TryGetStreamWriter(Stream stream, [NotNullWhen(true)] out StreamWriter? streamWriter, IExceptionCapture? exceptionCapture = null)
        {
            try
            {
                return true.Configure(streamWriter = new StreamWriter(stream));
            }
            catch (Exception Exception)
            {
                return false.Configure(exceptionCapture?.Exception = ExceptionDispatchInfo.Capture(Exception)).Configure(streamWriter = null);
            }
        }

        [DebuggerStepThrough]
        public static bool TryGetStreamWriter(string path, [NotNullWhen(true)] out StreamWriter? streamWriter, IExceptionCapture? exceptionCapture = null)
        {
            try
            {
                return true.Configure(streamWriter = new StreamWriter(path));
            }
            catch (Exception Exception)
            {
                return false.Configure(exceptionCapture?.Exception = ExceptionDispatchInfo.Capture(Exception)).Configure(streamWriter = null);
            }
        }
    }
}