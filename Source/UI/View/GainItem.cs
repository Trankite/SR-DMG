using SR_DMG.Source.UI.Event;
using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.View
{
    public class GainItem : Control
    {
        static GainItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GainItem), new FrameworkPropertyMetadata(typeof(GainItem)));
        }

        public static IGainItem Event { get; } = new();

        public Model.GainItem Model
        {
            set { SetValue(GainProperty, value); }
            get { return (Model.GainItem)GetValue(GainProperty); }
        }

        public static readonly DependencyProperty GainProperty = DependencyProperty.Register(nameof(Model), typeof(Model.GainItem), typeof(GainItem));
    }
}