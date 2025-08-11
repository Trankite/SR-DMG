using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SR_DMG.Source.UI
{
    public class LabelTextBox : Control
    {
        static LabelTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LabelTextBox), new FrameworkPropertyMetadata(typeof(LabelTextBox)));
        }

        public string Label { set; get; } = string.Empty;
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label",
            typeof(string), typeof(LabelTextBox), new PropertyMetadata(string.Empty));

        public ImageSource? Image { set; get; }
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image",
            typeof(ImageSource), typeof(LabelTextBox), new PropertyMetadata(null));

        public string Text { set; get; } = string.Empty;
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
            typeof(string), typeof(LabelTextBox), new FrameworkPropertyMetadata(string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Unit { set; get; } = string.Empty;
        public static readonly DependencyProperty UnitProperty = DependencyProperty.Register("Unit",
            typeof(string), typeof(LabelTextBox), new PropertyMetadata(string.Empty));
    }
}