using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StarRailDamage.Source.Extension
{
    public static class BitmapImageExtension
    {
        public static readonly BitmapImage DefaultImage;

        [DebuggerStepThrough]
        public static BitmapImage GetBitmapImage(this Stream stream)
        {
            stream.Position = 0;
            BitmapImage BitmapImage = new();
            BitmapImage.BeginInit();
            BitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            BitmapImage.StreamSource = stream;
            BitmapImage.EndInit();
            BitmapImage.Freeze();
            return BitmapImage;
        }

        static BitmapImageExtension()
        {
            PngBitmapEncoder Encoder = new();
            PixelFormat Format = PixelFormats.Pbgra32;
            int Width = 10, Height = 10, Dpi = 96, Pixel = (Format.BitsPerPixel + 7) / 8;
            Encoder.Frames.Add(BitmapFrame.Create(
                BitmapSource.Create(
                    Width, Height, Dpi, Dpi, Format, null,
                    new int[Pixel * Width * Height],
                    Width * Pixel)));
            using MemoryStream Stream = new();
            Encoder.Save(Stream);
            DefaultImage = GetBitmapImage(Stream);
        }
    }
}