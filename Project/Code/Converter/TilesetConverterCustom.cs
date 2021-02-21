using System.Drawing;
using static tilecon.ImageEditor;

namespace tilecon.Tileset.Converter
{
    /// <summary>Custom tileset class</summary>
    public class TilesetConverterCustom : TilesetConverterVertical
    {
        /// <summary>The empty constructor of the class to allow it to be inherited.</summary>
        public TilesetConverterCustom() { }

        /// <summary>Default constructor.</summary>
        /// <param name="mode">Mode how sprites should be pasted into the converted image.</param>
        /// <param name="ignoreAlpha">Flag toFlag to ignore ignore empty sprites.</param>
        /// <param name="spriteSize">Tile width.</param>
        public TilesetConverterCustom(SpriteMode mode, bool ignoreAlpha, int spriteSize = 0) : base(new Maker.Custom(), mode, ignoreAlpha)
        {
            if (spriteSize > 0)
                Maker.Custom.SPRITE_SIZE = spriteSize;
        }

        /// <summary>if the image is convertible to MV tileset.</summary>
        /// <param name="img">Image to be checked.</param>
        /// <returns>Return true if the image is convertible and false if not.</returns>
        protected override bool IsConvertible(Image img)
        {
            if (Maker.Custom.SPRITE_SIZE <= 0)
                throw new ConvertException(Vocab.GetText("sizeIsZeroErrorMsg"));

            if (Maker.Custom.SPRITE_SIZE >= img.Width || Maker.Custom.SPRITE_SIZE >= img.Height)
                throw new ConvertException(Vocab.GetText("sizeOutOfRangeErrorMsg"));

            return true;
        }
    }
}
