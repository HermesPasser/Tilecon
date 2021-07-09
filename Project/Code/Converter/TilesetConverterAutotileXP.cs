using System.Drawing;
using static tilecon.ImageEditor;

namespace tilecon.Tileset.Converter
{
    /// <summary>Autotile xp converter class.</summary>
    public class TilesetConverterAutotileXP : TilesetConverterVertical
    {
        /// <summary>Default constructor.</summary>
        /// <param name="inputTilesetImpl">Tileset type to be converted</param>
        /// <param name="mode">Mode how sprites should be pasted into the converted image.</param>
        /// <param name="ignoreAlpha">Flag to ignore empty sprites.</param>
        public TilesetConverterAutotileXP(ITileset inputTilesetImpl, SpriteMode mode, bool ignoreAlpha) : base(inputTilesetImpl, mode, ignoreAlpha) { }

        /// <summary>if the image is convertible to MV tileset.</summary>
        /// <param name="img">Image to be checked.</param>
        /// <returns>Return true if the image is convertible and false if not.</returns>
        protected override bool IsConvertible(Image img)
        {
            if (img.Width == Tileset.XP_Auto.Width || img.Height == Tileset.XP_Auto.Height)
                return true;
            else if (img.Width == Tileset.XP_AnimatedAuto.Width || img.Height == Tileset.XP_AnimatedAuto.Height)
                return true;
            return false;
        }

        private Bitmap GetMVAutotile(Bitmap bmp)
        {
            Bitmap temp = new Bitmap(Tileset.MV_A12.Size, Tileset.MV_A12.Size);
            Bitmap mv = new Bitmap(96, 144);
            Bitmap xp1 = Crop(bmp, 00, 00, Tileset.XP_Tile.Size, Tileset.XP_Tile.Size);
            Bitmap xp2 = Crop(bmp, 64, 00, Tileset.XP_Tile.Size / 2, Tileset.XP_Tile.Size / 2);
            Bitmap xp3 = Crop(bmp, 80, 00, Tileset.XP_Tile.Size / 2, Tileset.XP_Tile.Size / 2);
            Bitmap xp4 = Crop(bmp, 64, 16, Tileset.XP_Tile.Size / 2, Tileset.XP_Tile.Size / 2);
            Bitmap xp5 = Crop(bmp, 80, 16, Tileset.XP_Tile.Size / 2, Tileset.XP_Tile.Size / 2);
            Bitmap xp6 = Crop(bmp, 0, 32, 96, 96);
            Bitmap xp7 = Crop(bmp, 26, 56, Tileset.MV_A12.Size, Tileset.MV_A12.Size);

            mv = Paste(mv, xp1, 8, 8, Tileset.XP_Tile.Size, Tileset.XP_Tile.Size);
            temp = Paste(temp, xp2, 00, 00, Tileset.XP_Tile.Size / 2, Tileset.XP_Tile.Size / 2);
            temp = Paste(temp, xp3, 32, 00, Tileset.XP_Tile.Size / 2, Tileset.XP_Tile.Size / 2);
            temp = Paste(temp, xp4, 00, 32, Tileset.XP_Tile.Size / 2, Tileset.XP_Tile.Size / 2);
            temp = Paste(temp, xp5, 32, 32, Tileset.XP_Tile.Size / 2, Tileset.XP_Tile.Size / 2);
            temp = PasteInAlpha(temp, xp7);
            mv = Paste(mv, temp, 48, 00, 96, 96);
            mv = Paste(mv, xp6, 00, 48, 96, 96);
            return mv;
        }
        
        private Bitmap GetMVTileset(Bitmap bmp)
        {
            Bitmap mv = new Bitmap(Tileset.MV_A12.Width, Tileset.MV_A12.Height);

            if (bmp.Width == Tileset.XP_Auto.Width)
            {
                Bitmap b = GetMVAutotile(bmp);
                mv = Paste(mv, b, 288, 0, 96, 144);
            }
            else
            {
                Bitmap b1 = Crop(bmp, 00, 00, 96, 128);
                Bitmap b2 = Crop(bmp, 96, 00, 96, 128);
                Bitmap b3 = Crop(bmp, 192, 00, 96, 128);

                mv = Paste(mv, GetMVAutotile(b1), 0, 0, 96, 144);
                mv = Paste(mv, GetMVAutotile(b2), 96, 0, 96, 144);
                mv = Paste(mv, GetMVAutotile(b3), 192, 0, 96, 144);
            }
            return mv;
        }

        /// <summary>Get the number of pixels to be moved to center the sprite on the tileset.</summary>
        /// <returns>The number of pixels to be moved to center the sprite on the tileset.</returns>
        protected override int GetOffset()
        {
            return 0;
        }

        /// <summary>Converter the image to MV tileset.</summary>
        /// <param name="img">Image to be converted</param>
        /// <returns>An array of bitmaps converteds to MV tileset.</returns>
        public override Bitmap[] ConvertToMV(Image img)
        {
            if (!IsConvertible(img)) 
                return new Bitmap[0];
            
            return new Bitmap[1] { GetMVTileset(img as Bitmap) };
        }
    }
}
