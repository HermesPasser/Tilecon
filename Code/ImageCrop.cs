using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace tilecon
{
    class ImageCrop
    {
        public static readonly int XPDimension = 32;

        public static Bitmap Crop(Bitmap src, int x, int y, int width, int height)
        {
            Rectangle rect = new Rectangle(x, y, width, height);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            using (Graphics gph = Graphics.FromImage(bmp))
                gph.DrawImage(src, new Rectangle(0, 0, bmp.Width, bmp.Height), rect, GraphicsUnit.Pixel);

            return bmp;
        }

        public static void SaveEachSubimage(Image img, string fileDir)
        {
            int index = fileDir.LastIndexOf(".");
            fileDir = fileDir.Substring(0, index) + "_";

            for (int i = 0, x = 0; x < img.Width; x += 32)
            {
                for (int y = 0; y < img.Width; y += 32)
                {
                    int ipp = i + 1;
                    string s = ipp > 99 ? s = ipp.ToString() : ipp > 9 ? s = ipp.ToString() : s = "0" + ipp;
                    Bitmap b = ImageCrop.Crop(img as Bitmap, x, y, ImageCrop.XPDimension, ImageCrop.XPDimension);
                    b.Save(fileDir + s + ".png");
                    i++;
                }
            }
        }

        public static Bitmap ConvertToMV(Image img)
        {
            int cropSize = ((img.Height / ImageCrop.XPDimension) + (img.Width / ImageCrop.XPDimension));
            Bitmap[] croppeds = new Bitmap[cropSize];

            for (int x = 0, i = 0; x < img.Width; x += 32)
            {
                for (int y = 0; y < img.Width; y += 32)
                {
                    croppeds[i] = ImageCrop.Crop(img as Bitmap, x, y, 35, 35);
                    i++;
                }
            }
            //Aqui dar um for para reaninhas as bitmaps no formato aceito no mv
            //for (int x = 0, i = 0; x < img.Width; x += 32)
            //    for (int y = 0; y < img.Width; y += 32)
                    

            //trocar pela a imagem convertida
            return img as Bitmap;
        }
    }
}
