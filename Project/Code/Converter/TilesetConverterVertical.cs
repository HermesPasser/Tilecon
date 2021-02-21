using System.Collections.Generic;
using System.Drawing;
using static tilecon.ImageEditor;

namespace tilecon.Tileset.Converter
{
    /// <summary>Converter class for vertical tilesets.</summary>
    public class TilesetConverterVertical : TilesetConverterBase
    {
        /// <summary>The empty constructor of the class to allow it to be inherited.</summary>
        public TilesetConverterVertical() { }

        /// <summary>Default constructor.</summary>
        /// <param name="inputMaker">Tileset type to be converted</param>
        /// <param name="mode">Mode how sprites should be pasted into the converted image.</param>
        /// <param name="ignoreAlpha">Flag to ignore empty sprites.</param>
        public TilesetConverterVertical(ITileset inputMaker, SpriteMode mode, bool ignoreAlpha) : base(inputMaker, mode, ignoreAlpha) { }

        /// <summary>Call the protected GetSprites to be callsed by TilesetConverterVX</summary>
        /// <param name="img">Image to be slip.</param>
        /// <returns></returns>
        public List<Bitmap> GetSprites(Bitmap img)
        {
            return GetSprites(img as Image);
        }

        /// <summary>Split the image in various sprites.</summary>
        /// <param name="img">Image to be slip.</param>
        /// <returns>A list of sprites.</returns>
        protected override List<Bitmap> GetSprites(Image img)
        {
            int spriteSize = inputTileset.SpriteSize();
            List<Bitmap> sprites = new List<Bitmap>();

            // starts with 'y'
            for (int y = 0, i = 0; y < img.Height; y += spriteSize)
                for (int x = 0; x < img.Width; x += spriteSize, i++)
                    sprites.Add(Crop(img as Bitmap, x, y, spriteSize, spriteSize));
            return RemoveAlphaSprites(sprites);
        }

        /// <summary>Converter the image to MV tileset.</summary>
        /// <param name="img">Image to be converted</param>
        /// <returns>An array of bitmaps converteds to MV tileset.</returns>
        public override Bitmap[] ConvertToMV(Image img)
        {
            if (!IsConvertible(img)) 
                return new Bitmap[0];

            List<Bitmap> images = new List<Bitmap>();
            List<Bitmap> sprites = GetSprites(img);
          //  System.Windows.Forms.MessageBox.Show(inputTileset.SpriteSize() + "");

            int i = 0;

            while (i < sprites.Count)
            {
                Bitmap tempBmp = GetOutputBitmap(); //Draw image
                i = PasteEachSpriteHorizontal(tempBmp, sprites, 0, 0, tempBmp.Height, tempBmp.Width / 2, i);
                i = PasteEachSpriteHorizontal(tempBmp, sprites, 0, tempBmp.Width / 2, tempBmp.Height, tempBmp.Width, i);   
                images.Add(tempBmp); //Add image to the list
            }
            return images.ToArray();
        }
    }
}
