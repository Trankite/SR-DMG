using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace StarRailDamage.Source.UI.Xaml.View
{
    public partial class HorizontalMarker : UserControl
    {
        public HorizontalMarker()
        {
            InitializeComponent();
        }

        public ObservableCollection<string> Items
        {
            get { return (ObservableCollection<string>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(nameof(Items), typeof(ObservableCollection<string>), typeof(HorizontalMarker));

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(HorizontalMarker));
    }
}