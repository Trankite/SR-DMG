using SR_DMG.Source.UI.Event;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SR_DMG.Source.UI.View
{
    public class LabelTextBox : Control
    {
        static LabelTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LabelTextBox), new FrameworkPropertyMetadata(typeof(LabelTextBox)));
        }

        public static ILabelTextBox Event { get; } = new();

        public string Title
        {
            set { SetValue(TitleProperty, value); }
            get { return (string)GetValue(TitleProperty); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(LabelTextBox));

        public BitmapImage Image
        {
            set { SetValue(ImageProperty, value); }
            get { return (BitmapImage)GetValue(ImageProperty); }
        }

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(nameof(Image), typeof(BitmapImage), typeof(LabelTextBox));

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string)GetValue(TextProperty); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(LabelTextBox));

        public string Unit
        {
            set { SetValue(UnitProperty, value); }
            get { return (string)GetValue(UnitProperty); }
        }

        public static readonly DependencyProperty UnitProperty = DependencyProperty.Register(nameof(Unit), typeof(string), typeof(LabelTextBox));
    }
}