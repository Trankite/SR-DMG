using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SR_DMG.Source.UI.Model
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
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(ImageSpan),
                new PropertyMetadata(null));

        public List<ImageSource> Images
        {
            set { SetValue(ImagesProperty, value); }
            get { return (List<ImageSource>)GetValue(ImagesProperty); }
        }
        public static readonly DependencyProperty ImagesProperty =
            DependencyProperty.Register(nameof(Images), typeof(List<ImageSource>), typeof(ImageSpan),
                new PropertyMetadata(new List<ImageSource>()));

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string)GetValue(TextProperty); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(ImageSpan),
                new PropertyMetadata(string.Empty));
    }
}