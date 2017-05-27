using System.Collections.Generic;
using System.Drawing;

namespace tilecon.Converter
{
    public abstract class TilesetConverterBase : ImageProcessing
    {
        protected int outputSpriteSize;
        protected bool ignoreAlpha;
        protected SpriteMode mode;
        protected Maker.Tileset outputMaker;
        protected Maker.Tileset inputMaker;

        ~TilesetConverterBase() { }

        public TilesetConverterBase() { }

        public TilesetConverterBase(Maker.Tileset inputMaker, SpriteMode mode, bool ignoreAlpha)
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
            switch (inputMaker)
            {
                case Maker.Tileset.VX_Ace_A12:
                case Maker.Tileset.R2000_2003_Auto:
                case Maker.Tileset.XP_Auto:
                    outputMaker = Maker.Tileset.MV_A12;
                    break;
                case Maker.Tileset.VX_Ace_A3:
                    outputMaker = Maker.Tileset.MV_A3;
                    break;
                case Maker.Tileset.VX_Ace_A4:
                    outputMaker = Maker.Tileset.MV_A4;
                    break;
                case Maker.Tileset.VX_Ace_A5:
                    outputMaker = Maker.Tileset.MV_A5;
                    break;
                default:
                    outputMaker = Maker.Tileset.MV_BC;
                    break;
            }
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

        public Bitmap SetModeInSprite(Image img, int spriteSize)
        {
            int x = 0, y = 0, z = GetCentralizeNumber();
            Bitmap temp = new Bitmap(spriteSize, spriteSize);
            Bitmap bmp = img as Bitmap;
            
            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
            Graphics graphics = Graphics.FromImage(temp);

            switch (mode)
            {
                case SpriteMode.RESIZE:                    
                    return Stretch(bmp, spriteSize);
                case SpriteMode.ALIGN_TOP_LEFT:
                    break;
                case SpriteMode.ALIGN_TOP_CENTER:
                    x += z;
                    break;
                case SpriteMode.ALIGN_TOP_RIGHT:
                    x += z + z;
                    break;
                case SpriteMode.ALIGN_MIDDLE_LEFT:
                    y += z;
                    break;
                case SpriteMode.ALIGN_MIDDLE_CENTER:
                    x += z;
                    y += z;
                    break;
                case SpriteMode.ALIGN_MIDDLE_RIGHT:
                    x += z + z;
                    y += z;
                    break;
                case SpriteMode.ALIGN_BOTTOM_LEFT:
                    y += z + z;
                    break;
                case SpriteMode.ALIGN_BOTTOM_CENTER:
                    x += z;
                    y += z + z;
                    break;
                case SpriteMode.ALIGN_BOTTOM_RIGHT:
                    x += z + z;
                    y += z + z;
                    break;
            }

            graphics.DrawImage(bmp, x, y, rect, GraphicsUnit.Pixel);
            graphics.Dispose();
            bmp = temp;
            return bmp;
        }

        protected List<Bitmap> RemoveAlphaImages(List<Bitmap> sprites)
        {
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

        protected int PasteEachSpriteVertical(Bitmap origin, List<Bitmap> spritesToBePasted, int initY, int initX, int height, int width, int currentSprite)
        {
            Rectangle rect = new Rectangle(0, 0, outputSpriteSize, outputSpriteSize);
            Graphics graphics = Graphics.FromImage(origin);
           
            for (int x = initX; x < width; x += outputSpriteSize)
            {
                for (int y = initY; y < height; y += outputSpriteSize)
                {
                    if (currentSprite >= spritesToBePasted.Count || spritesToBePasted[currentSprite] == null)
                        break;

                    spritesToBePasted[currentSprite] = SetModeInSprite(spritesToBePasted[currentSprite], outputSpriteSize);
                    graphics.DrawImage(spritesToBePasted[currentSprite], x, y, rect, GraphicsUnit.Pixel);
                    currentSprite++;
                }
            }
            graphics.Dispose();
            return currentSprite;
        }

        protected int PasteEachSpriteHorizontal(Bitmap origin, List<Bitmap> spritesToBePasted, int initY, int initX, int height, int width, int currentSprite)
        {   
            Rectangle rect = new Rectangle(0, 0, outputSpriteSize, outputSpriteSize);
            Graphics graphics = Graphics.FromImage(origin);
            
            for (int y = initY; y < height; y += outputSpriteSize)
            {
                for (int x = initX; x < width; x += outputSpriteSize)
                {
                    if (currentSprite >= spritesToBePasted.Count || spritesToBePasted[currentSprite] == null)
                        break;

                    spritesToBePasted[currentSprite] = SetModeInSprite(spritesToBePasted[currentSprite], outputSpriteSize);
                    graphics.DrawImage(spritesToBePasted[currentSprite], x, y, rect, GraphicsUnit.Pixel);
                    currentSprite++;
                }
            }
            graphics.Dispose();
            return currentSprite;
        }
        
        public Bitmap TilesToTileset(List<Bitmap> bmps, int width, int height, int spriteSize)
        {
            Bitmap mv = new Bitmap(width, height);

            for (int y = 0, i = 0; y < height; y += spriteSize)
            {
                for (int x = 0; x < width; i++, x += spriteSize)
                {
                    if (bmps[i] != null)
                        mv = Paste(mv, bmps[i], x, y, spriteSize, spriteSize);
                }
            }
            return mv;
        }

        protected abstract int GetCentralizeNumber();

        protected abstract List<Bitmap> GetSprites(Image img);

        public abstract Bitmap[] ConvertToMV(Image img);   
    }
}