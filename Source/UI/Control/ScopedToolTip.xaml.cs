using System.Windows;
using System.Windows.Controls;

namespace StarRailDamage.Source.UI.Control
{
    public partial class ScopedToolTip : ToolTip
    {
        public ScopedToolTip()
        {
            InitializeComponent();
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(ScopedToolTip));

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(double), typeof(ScopedToolTip));
    }
}