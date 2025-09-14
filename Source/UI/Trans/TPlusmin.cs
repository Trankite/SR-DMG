using System.Globalization;
using System.Windows.Data;

namespace SR_DMG.Source.UI.Trans
{
    public class TPlusmin : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).StartsWith('-');
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}