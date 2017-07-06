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

        public static bool IsEqual(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1.Size != bmp2.Size)
                return false;

            for (int y = 0; y < bmp1.Height; y++)
            {
                for (int x = 0; x < bmp1.Width; x++)
                {
                    if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                        return false;
                }
            }

            return true;
        }

        protected Bitmap Crop(Bitmap src, int x, int y, int width, int height)
        {
            return Crop(src, new Rectangle(x, y, width, height));
        }

        protected Bitmap Crop(Bitmap src, Rectangle rect)
        {
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(src, new Rectangle(0, 0, bmp.Width, bmp.Height), rect, GraphicsUnit.Pixel);
            g.Dispose(); 
            return bmp;
        }

        protected virtual bool IsAllAlphaImage(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (bmp.GetPixel(x, y).A == 0)
                        return true;
                }
            }
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

        protected Bitmap Paste(Bitmap origin, Bitmap cut, int x, int y, int width, int height)
        {
            Rectangle rect = new Rectangle(0, 0, width, height);
            Graphics graphics = Graphics.FromImage(origin);
            graphics.DrawImage(cut, x, y, rect, GraphicsUnit.Pixel);
            graphics.Dispose();
            return origin;
        }
        
        protected Bitmap PasteInAlpha(Bitmap origin, Bitmap cut)
        {
            for (int x = 0; x < origin.Width; x++)
            {
                for (int y = 0; y < origin.Height; y++)
                {
                    if (origin.GetPixel(x, y).A == 0 && x <= cut.Width && y <= cut.Height)
                        origin.SetPixel(x, y, cut.GetPixel(x, y));
                }
            }
            return origin;
        }
    }
}