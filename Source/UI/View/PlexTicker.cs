using SR_DMG.Source.UI.Event;
using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.View
{
    public class PlexTicker : Control
    {
        static PlexTicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlexTicker), new FrameworkPropertyMetadata(typeof(PlexTicker)));
        }

        public static IPlexTicker Event { get; } = new();

        public bool Flag
        {
            set { SetValue(FlagProperty, value); }
            get { return (bool)GetValue(FlagProperty); }
        }

        public static readonly DependencyProperty FlagProperty = DependencyProperty.Register(nameof(Flag), typeof(bool), typeof(PlexTicker));

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string)GetValue(TextProperty); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(PlexTicker));
    }
}