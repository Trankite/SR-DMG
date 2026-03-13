using System.Diagnostics;
using System.IO;

namespace StarRailDamage.Source.Service.IO
{
    public static class FileManage
    {
        [DebuggerStepThrough]
        public static string BuildPath(string? path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                Directory.CreateDirectory(path);
            }
            return path ?? string.Empty;
        }

        [DebuggerStepThrough]
        public static string BuildFilePath(string? path)
        {
            BuildPath(Path.GetDirectoryName(path));
            return path ?? string.Empty;
        }
    }
}