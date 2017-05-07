using System.Collections.Generic;
using System.Drawing;

namespace tilecon.Converter
{
    public enum spriteMode
    {
        NONE, CENTRALIZE, RESIZE
    }

    public abstract class TilesetConverterBase : ImageProcessing
    {
        protected int outputSpriteSize;
        protected bool ignoreAlpha;
        protected spriteMode mode;
        protected Maker.Tileset outputMaker;
        protected Maker.Tileset inputMaker;

        ~TilesetConverterBase() { }

        public TilesetConverterBase() { }

        public TilesetConverterBase(Maker.Tileset inputMaker, spriteMode mode, bool ignoreAlpha)
        {
            this.mode = mode;
            this.inputMaker = inputMaker;
            this.ignoreAlpha = ignoreAlpha;
            SetOutput();
        }

        public void SaveEachSubimage(Image img, string fileDir)
        {
            int spriteSize = Maker.GetSpriteSize(inputMaker);
            int index = fileDir.LastIndexOf(".");
            string ex = fileDir.Substring(index);
            fileDir = fileDir.Substring(0, index) + "_";

            for (int i = 0, x = 0; x < img.Width; x += spriteSize)
            {
                for (int y = 0; y < img.Height; y += spriteSize)
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
            SetOutput();
            return outputMaker;
        }

        protected void SetOutput()
        {
            if (inputMaker == Maker.Tileset.VX_Ace_A12 || inputMaker == Maker.Tileset.R2000_2003_Auto || inputMaker == Maker.Tileset.XP_Auto)
                outputMaker = Maker.Tileset.MV_A12;
            else if (inputMaker == Maker.Tileset.VX_Ace_A3)
                outputMaker = Maker.Tileset.MV_A3;
            else if (inputMaker == Maker.Tileset.VX_Ace_A4)
                outputMaker = Maker.Tileset.MV_A4;
            else if (inputMaker == Maker.Tileset.VX_Ace_A5)
                outputMaker = Maker.Tileset.MV_A5;
            else outputMaker = Maker.Tileset.MV_BC;
            outputSpriteSize = Maker.IsMV(outputMaker) ? Maker.MV.SPRITE_SIZE : -1;
        }

        protected Bitmap GetOutputBitmap()
        {
            Bitmap bmp;
            switch (outputMaker)
            {
                case Maker.Tileset.MV_A12:
                    bmp = new Bitmap(Maker.MV.A12.SIZE_WIDTH, Maker.MV.A12.SIZE_HEIGHT);
                    break;
                case Maker.Tileset.MV_A3:
                    bmp = new Bitmap(Maker.MV.A3.SIZE_WIDTH, Maker.MV.A3.SIZE_HEIGHT);
                    break;
                case Maker.Tileset.MV_A4:
                    bmp = new Bitmap(Maker.MV.A4.SIZE_WIDTH, Maker.MV.A4.SIZE_HEIGHT);
                    break;
                case Maker.Tileset.MV_A5:
                    bmp = new Bitmap(Maker.MV.A5.SIZE_WIDTH, Maker.MV.A5.SIZE_HEIGHT);
                    break;
                default:
                    bmp = new Bitmap(Maker.MV.BE.SIZE, Maker.MV.BE.SIZE);
                    break;
            }
            bmp.SetPixel(0, 0, Color.White);
            return bmp;
        }

        protected List<Bitmap> SetModes(List<Bitmap> sprites)
        {
            //To resize
            for (int g = 0; mode == spriteMode.RESIZE && g < sprites.Count; g++)
                sprites[g] = Stretch(sprites[g], Maker.MV.SPRITE_SIZE);

            //To ignore alpha
            for (int g = 0; ignoreAlpha && g < sprites.Count; g++)
                if (IsAllAlphaImage(sprites[g])) sprites.Remove(sprites[g]);
            return sprites;
        }
        
        protected virtual bool IsConvertible(Image img)
        {
            if (inputMaker != Maker.Tileset.XP)
            {
                if (img.Width != Maker.GetSizeWidth(inputMaker) || img.Height != Maker.GetSizeHeight(inputMaker))
                {
                    System.Windows.Forms.MessageBox.Show(Vocab.errorMessage);
                    return false;
                }
            }
            return true;
        }

        protected int PasteEachSpriteVertical(Bitmap bmp, List<Bitmap> sprites, int initY, int initX, int height, int width, int spriteIterator)
        {
            Rectangle rect = new Rectangle(0, 0, sprites[0].Width, sprites[0].Height);
            Graphics graphics = Graphics.FromImage(bmp);
           
            //To resize
            if (mode == spriteMode.RESIZE)
                rect = new Rectangle(0, 0, outputSpriteSize, outputSpriteSize);

            for (int x = initX; x < width; x += outputSpriteSize)
            {
                for (int y = initY; y < height; y += outputSpriteSize)
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

        protected int PasteEachSpriteHorizontal(Bitmap bmp, List<Bitmap> sprites, int initY, int initX, int height, int width, int spriteIterator)
        {
            Rectangle rect = new Rectangle(0, 0, sprites[0].Width, sprites[0].Height);
            Graphics graphics = Graphics.FromImage(bmp);

            //To resize
            if (mode == spriteMode.RESIZE)
                rect = new Rectangle(0, 0, outputSpriteSize, outputSpriteSize);

            for (int y = initY; y < height; y += outputSpriteSize)
            {
                for (int x = initX; x < width; x += outputSpriteSize)
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

        protected abstract List<Bitmap> GetSprites(Image img);

        public abstract Bitmap[] ConvertToMV(Image img);   
    }
}