using System.Collections.Generic;
using System.Drawing;

namespace tilecon
{
    /// <summary>Image manipulation routines.</summary>
    public class ImageEditor
    {
        /// <summary>Split and save each sprite.</summary>
        /// <param name="img">Image to be cropped in sprites.</param>
        /// <param name="sizeWidth">Sprite width.</param>
        /// <param name="sizeHeight">Sprite heigh.</param>
        /// <returns>Each sprite of the bitmap.</returns>
        public static Bitmap[] SplitImageInSprites(Image img, int sizeWidth, int sizeHeight)
        {
            Bitmap bmp = img as Bitmap;
            List<Bitmap> sprites = new List<Bitmap>();
            for (int i = 0, y = 0; y < img.Height; y += sizeHeight)
            {
                for (int x = 0; x < img.Width; x += sizeWidth, i++)
                {
                    sprites.Add(Crop(bmp, x, y, sizeWidth, sizeHeight));
                }
            }

            return sprites.ToArray();
        }

        /// <summary>Split and save each sprite</summary>
        /// <param name="img">Image to be cropped in sprites.</param>
        /// <param name="size">Sprite size.</param>
        /// <returns>Each sprite of the bitmap.</returns>
        public static Bitmap[] SplitImageInSprites(Image img, int size)
        {
            return SplitImageInSprites(img, size, size);
        }

        /// <summary>Set a speficy color as transparent.</summary>
        /// <param name="bmp">Origin bitmap.</param>
        /// <param name="color">Color to be replaced with transparent.</param>
        /// <returns>The bitmap with transparent pixels.</returns>
        public static Bitmap SetColorAsAlpha(Bitmap bmp, Color color)
        {
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                    if (bmp.GetPixel(x, y) == color)
                        bmp.SetPixel(x, y, Color.Empty);
            return bmp;
        }

        /// <summary>Checks if two bitmaps are equals pixel by pixel.</summary>
        /// <param name="bmp1">Firs bitmap.</param>
        /// <param name="bmp2">Second bitmap.</param>
        /// <returns>Return true if the bitmaps are equals and false if not.</returns>
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

        /// <summary>Crop the bitmap.</summary>
        /// <param name="src">Bitmap to be cropped.</param>
        /// <param name="x">Beginning of the crop in x.</param>
        /// <param name="y">Beginning of the crop in y.</param>
        /// <param name="width">Width of the crop.</param>
        /// <param name="height">Height of the crop.</param>
        /// <returns>Cropped bitmap.</returns>
        public static Bitmap Crop(Bitmap src, int x, int y, int width, int height)
        {
            return Crop(src, new Rectangle(x, y, width, height));
        }

        /// <summary>Crop the bitmap.</summary>
        /// <param name="src">Bitmap to be cropped.</param>
        /// <param name="rect">Rectangle of crop.</param>
        /// <returns>Cropped bitmap.</returns>
        public static Bitmap Crop(Bitmap src, Rectangle rect)
        {
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(src, new Rectangle(0, 0, bmp.Width, bmp.Height), rect, GraphicsUnit.Pixel);
            g.Dispose(); 
            return bmp;
        }

        /// <summary>Checks if all area of image is transparent.</summary>
        /// <param name="bmp">Image to be checked.</param>
        /// <returns>Return true if is transparent and false if not.</returns>
        public static bool IsAlphaImage(Bitmap bmp)
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

        /// <summary>Stretch the bitmap.</summary>
        /// <param name="bmp">Bitmap to be stretched.</param>
        /// <param name="newSize">New size of bitmap.</param>
        /// <returns>Bitmap stretched.</returns>
        public static Bitmap Stretch(Bitmap bmp, int newSize)
        {
            return Stretch(bmp, newSize, newSize);
        }

        /// <summary>Stretch the bitmap.</summary>
        /// <param name="bmp">Bitmap to be stretched.</param>
        /// <param name="newSizeX">New size in x of bitmap.</param>
        /// <param name="newSizeY">New size in y of bitmap.</param>
        /// <returns>Bitmap stretched.</returns>
        public static Bitmap Stretch(Bitmap bmp, int newSizeX, int newSizeY)
        {
            Bitmap result = new Bitmap(newSizeX, newSizeY);
            Graphics g = Graphics.FromImage(result);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(bmp, 0, 0, newSizeX + 1, newSizeY + 1);
            g.Dispose();
            return result;
        }

        /// <summary>Paste the bitmap into the other.</summary>
        /// <param name="origin">Bitmap where another one will be pasted.</param>
        /// <param name="bmpToBePasted">Bitmap to be pasted.</param>
        /// <param name="x">Beginning of the paste in x.</param>
        /// <param name="y">Beginning of the paste in y.</param>
        /// <param name="width">Width of the paste.</param>
        /// <param name="height">Height of the paste.</param>
        /// <returns>The changed bitmap.</returns>
        public static Bitmap Paste(Bitmap origin, Bitmap bmpToBePasted, int x, int y, int width, int height)
        {
            Rectangle rect = new Rectangle(0, 0, width, height);
            Graphics graphics = Graphics.FromImage(origin);
            graphics.DrawImage(bmpToBePasted, x, y, rect, GraphicsUnit.Pixel);
            graphics.Dispose();
            return origin;
        }

        /// <summary>Paste a bitmap in the other where the bitmap is transparent.</summary>
        /// <param name="origin">Bitmap where another one will be pasted.</param>
        /// <param name="bmpToBePasted">Bitmap to be pasted.</param>
        /// <returns>The changed bitmap.</returns>
        public static Bitmap PasteInAlpha(Bitmap origin, Bitmap bmpToBePasted)
        {
            for (int x = 0; x < origin.Width; x++)
            {
                for (int y = 0; y < origin.Height; y++)
                {
                    if (origin.GetPixel(x, y).A == 0 && x <= bmpToBePasted.Width && y <= bmpToBePasted.Height)
                        origin.SetPixel(x, y, bmpToBePasted.GetPixel(x, y));
                }
            }
            return origin;
        }
    }
}