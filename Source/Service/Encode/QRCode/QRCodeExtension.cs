using System.Drawing;
using System.Drawing.Imaging;

namespace StarRailDamage.Source.Service.Encode.QRCode
{
    public static class QRCodeExtension
    {
        public static void Save(this QRCode qrcode, string file, int pixelSize = 5, int padding = 20)
        {
            qrcode.Save(file, Color.Black, Color.White, pixelSize, padding);
        }

        public static void Save(this QRCode qrcode, string filePath, Color black, Color white, int pixelSize = 5, int padding = 20)
        {
            int Size = padding * 2 + qrcode.Size * pixelSize;
            using Bitmap Bitmap = new(Size, Size, PixelFormat.Format24bppRgb);
            using Graphics Graphic = Graphics.FromImage(Bitmap);
            Graphic.Clear(white);
            Brush Brush = new SolidBrush(black);
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
            Bitmap.Save(filePath);
        }
    }
}