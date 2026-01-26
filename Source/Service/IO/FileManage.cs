using System.Diagnostics;
using System.IO;

namespace StarRailDamage.Source.Service.IO
{
    public static class FileManage
    {
        [DebuggerStepThrough]
        public static string BuildFolder(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Directory.CreateDirectory(value);
            }
            return value;
        }

        [DebuggerStepThrough]
        public static string BuildFile(string value)
        {
            string? FolderPath = Path.GetDirectoryName(value);
            if (!string.IsNullOrEmpty(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            return value;
        }
    }
}