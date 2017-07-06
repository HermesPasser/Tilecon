using System.Collections.Generic;
using System.Drawing;

namespace tilecon.Converter
{
    public class TilesetConverterVerticalApha : TilesetConverterVertical
    {
        public TilesetConverterVerticalApha(ITileset inputMaker, SpriteMode mode, bool ignoreAlpha) : base(inputMaker, mode, ignoreAlpha) { }

        protected override int GetCentralizeNumber()
        {
            return 16;
        }

        public override Bitmap[] ConvertToMV(Image img)
        {
            if (!IsConvertible(img)) return null;

            Bitmap[] images = new Bitmap[1];
            List<Bitmap> sprites = GetSprites(img);

            images[0] = GetOutputBitmap(); 
            PasteEachSpriteHorizontal(images[0], sprites, 0, 0, images[0].Height, images[0].Width / 4, 0);
          
            return images;
        }
    }
}
