using StarRailDamage.Source.Extension;
using StarRailDamage.Source.UI.Factory.PropertyBinding;
using StarRailDamage.Source.UI.Model.Control;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Control
{
    public partial class ScopedSlider : Slider
    {
        private static readonly PropertyBindingFactory<ScopedSlider> BindingFactory = new();

        public ScopedSlider()
        {
            InitializeComponent();
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue.With(Model?.Value = newValue));
        }

        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum.With(Model?.Minimun = newMinimum));
        }

        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum.With(Model?.Maximum = newMaximum));
        }

        public ScopedSliderModel Model
        {
            get => (ScopedSliderModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        public static readonly DependencyProperty ModelProperty = BindingFactory.ModelBinding(x => x.Model).With(BindingFactory.AddBinding(x => x.Model.Minimun, x => x.Minimum)).With(BindingFactory.AddBinding(x => x.Model.Maximum, x => x.Maximum));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty = BindingFactory.DependBinding(x => x.Model.Title, x => x.Title);

        public SolidColorBrush FocusBrush
        {
            get => (SolidColorBrush)GetValue(FocusBrushProperty);
            set => SetValue(FocusBrushProperty, value);
        }

        public static readonly DependencyProperty FocusBrushProperty = BindingFactory.DependProperty(x => x.FocusBrush);
    }
}