using System.Collections.Generic;
using System.Drawing;

namespace tilecon.Converter
{
    public class TilesetConverterVertical : TilesetConverterBase
    {
        public TilesetConverterVertical() { }

        public TilesetConverterVertical(Maker.Tileset inputMaker, SpriteMode mode, bool ignoreAlpha) : base(inputMaker, mode, ignoreAlpha) { }

        //To be called by TilesetConverterVX
        public List<Bitmap> GetSprites(Bitmap img)
        {
            return GetSprites(img as Image);
        }

        protected override List<Bitmap> GetSprites(Image img)
        {
            int spriteSize = Maker.GetSpriteSize(inputMaker);
            List<Bitmap> sprites = new List<Bitmap>();

            for (int y = 0, i = 0; y < img.Height; y += spriteSize)
                for (int x = 0; x < img.Width; x += spriteSize, i++)
                    sprites.Add(Crop(img as Bitmap, x, y, spriteSize, spriteSize));

            return RemoveAlphaImages(sprites);
        }

        protected override int GetCentralizeNumber()
        {
            return 8;
        }

        public override Bitmap[] ConvertToMV(Image img)
        {
            if (!IsConvertible(img)) return null;

            List<Bitmap> images = new List<Bitmap>();
            List<Bitmap> sprites = GetSprites(img);

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
