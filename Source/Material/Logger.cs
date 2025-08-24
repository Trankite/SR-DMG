using System.IO;

namespace SR_DMG.Source.Material
{
    public class Logger
    {
        public static readonly List<string> Logs = [];

        public static void Log(string Msg)
        {
            Logs.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {Msg}");
        }

        public static void Log(Exception E, string? Msg = null)
        {
            Log((Msg ?? E.Message) + Simple.NewLine + E.StackTrace);
        }

        public static void LogToFile()
        {
            File.WriteAllText(Program.GetPath(Simple.Log), string.Join(Simple.NewLine, Logs));
        }

    }
}
