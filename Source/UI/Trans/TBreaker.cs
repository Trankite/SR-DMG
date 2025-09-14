using SR_DMG.Source.Employ;
using System.Globalization;
using System.Windows.Data;

namespace SR_DMG.Source.UI.Trans
{
    public class TBreaker : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Simple.GetElementBreak((int)(float)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}