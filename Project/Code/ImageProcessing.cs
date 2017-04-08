using System.Drawing;

namespace tilecon
{
    public class ImageProcessing
    {
        protected ImageProcessing() { }

        public static Bitmap ChangePixelsColor(Bitmap bmp, Color color)
        {
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                    if (bmp.GetPixel(x, y) == color)
                        bmp.SetPixel(x, y, Color.Empty);
            return bmp;
        }

        protected Bitmap Crop(Bitmap src, int x, int y, int width, int height)
        {
            Rectangle rect = new Rectangle(x, y, width, height);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            using (Graphics gph = Graphics.FromImage(bmp))
            {
                gph.DrawImage(src, new Rectangle(0, 0, bmp.Width, bmp.Height), rect, GraphicsUnit.Pixel);
                gph.Dispose();
            }
            return bmp;
        }

        protected virtual bool IsAllAlphaImage(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                    if (bmp.GetPixel(x, y).A == 0)
                        return true;
            return false;
        }

        protected Bitmap Stretch(Bitmap bmp, int newSize)
        {
            Bitmap result = new Bitmap(newSize, newSize);
            Graphics g = Graphics.FromImage(result);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(bmp, 0, 0, newSize + 1, newSize + 1);
            g.Dispose();
            return result;
        }

        protected Bitmap StretchByLoop(Bitmap bmp, int newSize)
        {
            Bitmap tempBmp = new Bitmap(newSize, newSize);
            bool b = false;

            for (int y = 0, i = 0; y < bmp.Height; y++)
            {
                i = 0;
                for (int x = 0; x < bmp.Width; x++, i++)
                {
                    if (i < tempBmp.Width)
                        tempBmp.SetPixel(i, y, bmp.GetPixel(x, y));
                    if (b && i + 1 < tempBmp.Width)
                    {
                        tempBmp.SetPixel(i + 1, y, bmp.GetPixel(x, y));
                        i++;
                    }
                    b = !b;
                }
            }

            bmp = tempBmp;
            tempBmp = new Bitmap(newSize, newSize);

            for (int x = 0, i = 0; x < bmp.Width; x++)
            {
                i = 0;
                for (int y = 0; y < bmp.Height; y++, i++)
                {
                    if (i < tempBmp.Height)
                        tempBmp.SetPixel(x, i, bmp.GetPixel(x, y));

                    if (b && i + 1 < tempBmp.Height)
                    {
                        tempBmp.SetPixel(x, i + 1, bmp.GetPixel(x, y));
                        i++;
                    }
                    b = !b;
                }
            }
            return tempBmp;
        }

        protected Bitmap Paste(Bitmap origin, Bitmap cut, int x, int y, int width, int height)
        {
            Rectangle rect = new Rectangle(0, 0, width, height);
            Graphics graphics = Graphics.FromImage(origin);
            graphics.DrawImage(cut, x, y, rect, GraphicsUnit.Pixel);
            graphics.Dispose();
            return origin;
        }
    }
}