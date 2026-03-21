using StarRailDamage.Source.Service.Terminal;
using System.ComponentModel;
using System.Net.Http;
using System.Windows;

namespace StarRailDamage
{
    public class Program
    {
        public static bool IsDesignMode { get; }

        public static HttpClient HttpClient { get; }

        [STAThread]
        public static void Main(params string[] arguments)
        {
            DebugTest();
            TerminalHelper.Invoke(TerminalHelper.AllParse(arguments));
            if (!TerminalHelper.AllocMonitor().Result)
            {
                App.Main();
            }
            ApplicationDispose();
        }

        private static async void DebugTest()
        {

        }

        public static void ApplicationDispose()
        {
            HttpClient.Dispose();
        }

        static Program()
        {
            HttpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(15) };
            IsDesignMode = DesignerProperties.GetIsInDesignMode(new DependencyObject());
        }
    }
}