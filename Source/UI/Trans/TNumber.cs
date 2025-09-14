using System.Globalization;
using System.Windows.Data;

namespace SR_DMG.Source.UI.Trans
{
    public class TNumber : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((float)value, int.Parse((string)parameter)).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (float.TryParse((string)value, out float result))
            {
                return Math.Round(result, int.Parse((string)parameter));
            }
            return Binding.DoNothing;
        }
    }
}