using System.Collections.Generic;
using System.Drawing;

namespace tilecon.Converter
{
    class TilesetConverterVerticalApha : TilesetConverterVertical
    {
        public TilesetConverterVerticalApha(Maker.Tileset inputMaker, spriteMode mode, bool ignoreAlpha) : base(inputMaker, mode, ignoreAlpha) { }

        public override Bitmap[] ConvertToMV(Image img)
        {
            if (!IsConvertible(img)) return null;

            Bitmap[] images = new Bitmap[1];
            List<Bitmap> sprites = GetSprites(img);
            int i = 0;

            images[0] = GetOutputBitmap(); 
            i = PasteEachSpriteHorizontal(images[0], sprites, 0, 0, images[0].Height, images[0].Width / 4, i);
          
            return images;
        }
    }
}
