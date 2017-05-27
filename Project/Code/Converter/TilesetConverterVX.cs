
using System.Collections.Generic;
using System.Drawing;

namespace tilecon.Converter
{
    class TilesetConverterVX : TilesetConverterBase
    {
        public TilesetConverterVX(Maker.Tileset inputMaker, SpriteMode mode, bool ignoreAlpha) : base(inputMaker, mode, ignoreAlpha) { }

        protected override List<Bitmap> GetSprites(Image img)
        {
            int spriteSize = Maker.GetSpriteSize(inputMaker);
            int height = Maker.GetSizeHeight(inputMaker);
            int width = Maker.GetSizeWidth(inputMaker);
            List<Bitmap> sprites = new List<Bitmap>();

            for (int x = 0, i = 0; x < width; x += spriteSize)
            {
                for (int y = 0; y < height; y += spriteSize)
                {
                    sprites.Add(Crop(img as Bitmap, x, y, spriteSize, spriteSize));
                    i++;
                }
            }
            return RemoveAlphaImages(sprites);
        }

        protected override int GetCentralizeNumber()
        {
            return 8;
        }

        public override Bitmap[] ConvertToMV(Image img)
        {
            if (!IsConvertible(img)) return new Bitmap[1];
            List<Bitmap> images = new List<Bitmap>();
            List<Bitmap> sprites = GetSprites(img);
            int i = 0;

            if (outputMaker == Maker.Tileset.MV_A5 || outputMaker == Maker.Tileset.MV_BC)
            {
                TilesetConverterVertical con = new TilesetConverterVertical(inputMaker, mode, ignoreAlpha);
                sprites.Clear();
                sprites = con.GetSprites(img as Bitmap);
            }
            
            //For each image
            while (i < sprites.Count)
            {
                Bitmap tempBmp = GetOutputBitmap();

                //Set image format
                switch (outputMaker)
                {
                    case Maker.Tileset.MV_A12:
                    case Maker.Tileset.MV_A4:
                    case Maker.Tileset.MV_A3:
                        i = PasteEachSpriteVertical(tempBmp, sprites, 0, 0, tempBmp.Height, tempBmp.Width / 2, i);
                        i = PasteEachSpriteVertical(tempBmp, sprites, 0, tempBmp.Width / 2, tempBmp.Height, tempBmp.Width, i);
                        break;
                    case Maker.Tileset.MV_A5: 
                        i = PasteEachSpriteHorizontal(tempBmp, sprites, 0, 0, tempBmp.Height, tempBmp.Width, i);
                        break;
                    case Maker.Tileset.MV_BC:
                        i = PasteEachSpriteHorizontal(tempBmp, sprites, 0, 0, tempBmp.Height, tempBmp.Width, i);
                        break;
                }
                images.Add(tempBmp);
            }
            return images.ToArray();
        }
    }
}
