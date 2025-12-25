using System.ComponentModel;
using System.Windows;

namespace StarRailDamage
{
    public partial class App : Application
    {
        public static bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new());

        protected override void OnStartup(StartupEventArgs e)
        {
            DebugTest();
            base.OnStartup(e);
            if (e.Args.Length > 0)
            {

            }
        }

        private static async void DebugTest()
        {

        }
    }
}