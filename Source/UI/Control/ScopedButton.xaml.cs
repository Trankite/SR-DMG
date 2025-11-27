using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Control
{
    public partial class ScopedButton : Button
    {
        public ScopedButton()
        {
            InitializeComponent();
        }

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(double), typeof(ScopedButton));

        public SolidColorBrush FocusBrush
        {
            get => (SolidColorBrush)GetValue(FocusBrushProperty);
            set => SetValue(FocusBrushProperty, value);
        }

        public static readonly DependencyProperty FocusBrushProperty = DependencyProperty.Register(nameof(FocusBrush), typeof(SolidColorBrush), typeof(ScopedButton));
    }
}