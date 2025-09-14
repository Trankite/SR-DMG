using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.View
{
    public class HorizonItem : Control
    {
        static HorizonItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HorizonItem), new FrameworkPropertyMetadata(typeof(HorizonItem)));
        }

        public List<string> Tags
        {
            set { SetValue(TagsProperty, value); }
            get { return (List<string>)GetValue(TagsProperty); }
        }

        public static readonly DependencyProperty TagsProperty = DependencyProperty.Register(nameof(Tags), typeof(List<string>), typeof(HorizonItem));
    }
}