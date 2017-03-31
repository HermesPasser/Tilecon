using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace tilecon.Conversor
{
    public enum spriteMode
    {
        NONE, RESIZE, CENTRALIZE
    }

    class Converter : ImageProcessing
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
                for (int x = 192; x < width; x += spriteSize)
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

            if (Maker.Is2k_2k3(maker))
            {
                width = (img as Bitmap).Width;
                height = (img as Bitmap).Height;
            }

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

        private int PasteEachSprite(Bitmap bmp, List<Bitmap> sprites, int initY, int initX, int height, int width, int spriteIterator)
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

                    //To jump null sprite in r2k
                    if ((maker == Maker.version.R2000_2003_A && spriteIterator == 137) || 
                        (maker == Maker.version.R2000_2003_AB && (spriteIterator == 24 || spriteIterator == 163)))
                    {
                        spriteIterator++;
                        continue;
                    }
                    
                    //To centralize
                    int xx = x, yy = y;
                    if (mode == spriteMode.CENTRALIZE)
                    {
                        int z = 0;
                        if (Maker.GetSpriteSize(maker) == 32) z = 8;
                        else if (Maker.GetSpriteSize(maker) == 16) z = 16;

                        xx += z;
                        yy += z;
                    }
                    graphics.DrawImage(sprites[spriteIterator], xx, yy, rect, GraphicsUnit.Pixel);
                    spriteIterator++;
                }
            }
            graphics.Dispose();
            return spriteIterator;
        }

        private bool IsConvertible(Image img)
        {
            switch (maker)
            {
                case Maker.version.S97:
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
                    break;
            }

            if (Maker.Is2k_2k3(maker))
            {
                if ((img as Bitmap).Width != Maker.R2000_2003.SIZE_WIDTH || (img as Bitmap).Height != Maker.R2000_2003.SIZE_HEIGHT)
                {
                    return false;
                }
            }

            return true;
        }

        public Bitmap Get2kTileset(Bitmap bmp)
        {
            Bitmap temp, bmp1, bmp2;

            if (maker == Maker.version.R2000_2003_B)
            {
                temp = Crop(bmp, 288, 0, 192, Maker.R2000_2003.SIZE_HEIGHT); ;
                bmp1 = Crop(temp, 0, Maker.R2000_2003.SIZE_HEIGHT / 2, 192, Maker.R2000_2003.SIZE_HEIGHT / 2);
                bmp2 = Crop(temp, 96, 0, 96, Maker.R2000_2003.SIZE_HEIGHT / 2);
            }
            else
            {
                temp = Crop(bmp, 192, 0, 288, Maker.R2000_2003.SIZE_HEIGHT);
                bmp1 = Crop(temp, 0, 0, 192, Maker.R2000_2003.SIZE_HEIGHT / 2);
                bmp2 = Crop(temp, 0, Maker.R2000_2003.SIZE_HEIGHT / 2, 96, Maker.R2000_2003.SIZE_HEIGHT / 2);
            }

            if (maker == Maker.version.R2000_2003_AB)
            {
                temp = Crop(bmp, 288, 0, 192, Maker.R2000_2003.SIZE_HEIGHT); ;
                Bitmap bmp4 = Crop(temp, 0, Maker.R2000_2003.SIZE_HEIGHT / 2, 192, Maker.R2000_2003.SIZE_HEIGHT / 2);
                Bitmap bmp3 = Crop(temp, 96, 0, 96, Maker.R2000_2003.SIZE_HEIGHT / 2);

                temp = new Bitmap(576, Maker.R2000_2003.SIZE_HEIGHT / 2);
                temp = Paste(temp, bmp1, 0, 0, 192, 197);
                temp = Paste(temp, bmp2, 192, 0, 192, 197);

                temp = Paste(temp, bmp3, 288, 0, 575, Maker.R2000_2003.SIZE_HEIGHT / 2);
                temp = Paste(temp, bmp4, 384, 0, 575, Maker.R2000_2003.SIZE_HEIGHT / 2);
            }
            else
            {
                temp = new Bitmap(288, Maker.R2000_2003.SIZE_HEIGHT / 2);
                temp = Paste(temp, bmp1, 0, 0, 192, 197);
                temp = Paste(temp, bmp2, 192, 0, 191, 197);
            }
            return temp;
        }

        public Bitmap[] ConvertToMV(Image img)
        {
            if (!IsConvertible(img)) return new Bitmap[1];

            if (Maker.Is2k_2k3(maker))
                img = Get2kTileset(img as Bitmap);
            
            List<Bitmap> images = new List<Bitmap>();
            List<Bitmap> sprites = GetSprites(img).ToList<Bitmap>();

            //To ignore alpha
            if (ignoreAlpha)
                for (int g = 0; g < sprites.Count; g++)
                    if (IsAllAlphaImage(sprites[g])) sprites.Remove(sprites[g]);
            
            //To resize
            if (mode == spriteMode.RESIZE)
                for (int g = 0; g < sprites.Count; g++)
                    sprites[g] = Stretch(sprites[g], Maker.MV.SPRITE_SIZE);


            int i = 0;

            //To jump alpha tile in RM2000
            if (Maker.Is2k_2k3(maker))
            {
                i = Maker.R2000_2003.SPRITE_SIZE;
                //To resize
                if (mode == spriteMode.RESIZE)
                {
                    i = Maker.MV.SPRITE_SIZE; 
                    for (int g = 0; g < sprites.Count; g++) // To resiz more
                        sprites[g] = Stretch(sprites[g], Maker.MV.SPRITE_SIZE);
                }    
            }
                
            //For each image
            while (i < sprites.Count)
            {
                //Draw image
                Bitmap tempBmp = new Bitmap(Maker.MV.BE.SIZE, Maker.MV.BE.SIZE);
                tempBmp.SetPixel(0, 0, Color.White);

                //Draw in first part of bitmap
                i = PasteEachSprite(tempBmp, sprites, 0, 0, tempBmp.Height, tempBmp.Width / 2, i);

                //Draw in second part of bitmap
                i = PasteEachSprite(tempBmp, sprites, 0, tempBmp.Width / 2, tempBmp.Height, tempBmp.Width, i);

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