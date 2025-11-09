using StarRailDamage.Source.UI.Factory.PropertyBinding;
using StarRailDamage.Source.UI.Model.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Xaml.View
{
    public partial class LabelTextBox : UserControl
    {
        private static readonly PropertyBindingFactory<LabelTextBox> BindingFactory = new();

        public LabelTextBox()
        {
            InitializeComponent();
        }

        public LabelTextBoxModel Model
        {
            get => (LabelTextBoxModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        private static readonly DependencyProperty ModelProperty = BindingFactory.ModelBinding<LabelTextBoxModel>(nameof(Model));

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        private static readonly DependencyProperty IconProperty = BindingFactory.DependBinding(x => x.Model.Icon, x => x.Icon);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        private static readonly DependencyProperty TitleProperty = BindingFactory.DependBinding(x => x.Model.Title, x => x.Title);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static readonly DependencyProperty TextProperty = BindingFactory.DependBinding(x => x.Model.Text, x => x.Text);

        public string Unit
        {
            get => (string)GetValue(UnitProperty);
            set => SetValue(UnitProperty, value);
        }

        private static readonly DependencyProperty UnitProperty = BindingFactory.DependBinding(x => x.Model.Unit, x => x.Unit);

        public bool ReadOnly
        {
            get => (bool)GetValue(ReadOnlyProperty);
            set => SetValue(ReadOnlyProperty, value);
        }

        private static readonly DependencyProperty ReadOnlyProperty = BindingFactory.DependProperty<bool>(nameof(ReadOnly));

        public SolidColorBrush FocusBrush
        {
            get => (SolidColorBrush)GetValue(FocusBrushProperty);
            set => SetValue(FocusBrushProperty, value);
        }

        private static readonly DependencyProperty FocusBrushProperty = BindingFactory.DependProperty<SolidColorBrush>(nameof(FocusBrush));
    }
}