using SR_DMG.Source.Employ;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SR_DMG.Source.UI.Trans
{
    public class TSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<string> Targets)
            {
                return FindImage(Targets, parameter as BitmapImage);
            }
            else
            {
                return FindImage((string)value, parameter as BitmapImage);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static BitmapImage FindImage(string Target, BitmapImage? Default)
        {
            if (Simple.AppImages.TryGetValue(Target, out BitmapImage? BitImg)) return BitImg;
            if (Simple.RoleImages.TryGetValue(Target, out BitImg)) return BitImg;
            return Default ?? Simple.Not_Find_Image;
        }

        private static List<BitmapImage?> FindImage(List<string> Targets, BitmapImage? Default)
        {
            List<BitmapImage?> BitImgs = [];
            foreach (string Target in Targets)
            {
                BitImgs.Add(FindImage(Target, Default));
            }
            return BitImgs;
        }
    }
}