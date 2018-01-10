using System.Collections.Generic;
using System.Drawing;

namespace tilecon.Tileset.Converter
{
    /// <summary>Converter class for alpha tileset.</summary>
    public class TilesetConverterVerticalApha : TilesetConverterVertical
    {
        /// <summary>Default constructor.</summary>
        /// <param name="inputMaker">Tileset type to be converted</param>
        /// <param name="mode">Mode how sprites should be pasted into the converted image.</param>
        /// <param name="ignoreAlpha">Flag to ignore empty sprites.</param>
        public TilesetConverterVerticalApha(ITileset inputMaker, SpriteMode mode, bool ignoreAlpha) : base(inputMaker, mode, ignoreAlpha) { }

        /// <summary>Converter the image to MV tileset.</summary>
        /// <param name="img">Image to be converted</param>
        /// <returns>An array of bitmaps converteds to MV tileset.</returns>
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
