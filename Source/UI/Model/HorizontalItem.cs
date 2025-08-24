using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.Model
{
    public class HorizontalItem : Control
    {
        static HorizontalItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HorizontalItem), new FrameworkPropertyMetadata(typeof(HorizontalItem)));
        }

        public List<string> Tags
        {
            set { SetValue(TagsProperty, value); }
            get { return (List<string>)GetValue(TagsProperty); }
        }
        public static readonly DependencyProperty TagsProperty =
            DependencyProperty.Register(nameof(Tags), typeof(List<string>), typeof(HorizontalItem),
                new PropertyMetadata(null));
    }
}