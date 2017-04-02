using System.Collections.Generic;
using System.Drawing;
using System.Linq;

/// <summary>
/// 5-e dão erro
/// </summary>

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
        private Maker.Tileset outputMaker;
        private Maker.Tileset inputMaker;

        ~Converter() { }

        public Converter(Maker.Tileset inputMaker, spriteMode mode, bool ignoreAlpha)
        {
            this.mode = mode;
            this.inputMaker = inputMaker;
            this.ignoreAlpha = ignoreAlpha;
            SetOutputMaker();
        }

        public Converter(Maker.Tileset inputMaker)
        {
            this.inputMaker = inputMaker;
            this.mode = spriteMode.NONE;
            this.ignoreAlpha = false;
        }

        public void SaveEachSubimage(Image img, string fileDir)
        {
            int spriteSize = Maker.GetSpriteSize(inputMaker);
            int height = Maker.GetSizeHeight(inputMaker);
            int width = Maker.GetSizeWidth(inputMaker);
            int index = fileDir.LastIndexOf(".");
            string ex = fileDir.Substring(index);
            fileDir = fileDir.Substring(0, index) + "_";

            if (height == -1) height = img.Height;

            for (int i = 0, y = 0; y < height; y += spriteSize)
            {
                for (int x = 0; x < width; x += spriteSize)
                {
                    int ipp = i + 1;
                    string s = ipp > 99 ? s = ipp.ToString() : ipp > 9 ? s = ipp.ToString() : s = "0" + ipp;
                    Bitmap b = Crop(img as Bitmap, x, y, spriteSize, spriteSize);
                    b.Save(fileDir + s + ex);
                    i++;
                }
            }
        }

        public Maker.Tileset GetOutputMaker()
        {
            SetOutputMaker();
            return outputMaker;
        }

        private void SetOutputMaker()
        {
            outputMaker = Maker.Tileset.MV_A4;

            if (inputMaker == Maker.Tileset.VX_Ace_A12)
                outputMaker = Maker.Tileset.MV_A12;
            else if (inputMaker == Maker.Tileset.VX_Ace_A4)
                outputMaker = Maker.Tileset.MV_A4;
            else if (inputMaker == Maker.Tileset.VX_Ace_A5)
                outputMaker = Maker.Tileset.MV_A5;
            else outputMaker = Maker.Tileset.MV_BC;
        }

        private Bitmap GetOutputBitmap()
        {
            Bitmap bmp;
            switch (outputMaker)
            {
                case Maker.Tileset.MV_A12:
                    bmp = new Bitmap(Maker.MV.A12.SIZE_WIDTH, Maker.MV.A12.SIZE_HEIGHT);
                    break;
                case Maker.Tileset.MV_A4:
                    bmp = new Bitmap(Maker.MV.A4.SIZE_WIDTH, Maker.MV.A4.SIZE_HEIGHT);
                    break;
                case Maker.Tileset.MV_A5:
                    bmp = new Bitmap(Maker.MV.A5.SIZE_WIDTH, Maker.MV.A5.SIZE_HEIGHT);
                    break;
                default:
                    bmp = new Bitmap(Maker.MV.BC.SIZE, Maker.MV.BC.SIZE);
                    break;
            }
            bmp.SetPixel(0, 0, Color.White);
            return bmp;
        }

        private Bitmap[] GetSprites(Image img)
        {
            int spriteSize = Maker.GetSpriteSize(inputMaker);
            int height = Maker.GetSizeHeight(inputMaker);
            int width = Maker.GetSizeWidth(inputMaker);

            int cropSize = ((img.Height / spriteSize) * (img.Width / spriteSize));

            if (height == -1) height = img.Height;

            if (Maker.Is2K_2K3(inputMaker))
            {
                width = (img as Bitmap).Width;
                height = (img as Bitmap).Height;
            }

            if (Maker.IsVX_Ace(inputMaker))
                return GetSpritesHorizontal(img, width, height, cropSize, spriteSize);
            else
                return GetSpriteVertical(img, width, height, cropSize, spriteSize);
        }

        private Bitmap[] GetSpriteVertical(Image img, int width, int height, int cropSize, int spriteSize)
        {
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

        private Bitmap[] GetSpritesHorizontal(Image img, int width, int height, int cropSize, int spriteSize)
        {
            Bitmap[] sprites = new Bitmap[cropSize];

            for (int x = 0, i = 0; x < width; x += spriteSize)
            {
                for (int y = 0; y < height; y += spriteSize)
                {
                    sprites[i] = Crop(img as Bitmap, x, y, spriteSize, spriteSize);
                    i++;
                }
            }
            return sprites;
        }

        private int PasteEachSpriteHorizontal(Bitmap bmp, List<Bitmap> sprites, int initY, int initX, int height, int width, int spriteIterator)
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
                        int z = 0;
                        if (Maker.GetSpriteSize(inputMaker) == 32) z = 8;
                        else if (Maker.GetSpriteSize(inputMaker) == 16) z = 16;

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

        private int PasteEachSpriteVertical(Bitmap bmp, List<Bitmap> sprites, int initY, int initX, int height, int width, int spriteIterator)
        {
            Rectangle rect = new Rectangle(0, 0, sprites[0].Width, sprites[0].Height);
            Graphics graphics = Graphics.FromImage(bmp);

            //To resize
            if (mode == spriteMode.RESIZE)
                rect = new Rectangle(0, 0, Maker.MV.SPRITE_SIZE, Maker.MV.SPRITE_SIZE);

            for (int x = initX; x < width; x += Maker.MV.SPRITE_SIZE)
            {
                for (int y = initY; y < height; y += Maker.MV.SPRITE_SIZE)
                {
                    if (spriteIterator >= sprites.Count || sprites[spriteIterator] == null)
                        break;

                    //To centralize
                    int xx = x, yy = y;
                    if (mode == spriteMode.CENTRALIZE)
                    {
                        int z = 0;
                        if (Maker.GetSpriteSize(inputMaker) == 32) z = 8;
                        else if (Maker.GetSpriteSize(inputMaker) == 16) z = 16;

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
            if (inputMaker != Maker.Tileset.XP)
            {
                if (img.Width != Maker.GetSizeWidth(inputMaker) || img.Height != Maker.GetSizeHeight(inputMaker))
                {
                    System.Windows.Forms.MessageBox.Show(Vocab.errorMessage);
                    return false;
                }
            }
            //else if (img.Width != Maker.XP.SIZE_WIDTH){return false;}
            return true;
        }

        public Bitmap Get2kTileset(Bitmap bmp)
        {
            Bitmap temp, bmp1, bmp2;

            if (inputMaker == Maker.Tileset.R2000_2003_B)
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

            if (inputMaker == Maker.Tileset.R2000_2003_AB)
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

            if (Maker.Is2K_2K3(inputMaker))
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

            // To resize more
            if (Maker.Is2K_2K3(inputMaker) && mode == spriteMode.RESIZE) 
                for (int g = 0; g < sprites.Count; g++) 
                    sprites[g] = Stretch(sprites[g], Maker.MV.SPRITE_SIZE);

            //For each image
            while (i < sprites.Count)
            {
                //Draw image
                Bitmap tempBmp = GetOutputBitmap();

                //Set image format
                switch (outputMaker)
                {
                    case Maker.Tileset.MV_A12:
                    case Maker.Tileset.MV_A4:
                        i = PasteEachSpriteVertical(tempBmp, sprites, 0, 0, tempBmp.Height, tempBmp.Width / 2, i);
                        i = PasteEachSpriteVertical(tempBmp, sprites, 0, tempBmp.Width / 2, tempBmp.Height, tempBmp.Width, i);
                        break;
                    case Maker.Tileset.MV_A5:
                        i = PasteEachSpriteHorizontal(tempBmp, sprites, 0, 0, tempBmp.Height, tempBmp.Width, i);
                        break;
                    case Maker.Tileset.MV_BC:
                        i = PasteEachSpriteHorizontal(tempBmp, sprites, 0, 0, tempBmp.Height, tempBmp.Width / 2, i);
                        i = PasteEachSpriteHorizontal(tempBmp, sprites, 0, tempBmp.Width / 2, tempBmp.Height, tempBmp.Width, i);
                        break;
                }

                //Add image to the list
                images.Add(tempBmp);
            }
            return images.ToArray();
        }
    }
}