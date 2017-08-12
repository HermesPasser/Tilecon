﻿using System.Collections.Generic;
using System.Drawing;

namespace tilecon.Converter
{
    /// <summary>Super class for all tileset converters.</summary>
    public abstract class TilesetConverterBase : ImageProcessing
    {
        /// <summary>Size sprite in output tileset.</summary>
        protected int outputSpriteSize;
        
        /// <summary>Flag for ignore empty sprites.</summary>
        protected bool ignoreAlpha;

        /// <summary>Mode how sprites should be pasted into the output tileset.</summary>
        protected SpriteMode mode;

        /// <summary>Tileset type to be converted.</summary>
        protected ITileset inputTileset;

        /// <summary>Tileset type after be converted.</summary>
        protected ITileset outputTileset;

        //~TilesetConverterBase() { }

        /// <summary>The empty constructor of the class to allow it to be inherited.</summary>
        public TilesetConverterBase() { }

        /// <summary>Default constructor.</summary>
        /// <param name="inputMaker">Tileset type to be converted</param>
        /// <param name="mode">Mode how sprites should be pasted into the converted image.</param>
        /// <param name="ignoreAlpha">Flag for ignore empty sprites.</param>
        public TilesetConverterBase(ITileset inputMaker, SpriteMode mode, bool ignoreAlpha)
        {
            this.mode = mode;
            this.inputTileset = inputMaker;
            this.ignoreAlpha = ignoreAlpha;
            SetOutputTileset();
        }

        /// <summary>Split and save each sprite</summary>
        /// <param name="img">Image to be cropped in sprites.</param>
        /// <param name="fileDir">The directory for the sprites to be saved.</param>
        public void SaveEachSubimage(Image img, string fileDir)
        {
            int spriteSize = inputTileset.SpriteSize();
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

        /// <summary>Set  the output tileset and output tileset size based in the input tileset.</summary>
        /// <returns>The output tileset.</returns>
        public ITileset SetOutputTileset()
        {
            switch (inputTileset.TilesetName())
            {
                case Maker.R2k_2k3_AnimObj.NAME:
                    outputTileset = new Maker.MV_Other();
                    break;
                case Maker.VX_Ace_A12.NAME:
                case Maker.R2k_2k3_Auto.NAME:
                case Maker.XP_Auto.NAME:
                    outputTileset = new Maker.MV_A12();
                    break;
                case Maker.VX_Ace_A3.NAME:
                    outputTileset = new Maker.MV_A3();
                    break;
                case Maker.VX_Ace_A4.NAME:
                    outputTileset = new Maker.MV_A4();
                    break;
                case Maker.VX_Ace_A5.NAME:
                    outputTileset = new Maker.MV_A5();
                    break;
                default:
                    outputTileset = new Maker.MV_BE();
                    break;
            }

            // If output is a MV or MV childen then return mv sprite size else return -1
            outputSpriteSize = outputTileset is Maker.MV ? Maker.MV.SPRITE_SIZE : -1;
            return outputTileset;
        }
        
        /// <summary>Create a empty bitmap with the size of the output tileset.</summary>
        /// <returns>Bitmap with the size of output tileset.</returns>
        protected Bitmap GetOutputBitmap()
        {
            Bitmap bmp = new Bitmap(outputTileset.SizeWidth(), outputTileset.SizeHeight());
            bmp.SetPixel(0, 0, Color.White);
            return bmp;
        }

        /// <summary>Set mode in the sprite.</summary>
        /// <param name="img">Sprite to mode be setted.</param>
        /// <param name="spriteSize">Size of new sprite.</param>
        /// <returns>A bitmap who contains the sprite with the setted mode.</returns>
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

        /// <summary>Remove empty sprites of a list.</summary>
        /// <param name="sprites">List of sprites.</param>
        /// <returns>List of sprites not empty.</returns>
        protected List<Bitmap> RemoveAlphaSprites(List<Bitmap> sprites)
        {
            for (int g = 0; ignoreAlpha && g < sprites.Count; g++)
                if (IsAllAlphaImage(sprites[g])) sprites.Remove(sprites[g]);
            return sprites;
        }
        
        /// <summary>if the image is convertible to MV tileset.</summary>
        /// <param name="img">Image to be checked.</param>
        /// <returns>Return true if the image is convertible and false if not.</returns>
        protected virtual bool IsConvertible(Image img)
        {
            if (inputTileset.GetType() != typeof(Maker.XP_Tile))
            {
                if (img.Width != inputTileset.SizeWidth() || img.Height != inputTileset.SizeHeight())
                {
                    System.Windows.Forms.MessageBox.Show(Vocab.errorMessage);
                    return false;
                }
            }
            return true;
        }

        /// <summary>Paste the sprites in the bitmap and return the index of last sprite pasted started vertically.</summary>
        /// <param name="origin">The bitmap where the sprites will be pasted.</param>
        /// <param name="spritesToBePasted">List of sprites to be pasted in the bitmap.</param>
        /// <param name="initY">Initial position in y where sprites started to be pasted.</param>
        /// <param name="initX">Initial position in x where sprites started to be pasted.</param>
        /// <param name="height">Max height to be pasted in the bitmap.</param>
        /// <param name="width">Max width to be pasted in the bitmap.</param>
        /// <param name="currentSprite">The current sprite to be pasted.</param>
        /// <returns>currentSprite will be returned in case it is necessary to continue the process in another image.</returns>
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

        /// <summary>Paste the sprites in the bitmap and return the index of last sprite pasted started horizontally.</summary>
        /// <param name="origin">The bitmap where the sprites will be pasted.</param>
        /// <param name="spritesToBePasted">List of sprites to be pasted in the bitmap.</param>
        /// <param name="initY">Initial position in y where sprites started to be pasted.</param>
        /// <param name="initX">Initial position in x where sprites started to be pasted.</param>
        /// <param name="height">Max height to be pasted in the bitmap.</param>
        /// <param name="width">Max width to be pasted in the bitmap.</param>
        /// <param name="currentSprite">The current sprite to be pasted.</param>
        /// <returns>currentSprite will be returned in case it is necessary to continue the process in another image.</returns>
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
        
        /// <summary>Make a tileset of a bitmaps list.</summary>
        /// <param name="bmps">List of bitmaps to be included in tileset.</param>
        /// <param name="width">With of tileset.</param>
        /// <param name="height">Height of tileset.</param>
        /// <param name="spriteSize">Size of each sprite in tileset.</param>
        /// <returns>Return the tileset as bitmap.</returns>
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

        /// <summary>Get the number of pixels to be moved to center the sprite on the tileset.</summary>
        /// <returns>The number of pixels to be moved to center the sprite on the tileset.</returns>
        protected abstract int GetCentralizeNumber();

        /// <summary>Split the image in various sprites.</summary>
        /// <param name="img">Image to be slip.</param>
        /// <returns>A list of sprites.</returns>
        protected abstract List<Bitmap> GetSprites(Image img);

        /// <summary>Converter the image to MV tileset.</summary>
        /// <param name="img">Image to be converted</param>
        /// <returns>An array of bitmaps converteds to MV tileset.</returns>
        public abstract Bitmap[] ConvertToMV(Image img);   
    }
}