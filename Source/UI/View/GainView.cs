using SR_DMG.Source.UI.Event;
using System.Windows;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.View
{
    public class GainView : Control
    {
        static GainView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GainView), new FrameworkPropertyMetadata(typeof(GainView)));
        }

        public static IGainView Event { get; } = new();

        public Model.GainView Model
        {
            set { SetValue(ModelProperty, value); }
            get { return (Model.GainView)GetValue(ModelProperty); }
        }

        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(nameof(Model), typeof(Model.GainView), typeof(GainView));
    }
}