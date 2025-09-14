using SR_DMG.Source.Employ;
using SR_DMG.Source.UI.Event;
using System.Collections;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SR_DMG
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            Initialize();
            InitializeComponent();
        }

        public static IMainWindow Event { get; } = new();

        private static void Initialize()
        {
            foreach (DictionaryEntry item in Application.Current.Resources.MergedDictionaries[0])
            {
                Simple.AppImages[$"{Simple.App_Name}:{item.Key}"] = (BitmapImage)(item.Value ?? Simple.Not_Find_Image);
            }
        }
    }
}