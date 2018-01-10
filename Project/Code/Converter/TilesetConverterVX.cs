using System.Collections.Generic;
using System.Drawing;

namespace tilecon.Tileset.Converter
{
    /// <summary>Converter class for vx tilesets.</summary>
    public class TilesetConverterVX : TilesetConverterBase
    {
        /// <summary>Default constructor.</summary>
        /// <param name="inputMaker">Tileset type to be converted</param>
        /// <param name="mode">Mode how sprites should be pasted into the converted image.</param>
        /// <param name="ignoreAlpha">Flag to ignore empty sprites.</param>
        public TilesetConverterVX(ITileset inputMaker, SpriteMode mode, bool ignoreAlpha) : base(inputMaker, mode, ignoreAlpha) { }

        /// <summary>Split the image in various sprites.</summary>
        /// <param name="img">Image to be slip.</param>
        /// <returns>A list of sprites.</returns>
        protected override List<Bitmap> GetSprites(Image img)
        {
            int spriteSize = inputTileset.SpriteSize();
            int height = inputTileset.SizeHeight();
            int width = inputTileset.SizeWidth();
            Bitmap[] sprites = new Bitmap[(img.Width / spriteSize) * (img.Height / spriteSize)];

            for (int x = 0, i = 0; x < width; x += spriteSize)
            {
                for (int y = 0; y < height; y += spriteSize, i++)
                {
                    sprites[i] = Crop(img as Bitmap, x, y, spriteSize, spriteSize); 
                }
            }
            return RemoveAlphaSprites(sprites);
        }

        /// <summary>Converter the image to MV tileset.</summary>
        /// <param name="img">Image to be converted</param>
        /// <returns>An array of bitmaps converteds to MV tileset.</returns>
        public override Bitmap[] ConvertToMV(Image img)
        {
            if (!IsConvertible(img)) return new Bitmap[1];
            List<Bitmap> images = new List<Bitmap>();
            List<Bitmap> sprites = GetSprites(img);
            int i = 0;

            if (outputTileset.GetType() == typeof(Maker.MV_A5) || outputTileset.GetType() == typeof(Maker.MV_BE))
            {
                TilesetConverterVertical con = new TilesetConverterVertical(inputTileset, mode, ignoreAlpha);
                sprites.Clear();
                sprites = con.GetSprites(img as Bitmap);
            }
            //For each image
            while (i < sprites.Count)
            {
                Bitmap tempBmp = GetOutputBitmap();

                //Set image format
                switch (outputTileset.TilesetName())
                {
                    case Maker.MV_A12.NAME:
                    case Maker.MV_A4.NAME:
                    case Maker.MV_A3.NAME:
                        i = PasteEachSpriteVertical(tempBmp, sprites, 0, 0, tempBmp.Height, tempBmp.Width / 2, i);
                        i = PasteEachSpriteVertical(tempBmp, sprites, 0, tempBmp.Width / 2, tempBmp.Height, tempBmp.Width, i);
                        break;
                    case Maker.MV_A5.NAME: 
                        i = PasteEachSpriteHorizontal(tempBmp, sprites, 0, 0, tempBmp.Height, tempBmp.Width, i);
                        break;
                    case Maker.MV_BE.NAME:
                        i = PasteEachSpriteHorizontal(tempBmp, sprites, 0, 0, tempBmp.Height, tempBmp.Width, i);
                        break;
                }
                images.Add(tempBmp);
            }
            return images.ToArray();
        }
    }
}
