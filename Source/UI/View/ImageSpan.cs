using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SR_DMG.Source.UI.View
{
    public class ImageSpan : Control
    {
        static ImageSpan()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageSpan), new FrameworkPropertyMetadata(typeof(ImageSpan)));
        }

        public ImageSource Icon
        {
            set { SetValue(IconProperty, value); }
            get { return (ImageSource)GetValue(IconProperty); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(ImageSpan));

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string)GetValue(TextProperty); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(ImageSpan));

        public List<ImageSource> Tags
        {
            set { SetValue(TagsProperty, value); }
            get { return (List<ImageSource>)GetValue(TagsProperty); }
        }

        public static readonly DependencyProperty TagsProperty = DependencyProperty.Register(nameof(Tags), typeof(List<ImageSource>), typeof(ImageSpan));
    }
}