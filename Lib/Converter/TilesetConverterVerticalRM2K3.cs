using System.Drawing;
using static tilecon.Core.ImageEditor;

namespace tilecon.Core.Converter
{
    /// <summary>Converter class for 2000 and 20003 tilesets.</summary>
    public class TilesetConverterVerticalRM2K3 : TilesetConverterVertical
    {
        /// <summary>Default constructor.</summary>
        /// <param name="inputTilesetImpl">Tileset type to be converted</param>
        /// <param name="mode">Mode how sprites should be pasted into the converted image.</param>
        /// <param name="ignoreAlpha">Flag to ignore empty sprites.</param>
        public TilesetConverterVerticalRM2K3(ITileset inputTilesetImpl, SpriteMode mode, bool ignoreAlpha) : base(inputTilesetImpl, mode, ignoreAlpha) { }

        private Bitmap PasteAnimatedObjectInMV(Bitmap bmp)
        {
            Bitmap[] bmps = GetAnimatedObj(bmp);
            Bitmap fbmp = new Bitmap(48, 64);
            short r2kSize = Tileset.R2k_2k3_AB.Size;

            fbmp = Paste(fbmp, bmps[0], 0, 0, r2kSize, r2kSize);
            fbmp = Paste(fbmp, bmps[1], 16, 0, r2kSize, r2kSize);
            fbmp = Paste(fbmp, bmps[2], 32, 0, r2kSize, r2kSize);

            fbmp = Paste(fbmp, bmps[4], 0, 16, r2kSize, r2kSize);
            fbmp = Paste(fbmp, bmps[5], 16, 16, r2kSize, r2kSize);
            fbmp = Paste(fbmp, bmps[6], 32, 16, r2kSize, r2kSize);

            fbmp = Paste(fbmp, bmps[8], 0, 32, r2kSize, r2kSize);
            fbmp = Paste(fbmp, bmps[9], 16, 32, r2kSize, r2kSize);
            fbmp = Paste(fbmp, bmps[10], 32, 32, r2kSize, r2kSize);

            fbmp = Paste(fbmp, bmps[3], 0, 48, r2kSize, r2kSize);
            fbmp = Paste(fbmp, bmps[7], 16, 48, r2kSize, r2kSize);
            fbmp = Paste(fbmp, bmps[11], 32, 48, r2kSize, r2kSize);

            if (mode == SpriteMode.RESIZE) fbmp = Stretch(fbmp, 144, 192);

            return fbmp;
        }
        
        private Bitmap[] GetAnimatedObj(Bitmap bmp)
        {
            Bitmap[] bmps = new Bitmap[12];
            short r2kSize = Tileset.R2k_2k3_AB.Size;

            bmps[0] = Crop(bmp, 48, 64, r2kSize, r2kSize);
            bmps[1] = Crop(bmp, 48, 80, r2kSize, r2kSize);
            bmps[2] = Crop(bmp, 48, 96, r2kSize, r2kSize);
            bmps[3] = Crop(bmp, 48, 112, r2kSize, r2kSize);

            bmps[4] = Crop(bmp, 64, 64, r2kSize, r2kSize);
            bmps[5] = Crop(bmp, 64, 80, r2kSize, r2kSize);
            bmps[6] = Crop(bmp, 64, 96, r2kSize, r2kSize);
            bmps[7] = Crop(bmp, 64, 112, r2kSize, r2kSize);

            bmps[8] = Crop(bmp, 80, 64, r2kSize, r2kSize);
            bmps[9] = Crop(bmp, 80, 80, r2kSize, r2kSize);
            bmps[10] = Crop(bmp, 80, 96, r2kSize, r2kSize);
            bmps[11] = Crop(bmp, 80, 112, r2kSize, r2kSize);

            return bmps;
        }

        private Bitmap PasteAutotileInMV(Bitmap bmp)
        {
            Bitmap[] sprites = SetMVAutotile(bmp);
            Bitmap img = new Bitmap(Tileset.MV_A12.Width, Tileset.MV_A12.Height);
            Rectangle rect = new Rectangle(0, 0, sprites[0].Width, sprites[0].Height);
            Graphics graphics = Graphics.FromImage(img);

            for (int x = 0, i = 0; x < img.Height; x += 96)
            {
                for (int y = 0; y < img.Height; y += 144)
                {
                    if (i >= sprites.Length || sprites[i] == null)
                        break;

                    graphics.DrawImage(sprites[i], x, y, rect, GraphicsUnit.Pixel);
                    i++;
                }
            }
            graphics.Dispose();
            return img;
        }
         
        private Bitmap[] SetMVAutotile(Bitmap bmp)
        {    
            Bitmap[] autotiles = GetAutotiles(bmp);
            List<Bitmap> sprites = new List<Bitmap>();

            for (int i = 0; i < autotiles.Length; i++)
            {
                Bitmap temp = new Bitmap(96, 144);
                Bitmap singleTileBmp = Stretch(Crop(autotiles[i], 0, 0, 16, 16), Tileset.MV_A12.Size);
                Bitmap cornerTileBpm = Stretch(Crop(autotiles[i], 32, 0, 16, 16), Tileset.MV_A12.Size);
                Bitmap edgesAndFillerTilesBmp = Stretch(Crop(autotiles[i], 0, 16, 48, 48), 96);
                temp = Paste(temp, singleTileBmp, 0, 0, Tileset.MV_A12.Size, Tileset.MV_A12.Size);
                temp = Paste(temp, cornerTileBpm, 48, 0, Tileset.MV_A12.Size, Tileset.MV_A12.Size);
                temp = Paste(temp, edgesAndFillerTilesBmp, 0, 48, 96, 96);
                sprites.Add(temp);
            }
            return sprites.ToArray();
        }

