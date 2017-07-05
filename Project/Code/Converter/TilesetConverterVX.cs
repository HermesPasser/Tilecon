
using System.Collections.Generic;
using System.Drawing;

namespace tilecon.Converter
{
    class TilesetConverterVX : TilesetConverterBase
    {
        public TilesetConverterVX(ITileset inputMaker, SpriteMode mode, bool ignoreAlpha) : base(inputMaker, mode, ignoreAlpha) { }

        protected override List<Bitmap> GetSprites(Image img)
        {
            int spriteSize = inputTileset.SpriteSize();
            int height = inputTileset.SizeHeight();
            int width = inputTileset.SizeWidth();
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

            if (outputTileset.GetType() == typeof(Maker.MV_A5) || outputTileset.GetType() == typeof(Maker.MV_BE))
            {
                System.Windows.Forms.MessageBox.Show("penis");
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
