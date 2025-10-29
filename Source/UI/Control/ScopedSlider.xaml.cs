using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Control
{
    public partial class ScopedSlider : Slider
    {
        public ScopedSlider()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(ScopedSlider));

        public SolidColorBrush FocusBrush
        {
            get => (SolidColorBrush)GetValue(FocusBrushProperty);
            set => SetValue(FocusBrushProperty, value);
        }

        private static readonly DependencyProperty FocusBrushProperty = DependencyProperty.Register(nameof(FocusBrush), typeof(SolidColorBrush), typeof(ScopedSlider));
    }
}