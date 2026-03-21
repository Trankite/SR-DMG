using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace StarRailDamage.Source.Extension
{
    public static class BitmapExtension
    {
        [DebuggerStepThrough]
        public static void SaveAndDisponse(this Bitmap value, string filePath, ImageFormat? imageFormat = default)
        {
            using Bitmap Bitmap = value;
            Bitmap.Save(filePath, imageFormat ?? ImageFormat.Jpeg);
        }
    }
}