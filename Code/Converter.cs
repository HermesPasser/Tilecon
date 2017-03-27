using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace tilecon.Conversor
{
    class Converter
    {
        public enum spriteMode
        {
            NONE, RESIZE, CENTRALIZE
        }

        private static Bitmap Crop(Bitmap src, int x, int y, int width, int height)
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

        public static void SaveEachSubimage(Image img, string fileDir, int spriteSize)
        {
            int index = fileDir.LastIndexOf(".");
            fileDir = fileDir.Substring(0, index) + "_";

            for (int i = 0, y = 0; y < img.Height; y += spriteSize)
            {
                for (int x = 0; x < img.Width; x += spriteSize)
                {
                    int ipp = i + 1;
                    string s = ipp > 99 ? s = ipp.ToString() : ipp > 9 ? s = ipp.ToString() : s = "0" + ipp;
                    Bitmap b = Converter.Crop(img as Bitmap, x, y, spriteSize, spriteSize);
                    b.Save(fileDir + s + ".png");
                    i++;
                }
            }
        }

        private static Bitmap[] GetSprites(Image img, int spriteSize)
        {
            int cropSize = ((img.Height / spriteSize) * (img.Width / spriteSize));
            Bitmap[] sprites = new Bitmap[cropSize];

            for (int y = 0, i = 0; y < img.Height; y += spriteSize)
            {
                for (int x = 0; x < img.Width; x += spriteSize)
                {
                    sprites[i] = Converter.Crop(img as Bitmap, x, y, spriteSize, spriteSize);
                    i++;
                }
            }
            return sprites;
        }

        private static bool IsAlpha(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                    if (bmp.GetPixel(x, y).A == 0)
                        return true;
            return false;
        }

        private static Bitmap ResizeMV(Bitmap bmp)
        {
            // for encadeado, a cada img par copiar o pixel duas vezes
            return bmp;
        }

        //Criar meu proprio metodo de resize?
        private static int PasteMV(Bitmap bmp, List<Bitmap> sprites, int initY, int initX, int height, int width, int spriteIterator, bool ignoreAlpha, spriteMode mode)
        {
            Rectangle rect = new Rectangle(0, 0, sprites[0].Width, sprites[0].Height);
            Graphics graphics = Graphics.FromImage(bmp);

            //To resize
             if (mode == spriteMode.RESIZE)
                rect = new Rectangle(0, 0, Maker.MV.SPRITE_SIZE, Maker.MV.SPRITE_SIZE);

            for (int y = initY; y < height; y += Maker.MV.SPRITE_SIZE)
            {
                for (int x = initX; x < width; x += Maker.MV.SPRITE_SIZE)
                {
                    if (spriteIterator >= sprites.Count || sprites[spriteIterator] == null)
                        break;

                    //To centralize
                    int xx = x, yy = y;
                    if (mode == spriteMode.CENTRALIZE)
                    {
                        xx += 8;
                        yy += 8;
                    } //To resize
                    else if (mode == spriteMode.RESIZE)
                         sprites[spriteIterator] = new Bitmap(sprites[spriteIterator], new Size(Maker.MV.SPRITE_SIZE, Maker.MV.SPRITE_SIZE));

                    //To ignore alpha
                    if (IsAlpha(sprites[spriteIterator]) && ignoreAlpha)
                    {
                        if (spriteIterator + 1 < sprites.Count)
                        {
                            //To resize
                            if (mode == spriteMode.RESIZE)
                                sprites[spriteIterator + 1] = new Bitmap(sprites[spriteIterator + 1], new Size(Maker.MV.SPRITE_SIZE, Maker.MV.SPRITE_SIZE));

                            graphics.DrawImage(sprites[spriteIterator + 1], xx, yy, rect, GraphicsUnit.Pixel);
                        }
                        spriteIterator++;
                    }
                    else
                        graphics.DrawImage(sprites[spriteIterator], xx, yy, rect, GraphicsUnit.Pixel);
                    spriteIterator++;
                }
            }
            graphics.Dispose();
            return spriteIterator;
        }

        //Colocar opção para redimencionar a imagem

        public static Bitmap[] ConvertToMV(Image img, bool ignoreAlpha, spriteMode mode)
        {
            List<Bitmap> images = new List<Bitmap>();
            List<Bitmap> sprites = GetSprites(img, Maker.XP.SPRITE_SIZE).ToList<Bitmap>();
            int i = 0;

            //For each image
            while (i < sprites.Count)
            {
                //Draw image
                Bitmap tempBmp = new Bitmap(Maker.MV.BE.SIZE, Maker.MV.BE.SIZE);
                tempBmp.SetPixel(0, 0, Color.White);

                //Draw in first part of bitmap
                i = PasteMV(tempBmp, sprites, 0, 0, tempBmp.Height, tempBmp.Width / 2, i, ignoreAlpha, mode);

                //Draw in second part of bitmap
                i = PasteMV(tempBmp, sprites, 0, tempBmp.Width / 2, tempBmp.Height, tempBmp.Width, i, ignoreAlpha, mode);

                //Add image to the list
                images.Add(tempBmp);
            }

            return images.ToArray();
        }
    }
}

// criar quadriculado
//bool b = false;

//for (int y = 0; y<Maker.MV.SPRITE_SIZE; y++)
//    for (int x = 0; x<Maker.MV.SPRITE_SIZE; x++)
//    {
//        if (b)//&& xx < bmp.Width)
//        {
//            tempBmp.SetPixel(x, y, bmp.GetPixel(1, 1));
//            tempBmp.SetPixel(x + 1, y, bmp.GetPixel(1, 1));

//            x++;

//        }
//        else
//        {
//            // if (xx < bmp.Width)
//                tempBmp.SetPixel(x, y, bmp.GetPixel(1, 1));
//        }
//        b = !b;
//    }