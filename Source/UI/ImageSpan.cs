using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SR_DMG.Source.UI
{
    public class ImageSpan : Control
    {
        static ImageSpan()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageSpan), new FrameworkPropertyMetadata(typeof(ImageSpan)));
        }

        public ImageSource? Icon { set; get; }
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon",
            typeof(ImageSource), typeof(ImageSpan), new PropertyMetadata(null));

        public List<Image> Images { set; get; } = [];
        public static readonly DependencyProperty ImagesProperty = DependencyProperty.Register("Images",
            typeof(List<Image>), typeof(ImageSpan), new PropertyMetadata(new List<Image>()));

        public string Text { set; get; } = string.Empty;
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
            typeof(string), typeof(ImageSpan), new FrameworkPropertyMetadata(string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    }
}