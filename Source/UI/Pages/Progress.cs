using SR_DMG.Source.Employ;
using SR_DMG.Source.UI.Event;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.Pages
{
    public partial class Progress : Page
    {

        public static IProgress Event { get; } = new();

        public Progress()
        {
            InitializeComponent();
            DataContext = Simple.App_Progress;
        }
    }
}