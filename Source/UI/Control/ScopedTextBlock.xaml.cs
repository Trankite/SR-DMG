using StarRailDamage.Source.Core.Setting;
using StarRailDamage.Source.Model.Text;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StarRailDamage.Source.UI.Control
{
    public partial class ScopedTextBlock : TextBlock
    {
        public ScopedTextBlock()
        {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
        }

        public TextBinding TextBinding
        {
            get => (TextBinding)GetValue(TextBindingProperty);
            set => SetValue(TextBindingProperty, value);
        }

        private static readonly DependencyProperty TextBindingProperty = DependencyProperty.Register(nameof(TextBinding), typeof(TextBinding), typeof(ScopedTextBlock), new PropertyMetadata(default, TextBindingChangedCallback));

        private static void TextBindingChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScopedTextBlock ScopedTextBlock)
            {
                ScopedTextBlock.Text = ScopedTextBlock.TextBinding.Text;
            }
        }

        public string TipText
        {
            get => (string)GetValue(TipTextProperty);
            set => SetValue(TipTextProperty, value);
        }

        private static readonly DependencyProperty TipTextProperty = DependencyProperty.Register(nameof(TipText), typeof(string), typeof(ScopedTextBlock), new PropertyMetadata(default, TipTextChangedCallBack));

        private static void TipTextChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScopedTextBlock ScopedTextBlock)
            {
                SetToolTip(ScopedTextBlock, e.NewValue as string);
            }
        }

        public bool TipOnlyTrim
        {
            get => (bool)GetValue(TipOnlyTrimProperty);
            set => SetValue(TipOnlyTrimProperty, value);
        }

        private static readonly DependencyProperty TipOnlyTrimProperty = DependencyProperty.Register(nameof(TipOnlyTrim), typeof(bool), typeof(ScopedTextBlock), new PropertyMetadata(false, TipOnlyTrimChangedCallBack));

        private static void TipOnlyTrimChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScopedTextBlock ScopedTextBlock)
            {
                ScopedTextBlock.EnabledToolTip();
            }
        }

        private static void SetToolTip(ScopedTextBlock scopedTextBlock, string? tipText)
        {
            if (string.IsNullOrEmpty(tipText))
            {
                scopedTextBlock.ToolTip = null;
            }
            else
            {
                if (scopedTextBlock.ToolTip is ScopedToolTip ScopedToolTip)
                {
                    ScopedToolTip.Text = tipText;
                }
                else
                {
                    scopedTextBlock.ToolTip = new ScopedToolTip(tipText);
                    scopedTextBlock.EnabledToolTip();
                }
            }
        }

        public Size GetTextSize()
        {
            Typeface Typeface = new(FontFamily, FontStyle, FontWeight, FontStretch);
            FormattedText FormattedText = new(Text, CultureInfo.CurrentCulture, FlowDirection, Typeface, FontSize, Foreground, AppSetting.PixelsPerDip);
            return new Size(FormattedText.Width, FormattedText.Height);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e) => EnabledToolTip();

        private void EnabledToolTip() => ToolTipService.SetIsEnabled(this, !TipOnlyTrim || IsTextTrimmed());

        public bool IsTextTrimmed() => TextTrimming == TextTrimming.CharacterEllipsis && GetTextSize().Width - 0.1 > ActualWidth;

        public override string ToString() => Text;
    }
}