using System.Collections.Generic;
using System.Threading;
using System.Drawing;
using System.Linq;

namespace tilecon.Conversor
{
    public enum spriteMode
    {
        NONE, RESIZE, CENTRALIZE
    }

    class Converter
    { 
        private bool ignoreAlpha;
        private spriteMode mode;
        private Maker.version maker;

        public Converter(Maker.version maker, spriteMode mode, bool ignoreAlpha)
        {
            this.maker = maker;
            this.mode = mode;
            this.ignoreAlpha = ignoreAlpha;
        }

        public Converter(Maker.version maker)
        {
            this.maker = maker;
            this.mode = spriteMode.NONE;
            this.ignoreAlpha = false;
        }

        private Bitmap Crop(Bitmap src, int x, int y, int width, int height)
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

        public void SaveEachSubimage(Image img, string fileDir)
        {
            int spriteSize = Maker.GetSpriteSize(maker);
            int height = Maker.GetSizeHeight(maker);
            int width = Maker.GetSizeWidth(maker);
            int index = fileDir.LastIndexOf(".");
            fileDir = fileDir.Substring(0, index) + "_";

            if (height == -1) height = img.Height;

            for (int i = 0, y = 0; y < height; y += spriteSize)
            {
                for (int x = 0; x < width; x += spriteSize)
                {
                    int ipp = i + 1;
                    string s = ipp > 99 ? s = ipp.ToString() : ipp > 9 ? s = ipp.ToString() : s = "0" + ipp;
                    Bitmap b = Crop(img as Bitmap, x, y, spriteSize, spriteSize);
                    b.Save(fileDir + s + ".png");
                    i++;
                }
            }
        }

        private Bitmap[] GetSprites(Image img)
        {
            int spriteSize = Maker.GetSpriteSize(maker);
            int height = Maker.GetSizeHeight(maker);
            int width = Maker.GetSizeWidth(maker);
            int cropSize = ((img.Height / spriteSize) * (img.Width / spriteSize));

            if (height == -1) height = img.Height;

            Bitmap[] sprites = new Bitmap[cropSize];

            for (int y = 0, i = 0; y < height; y += spriteSize)
            {
                for (int x = 0; x < width; x += spriteSize)
                {
                    sprites[i] = Crop(img as Bitmap, x, y, spriteSize, spriteSize);
                    i++;
                }
            }
            return sprites;
        }

        private bool IsAlpha(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                    if (bmp.GetPixel(x, y).A == 0)
                        return true;
            return false;
        }

        private Bitmap Resize(Bitmap bmp, int newSpriteSize)
        {
            Bitmap tempBmp = new Bitmap(newSpriteSize, newSpriteSize);
            bool b = false;

            int i = 0;//, i = 0
            for (int y = 0; y < bmp.Height; y++)
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
            tempBmp = new Bitmap(newSpriteSize, newSpriteSize);

            for (int x = 0; x < bmp.Width; x++)
            {
                i = 0;
                for (int y = 0; y < bmp.Height; y++, i++)
                {
                    if (i < tempBmp.Height)
                    {
                        tempBmp.SetPixel(x, i, bmp.GetPixel(x, y));
                        if (b && i + 1 < tempBmp.Height)
                        {
                            tempBmp.SetPixel(x, i + 1, bmp.GetPixel(x, y));
                            i++;
                        }
                    }
                    b = !b;
                }
            }
            //tempBmp.Save("C:\\Users\\DSL\\Desktop\\tilecon\\0D + " + g + ".png");
            return tempBmp;
        }

        private int PasteMV(Bitmap bmp, List<Bitmap> sprites, int initY, int initX, int height, int width, int spriteIterator)
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
                        xx += Maker.GetSpriteSize(maker) - (Maker.MV.SPRITE_SIZE / 2);
                        yy += Maker.GetSpriteSize(maker) - (Maker.MV.SPRITE_SIZE / 2);
                    } //To resize
                    //else if (mode == spriteMode.RESIZE)
                    //    sprites[spriteIterator] = Resize(sprites[spriteIterator], Maker.MV.SPRITE_SIZE);

                    //so o fato desse if estar descomentado ja buga tudo mesmo que o resize dentro dele esteja comentado
                    //To ignore alpha
                    //if (ignoreAlpha && IsAlpha(sprites[spriteIterator], spriteIterator))
                    //{
                    //    if (spriteIterator + 1 < sprites.Count)
                    //    {
                    //        //        // AQUI MORA O ERRO

                    //        //        //To resize
                    //        //        if (mode == spriteMode.RESIZE)
                    //        //            sprites[spriteIterator + 1] = Resize(sprites[spriteIterator], Maker.MV.SPRITE_SIZE);

                    //        graphics.DrawImage(sprites[spriteIterator + 1], xx, yy, rect, GraphicsUnit.Pixel);

                    //    }
                    //    spriteIterator++;
                    //}
                    else
                        graphics.DrawImage(sprites[spriteIterator], xx, yy, rect, GraphicsUnit.Pixel);

                    spriteIterator++;
                }
            }
            graphics.Dispose();
            return spriteIterator;
        }

        private bool IsConvertible(Image img)
        {
            if (maker == Maker.version.S97)
            {
                if (img.Width != Maker.S97.SIZE_WIDTH)
                {
                    System.Windows.Forms.MessageBox.Show(Vocab.errorMessage[0]);
                    return false;
                }
                else if (img.Height > Maker.S97.SIZE_HEIGHT)
                {
                    System.Windows.Forms.MessageBox.Show(Vocab.errorMessage[1]);
                    return false;
                }
            }
            return true;
        }


        public Bitmap[] ConvertToMV(Image img)
        {
            if (!IsConvertible(img)) return new Bitmap[1];

            List<Bitmap> images = new List<Bitmap>();
            List<Bitmap> sprites = GetSprites(img).ToList<Bitmap>();

            //TESTANDO SE DAR RESIZE ANTES MUDA ALGO

            //To ignore alpha
            if (ignoreAlpha)
                for (int g = 0; g < sprites.Count; g++)
                    if (IsAlpha(sprites[g])) sprites.Remove(sprites[g]);

            //To resize
            if (mode == spriteMode.RESIZE)
                for (int g = 0; g < sprites.Count; g++)
                    sprites[g] = Resize(sprites[g], Maker.MV.SPRITE_SIZE);

        //FIM TESTE

            int i = 0;

            //For each image
            while (i < 1)//sprites.Count)
            {
                //Draw image
                Bitmap tempBmp = new Bitmap(Maker.MV.BE.SIZE, Maker.MV.BE.SIZE);
                tempBmp.SetPixel(0, 0, Color.White);

                //Draw in first part of bitmap
                i = PasteMV(tempBmp, sprites, 0, 0, tempBmp.Height, tempBmp.Width / 2, i);

                //Draw in second part of bitmap
                i = PasteMV(tempBmp, sprites, 0, tempBmp.Width / 2, tempBmp.Height, tempBmp.Width, i);

                //Add image to the list
                images.Add(tempBmp);
            }

            //System.Windows.Forms.MessageBox.Show("Done");
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