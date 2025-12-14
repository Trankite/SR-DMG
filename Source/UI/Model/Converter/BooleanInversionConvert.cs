using System.Globalization;
using System.Windows.Data;

namespace StarRailDamage.Source.UI.Model.Converter
{
    public class BooleanInversionConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is false;
        }
    }
}