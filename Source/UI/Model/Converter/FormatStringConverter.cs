using StarRailDamage.Source.Extension;
using System.Globalization;
using System.Windows.Data;

namespace StarRailDamage.Source.UI.Model.Converter
{
    public class FormatStringConverter : IValueConverter, IMultiValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return StringExtension.Format(parameter as string, value);
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return StringExtension.Format(parameter as string, values);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return [Binding.DoNothing];
        }
    }
}