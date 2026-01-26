using System.ComponentModel;
using System.Windows;

namespace StarRailDamage
{
    public class Program
    {
        public static bool IsDesignMode { get; }

        [STAThread]
        static void Main()
        {
            DebugTest();
            App app = new();
            app.InitializeComponent();
            app.Run();
        }

        private static async void DebugTest()
        {

        }

        static Program()
        {
            IsDesignMode = DesignerProperties.GetIsInDesignMode(new DependencyObject());
        }
    }
}