        /// <summary>
        /// Takes a whole RPG Maker 2000 or RPG Maker 2003 Chipset and extracts all the 12 autotile Areas into an Bitmap Array.
        /// </summary>
        /// <param name="bmp">The RPG Maker 2000 or 2003 Chipset Bitmap</param>
        /// <returns>An array with all 12 autotile area bitmaps</returns>
        private Bitmap[] GetAutotiles(Bitmap bmp)
        {
            Bitmap[] bmps = new Bitmap[12];
            Bitmap temp = Crop(bmp, 0, 0, 192, Tileset.R2k_2k3_AB.Height);

            bmps[0] = Crop(temp, 0, 128, 48, 64);
            bmps[1] = Crop(temp, 48, 128, 48, 64);
            bmps[2] = Crop(temp, 0, 192, 48, 64);
            bmps[3] = Crop(temp, 48, 192, 48, 64);
            bmps[4] = Crop(temp, 96, 0, 48, 64);
            bmps[5] = Crop(temp, 144, 0, 48, 64);
            bmps[6] = Crop(temp, 96, 64, 48, 64);
            bmps[7] = Crop(temp, 144, 64, 48, 64);
            bmps[8] = Crop(temp, 96, 128, 48, 64);
            bmps[9] = Crop(temp, 144, 128, 48, 64);
            bmps[10] = Crop(temp, 96, 192, 48, 64);
            bmps[11] = Crop(temp, 144, 192, 48, 64);
            return bmps;
        }

        private Bitmap GetTilesetAB(Bitmap bmp)
        {
            Bitmap temp = new Bitmap(96, 768);
            Bitmap bmp1 = GetTilesetA(bmp);
            Bitmap bmp2 = GetTilesetB(bmp);

            temp = Paste(temp, bmp1, 0, 0, bmp1.Width, bmp1.Height);
            temp = Paste(temp, bmp2, 0, bmp1.Height, bmp1.Width, bmp1.Height);
            return temp;
        }

        private Bitmap GetTilesetA(Bitmap bmp)
        {
            Bitmap temp = Crop(bmp, 192, 0, 288, Tileset.R2k_2k3_AB.Height);
            Bitmap bmp1 = Crop(temp, 0, 0, 96, Tileset.R2k_2k3_AB.Height / 2);
            Bitmap bmp2 = Crop(temp, 0, Tileset.R2k_2k3_AB.Height / 2, 96, Tileset.R2k_2k3_AB.Height / 2);
            Bitmap bmp3 = Crop(temp, 96, 0, 96, Tileset.R2k_2k3_AB.Height / 2);

            temp = new Bitmap(96, 384);
            temp = Paste(temp, bmp1, 0, 0, 192, 197);
            temp = Paste(temp, bmp2, 0, Tileset.R2k_2k3_AB.Height / 2, 191, 197);
            temp = Paste(temp, bmp3, 0, Tileset.R2k_2k3_AB.Height, 191, 197);
            return temp;
        }

        private Bitmap GetTilesetB(Bitmap bmp)
        {
            Bitmap temp = Crop(bmp, 288, 00, 192, Tileset.R2k_2k3_AB.Height);
            Bitmap bmp1 = Crop(temp, 00, Tileset.R2k_2k3_AB.Height / 2, 96, Tileset.R2k_2k3_AB.Height / 2);
            Bitmap bmp2 = Crop(temp, 96, 00, 96, Tileset.R2k_2k3_AB.Height / 2);
            Bitmap bmp3 = Crop(temp, 96, Tileset.R2k_2k3_AB.Height / 2, 96, Tileset.R2k_2k3_AB.Height / 2);

            temp = new Bitmap(96, 384);
            temp = Paste(temp, bmp1, 0, 0, 192, 197);
            temp = Paste(temp, bmp2, 0, Tileset.R2k_2k3_AB.Height / 2, 191, 197);
            temp = Paste(temp, bmp3, 0, Tileset.R2k_2k3_AB.Height, 191, 197);
            return temp;
        }

        /// <summary>Converter the image to MV tileset.</summary>
        /// <param name="img">Image to be converted</param>
        /// <returns>An array of bitmaps converteds to MV tileset.</returns>
        public override Bitmap[] ConvertToMV(Image img)
        {
            if (!IsConvertible(img)) 
                return new Bitmap[0];

            List<Bitmap> images = new List<Bitmap>();

            string name = inputTileset.TilesetName();
            if (name == Tileset.R2k_2k3_Auto.Name)
            {
                images.Add(PasteAutotileInMV(img as Bitmap));
                return images.ToArray();
            }
            else if (name == Tileset.R2k_2k3_AnimObj.Name)
            {
                images.Add(PasteAnimatedObjectInMV(img as Bitmap));
                return images.ToArray();
            }
            else if (name == Tileset.R2k_2k3_AB.Name)
            {
                img = GetTilesetAB(img as Bitmap);
            }
            else if (name == Tileset.R2k_2k3_A.Name)
            {
                img = GetTilesetA(img as Bitmap);
            }
            else if (name == Tileset.R2k_2k3_B.Name)
            {
                img = GetTilesetB(img as Bitmap);
            }

            List<Bitmap> sprites = GetSprites(img);
            int i = 0;

            while (i < sprites.Count)
            {
                Bitmap tempBmp = GetOutputBitmap();
                i = PasteEachSpriteHorizontal(tempBmp, sprites, 0, 0, tempBmp.Height, tempBmp.Width / 3, i);
                i = PasteEachSpriteHorizontal(tempBmp, sprites, 0, tempBmp.Width / 2, tempBmp.Height, tempBmp.Width - 100, i); 
                images.Add(tempBmp);
            }
            return images.ToArray();
        }
    }
}
