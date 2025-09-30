using System.IO;

namespace SR_DMG.Source.Employ
{
    public class Logger
    {
        public static readonly List<string> Logs = [];

        public static void Log(Exception Exception)
        {
            Log(Exception.Message + Simple.NewLine + Exception.StackTrace);
        }

        public static void Log(Exception Exception, string Message)
        {
            Log($"{Exception.Message}({Message})");
        }

        public static void Log(string Message)
        {
            Logs.Add($"[{DateTime.Now.ToString(Simple.Format_DateTime)}] {Message}");
        }

        public static void LogToFile()
        {
            File.WriteAllText(Program.GetPath(Simple.File_Log), string.Join(Simple.NewLine, Logs));
        }
    }
}