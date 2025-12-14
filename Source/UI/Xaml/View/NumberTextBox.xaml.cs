using StarRailDamage.Source.Core.Language;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.UI.Control;
using StarRailDamage.Source.UI.Factory.PropertyBinding;
using StarRailDamage.Source.UI.Model.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Xaml.View
{
    public partial class NumberTextBox : UserControl
    {
        private static readonly PropertyBindingFactory<NumberTextBox> BindingFactory = new();

        public static TextBinding TitleTextBinding { get; } = FixedText.NumberTextBoxTitle.Binding();

        private ScopedTextBox InnerTextBox { get => field.IsNotNull() ? field : field = (ScopedTextBox)Template.FindName("PART_TextBox", this); }

        public NumberTextBox()
        {
            InitializeComponent();
            this.SetFocusable();
            Unloaded += (sender, e) =>
            {
                BindingFactory.ClearModelBinding(Model);
            };
        }

        private void TextBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                Dropdown = !Dropdown.Configure(e.Handled = true);
            }
            else if (e.Key == Key.Enter)
            {
                Focus().Configure(Dropdown = false);
            }
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(EvaluateEvent, this).Configure(Dropdown = false));
        }

        private void PromptItemClick(object sender, RoutedEventArgs e)
        {
            ScopedButton ScopedButton = (ScopedButton)sender;
            int CaretIndex = InnerTextBox.CaretIndex;
            string Prompt = $"[{ScopedButton.Content}]";
            InnerTextBox.Text = InnerTextBox.Text.Insert(CaretIndex, Prompt);
            InnerTextBox.CaretIndex = CaretIndex + Prompt.Length;
            InnerTextBox.Focus();
            Dropdown = false;
        }

        public static readonly RoutedEvent EvaluateEvent = EventManager.RegisterRoutedEvent(nameof(EvaluateEvent), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NumberTextBox));

        public event RoutedEventHandler OnEvaluate
        {
            add => AddHandler(EvaluateEvent, value);
            remove => RemoveHandler(EvaluateEvent, value);
        }

        public NumberTextBoxModel Model
        {
            get => (NumberTextBoxModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        public static readonly DependencyProperty ModelProperty = BindingFactory.ModelBinding(x => x.Model);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty = BindingFactory.DependBinding(x => x.Model.Text, x => x.Text, PropertyBindingMode.TwoWay);

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty = BindingFactory.DependBinding(x => x.Model.Value, x => x.Value);

        public ObservableCollection<string> Items
        {
            get => (ObservableCollection<string>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public static readonly DependencyProperty ItemsProperty = BindingFactory.DependProperty(x => x.Items);

        public bool Dropdown
        {
            get => (bool)GetValue(DropdownProperty);
            set => SetValue(DropdownProperty, value);
        }

        public static readonly DependencyProperty DropdownProperty = BindingFactory.DependProperty(x => x.Dropdown);

        public SolidColorBrush FocusBrush
        {
            get => (SolidColorBrush)GetValue(FocusBrushProperty);
            set => SetValue(FocusBrushProperty, value);
        }

        public static readonly DependencyProperty FocusBrushProperty = BindingFactory.DependProperty(x => x.FocusBrush);
    }
}