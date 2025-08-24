using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SR_DMG.Source.UI.Model
{
    public class LabelTextBox : Control
    {
        static LabelTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LabelTextBox), new FrameworkPropertyMetadata(typeof(LabelTextBox)));
        }

        public string Title
        {
            set { SetValue(TitleProperty, value); }
            get { return (string)GetValue(TitleProperty); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(LabelTextBox),
                new PropertyMetadata(string.Empty));

        public ImageSource Image
        {
            set { SetValue(ImageProperty, value); }
            get { return (ImageSource)GetValue(ImageProperty); }
        }
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register(nameof(Image), typeof(ImageSource), typeof(LabelTextBox),
                new PropertyMetadata(null));

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string)GetValue(TextProperty); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(LabelTextBox),
                new PropertyMetadata(string.Empty));

        public string Unit
        {
            set { SetValue(UnitProperty, value); }
            get { return (string)GetValue(UnitProperty); }
        }
        public static readonly DependencyProperty UnitProperty =
            DependencyProperty.Register(nameof(Unit), typeof(string), typeof(LabelTextBox),
                new PropertyMetadata(string.Empty));
    }
}