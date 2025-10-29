using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Control
{
    public partial class ScopedTabControl : TabControl
    {
        public ScopedTabControl()
        {
            InitializeComponent();
        }

        private void TabItemClose(object sender, RoutedEventArgs e)
        {

        }

        public SolidColorBrush FocusBrush
        {
            get => (SolidColorBrush)GetValue(FocusBrushProperty);
            set => SetValue(FocusBrushProperty, value);
        }

        private static readonly DependencyProperty FocusBrushProperty = DependencyProperty.Register(nameof(FocusBrush), typeof(SolidColorBrush), typeof(ScopedTabControl));
    }
}