using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Xaml.View
{
    public partial class HeadSculpture : UserControl
    {
        public HeadSculpture()
        {
            InitializeComponent();
        }

        public ImageSource HeadImage
        {
            get => (ImageSource)GetValue(HeadImageProperty);
            set => SetValue(HeadImageProperty, value);
        }

        private static readonly DependencyProperty HeadImageProperty = DependencyProperty.Register(nameof(HeadImage), typeof(ImageSource), typeof(HeadSculpture));

        public ImageSource DefaultHeadImage
        {
            get => (ImageSource)GetValue(DefaultHeadImageProperty);
            set => SetValue(DefaultHeadImageProperty, value);
        }

        private static readonly DependencyProperty DefaultHeadImageProperty = DependencyProperty.Register(nameof(DefaultHeadImage), typeof(ImageSource), typeof(HeadSculpture));

        public ObservableCollection<ImageSource> MarkItems
        {
            get => (ObservableCollection<ImageSource>)GetValue(MarkItemsProperty);
            set => SetValue(MarkItemsProperty, value);
        }

        private static readonly DependencyProperty MarkItemsProperty = DependencyProperty.Register(nameof(MarkItems), typeof(ObservableCollection<ImageSource>), typeof(HeadSculpture));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(HeadSculpture));
    }
}