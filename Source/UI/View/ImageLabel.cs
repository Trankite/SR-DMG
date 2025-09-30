using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SR_DMG.Source.UI.View
{
    public class ImageLabel : Control
    {
        static ImageLabel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageLabel), new FrameworkPropertyMetadata(typeof(ImageLabel)));
        }

        public ImageSource Image
        {
            set { SetValue(ImageProperty, value); }
            get { return (ImageSource)GetValue(ImageProperty); }
        }

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(nameof(Image), typeof(ImageSource), typeof(ImageLabel));

        public string Title
        {
            set { SetValue(TitleProperty, value); }
            get { return (string)GetValue(TitleProperty); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(ImageLabel));
    }
}