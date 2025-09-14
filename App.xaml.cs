using System.Windows;

namespace SR_DMG
{
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += (s, e) => e.Handled = true;
        }
    }
}