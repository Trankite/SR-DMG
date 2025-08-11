using System.IO;
using System.Windows;

namespace SR_DMG.Source
{
    interface Program
    {
        public static void Debug()
        {
            //Mihomo.GetResources(5);
        }

        public static string GetPath(params string[] FileName)
        {
            return Path.Combine([Simple.RootFolder, .. FileName]);
        }

        public static bool Message(string Msg, string Tietle = Simple.App)
        {
            return MessageBox.Show(Msg, Tietle, MessageBoxButton.OKCancel) == MessageBoxResult.OK;
        }
    }
}
