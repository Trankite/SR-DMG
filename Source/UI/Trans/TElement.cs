using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SR_DMG.Source.UI.Trans
{
    public class TElement : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Application.Current.Resources[$"{parameter}_{value}"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}