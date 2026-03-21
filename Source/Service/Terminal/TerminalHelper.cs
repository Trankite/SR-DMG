using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Service.Terminal.Abstraction;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace StarRailDamage.Source.Service.Terminal
{
    public static partial class TerminalHelper
    {
        [LibraryImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool AllocConsole();

        [LibraryImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool FreeConsole();

        private static bool _MonitorMode;

        public static bool MonitorMode => _MonitorMode;

        public static bool ConsoleMode { get; private set; }

        public static bool Alloc() => ConsoleMode = ConsoleMode || AllocConsole();

        public static bool Close() => ConsoleMode = ConsoleMode && !FreeConsole();

        public static IEnumerable<CommandLine> AllParse(string line)
        {
            return AllParse([.. TerminalCommandLineRegex().Matches(line).Select(Matche => Matche.Value)]);
        }

        public static IEnumerable<CommandLine> AllParse(string[] arguments)
        {
            for (int i = 0; i < arguments.Length; i++)
            {
                yield return NextCommandLine(arguments, ref i, i != 0);
            }
        }

        private static CommandLine NextCommandLine(string[] arguments, ref int index, bool trim)
        {
            string CommandName = arguments[index];
            CommandLine CommandLine = new(trim && CommandName.StartsWith('-') ? CommandName[1..] : CommandName);
            while (++index < arguments.Length && !arguments[index].StartsWith('-'))
            {
                CommandLine.Expand.Add(arguments[index]);
            }
            return CommandLine.Configure(index--);
        }

        public static async Task<bool> AllocMonitor()
        {
            if (ConsoleMode && !Interlocked.Exchange(ref _MonitorMode, true))
            {
                while (ConsoleMode && MonitorMode)
                {
                    Invoke(AllParse(Console.ReadLine() ?? string.Empty));
                }
                return Interlocked.Exchange(ref _MonitorMode, false);
            }
            return false;
        }

        public static bool CloseMonitor()
        {
            return Interlocked.Exchange(ref _MonitorMode, false);
        }

        public static void Invoke(params IEnumerable<ITerminalCommandLine> commandLines)
        {
            foreach (ITerminalCommandLine CommandLine in commandLines)
            {
                ITerminalResponse Response = CommandLine.Invoke();
                if (ConsoleMode)
                {
                    Console.WriteLine(Response.Message);
                }
            }
        }

        [GeneratedRegex(@"(?<!\\)""(?:(\\""|[^""])*)""|[^ ]+")]
        private static partial Regex TerminalCommandLineRegex();
    }
}