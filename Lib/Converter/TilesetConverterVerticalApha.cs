using System.Drawing;

namespace tilecon.Core.Converter
{
    /// <summary>Converter class for alpha tileset.</summary>
    public class TilesetConverterVerticalApha : TilesetConverterVertical
    {
    
        public TilesetConverterVerticalApha(ITileset inputMaker, SpriteMode mode, bool ignoreAlpha) : base(inputMaker, mode, ignoreAlpha) { }

        /// <summary>Converter the image to MV tileset.</summary>
        /// <param name="img">Image to be converted</param>
        /// <returns>An array of bitmaps converteds to MV tileset.</returns>
        public override Bitmap[] ConvertToMV(Image img)
        {
            if (!IsConvertible(img)) 
                return new Bitmap[0];

            Bitmap[] images = new Bitmap[1];
            List<Bitmap> sprites = GetSprites(img);

            images[0] = GetOutputBitmap(); 
            PasteEachSpriteHorizontal(images[0], sprites, 0, 0, images[0].Height, images[0].Width / 4, 0);
          
            return images;
        }
    }
}
