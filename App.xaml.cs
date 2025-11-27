using StarRailDamage.Source.Service.Language;
using System.Windows;

namespace StarRailDamage
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LanguageReader.Load();
            if (e.Args.Length > 0)
            {

            }
        }
    }
}