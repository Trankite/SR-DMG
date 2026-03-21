using StarRailDamage.Source.Extension;
using System.Drawing;
using System.Drawing.Imaging;

namespace StarRailDamage.Source.Service.Encode.QRCode
{
    public static class QRCodeExtension
    {
        public static void Save(this QRCode qrcode, string file, int pixelSize = 5, int padding = 20, ImageFormat? imageFormat = default)
        {
            qrcode.Save(file, Color.Black, Color.White, pixelSize, padding, imageFormat);
        }

        public static void Save(this QRCode qrcode, string filePath, Color black, Color white, int pixelSize = 5, int padding = 20, ImageFormat? imageFormat = default)
        {
            qrcode.GetBitmap(black, white, pixelSize, padding).SaveAndDisponse(filePath, imageFormat);
        }

        public static Bitmap GetBitmap(this QRCode qrcode, int pixelSize = 5, int padding = 20)
        {
            return qrcode.GetBitmap(Color.Black, Color.White, pixelSize, padding);
        }

        public static Bitmap GetBitmap(this QRCode qrcode, Color black, Color white, int pixelSize = 5, int padding = 20)
        {
            int Size = padding * 2 + qrcode.Size * pixelSize;
            Bitmap Bitmap = new(Size, Size, PixelFormat.Format24bppRgb);
            using Graphics Graphic = Graphics.FromImage(Bitmap);
            using Brush Brush = new SolidBrush(black);
            Graphic.Clear(white);
            for (int x = 0; x < qrcode.Size; x++)
            {
                for (int y = 0; y < qrcode.Size; y++)
                {
                    if (qrcode[x, y].HasBit)
                    {
                        Graphic.FillRectangle(Brush, new Rectangle(padding + x * pixelSize, padding + y * pixelSize, pixelSize, pixelSize));
                    }
                }
            }
            return Bitmap;
        }
    }
}