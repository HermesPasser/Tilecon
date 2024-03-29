﻿using System.Drawing;
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
        public TilesetConverterCustom(ITileset tileset, SpriteMode mode, bool ignoreAlpha) : base(tileset, mode, ignoreAlpha)
        { }

        /// <summary>if the image is convertible to MV tileset.</summary>
        /// <param name="img">Image to be checked.</param>
        /// <returns>Return true if the image is convertible and false if not.</returns>
        protected override bool IsConvertible(Image img)
        {
            if (inputTileset.TileSize() <= 0)
                throw new ConvertException(Vocab.GetText("sizeIsZeroErrorMsg"));
            
            if (inputTileset.TileSize() >= img.Width || inputTileset.TileSize() >= img.Height)
                throw new ConvertException(Vocab.GetText("sizeOutOfRangeErrorMsg"));

            return true;
        }
    }
}
