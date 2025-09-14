using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.View
{
    public class PlexSlider : Control
    {
        static PlexSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlexSlider), new FrameworkPropertyMetadata(typeof(PlexSlider)));
        }

        public string Title
        {
            set { SetValue(TitleProperty, value); }
            get { return (string)GetValue(TitleProperty); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(PlexSlider));

        public double Value
        {
            set { SetValue(ValueProperty, value); }
            get { return (double)GetValue(ValueProperty); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(double), typeof(PlexSlider));

        public double Maximum
        {
            set { SetValue(MaximumProperty, value); }
            get { return (double)GetValue(MaximumProperty); }
        }

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(PlexSlider));
    }
}