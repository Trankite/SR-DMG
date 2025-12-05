using StarRailDamage.Source.Core.Setting;
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
            SizeChanged += (sender, e) =>
            {
                EnabledToolTip();
            };
        }

        public string TipText
        {
            get => (string)GetValue(TipTextProperty);
            set => SetValue(TipTextProperty, value);
        }

        private static readonly DependencyProperty TipTextProperty = DependencyProperty.Register(nameof(TipText), typeof(string), typeof(ScopedTextBlock), new PropertyMetadata(null, TipTextChangedCallBack));

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

        private void EnabledToolTip()
        {
            ToolTipService.SetIsEnabled(this, !TipOnlyTrim || IsTextTrimmed());
        }

        public bool IsTextTrimmed()
        {
            if (TextTrimming == TextTrimming.None) return false;
            Typeface Typeface = new(FontFamily, FontStyle, FontWeight, FontStretch);
            FormattedText FormattedText = new(Text, CultureInfo.CurrentCulture, FlowDirection, Typeface, FontSize, Foreground, AppSetting.PixelsPerDip);
            return FormattedText.Width - 0.1 > ActualWidth;
        }
    }
}