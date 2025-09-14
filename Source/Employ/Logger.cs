using System.IO;

namespace SR_DMG.Source.Employ
{
    public class Logger
    {
        public static readonly List<string> Logs = [];

        public static void Log(Exception Except)
        {
            Log(Except.Message + Simple.NewLine + Except.StackTrace);
        }

        public static void Log(Exception Except, string Message)
        {
            Log($"{Except.Message}({Message})");
        }

        public static void Log(string Message)
        {
            Logs.Add($"[{DateTime.Now.ToString(Simple.Lay_DateTime)}] {Message}");
        }

        public static void LogToFile()
        {
            File.WriteAllText(Program.GetPath(Simple.File_Log), string.Join(Simple.NewLine, Logs));
        }
    }
}