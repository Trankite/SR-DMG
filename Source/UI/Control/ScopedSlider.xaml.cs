using StarRailDamage.Source.Extension;
using StarRailDamage.Source.UI.Factory.PropertyBinding;
using StarRailDamage.Source.UI.Model.Control;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace StarRailDamage.Source.UI.Control
{
    public partial class ScopedSlider : Slider
    {
        private static readonly Duration ThumbDuration = new(TimeSpan.FromMilliseconds(500));

        private static readonly PropertyBindingFactory<ScopedSlider> BindingFactory = new();

        public ScopedSlider()
        {
            InitializeComponent();
        }

        protected override void OnThumbDragCompleted(DragCompletedEventArgs e)
        {
            double Target = GetTickValue(Value);
            DoubleAnimation Animation = new(Target, ThumbDuration);
            Animation.Completed += (sender, e) =>
            {
                Value = Target;
                BeginAnimation(ValueProperty, null);
            };
            BeginAnimation(ValueProperty, Animation);
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            if (Model.IsNull()) return;
            if (Math.Abs(Model.Value - newValue) >= SmallChange)
            {
                Model.Value = GetTickValue(newValue);
            }
        }

        private double GetTickValue(double value)
        {
            return Math.Round(value / TickFrequency) * TickFrequency;
        }

        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            Model?.Minimun = newMinimum;
        }

        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            Model?.Maximum = newMaximum;
        }

        public ScopedSliderModel Model
        {
            get => (ScopedSliderModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        public static readonly DependencyProperty ModelProperty = BindingFactory.ModelBinding(x => x.Model).Configure(BindingFactory.AddBinding(x => x.Model.Minimun, x => x.Minimum)).Configure(BindingFactory.AddBinding(x => x.Model.Maximum, x => x.Maximum));

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