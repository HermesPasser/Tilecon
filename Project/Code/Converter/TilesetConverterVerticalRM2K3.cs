using System.Collections.Generic;
using System.Drawing;

namespace tilecon.Tileset.Converter
{
    /// <summary>Converter class for 2000 and 20003 tilesets.</summary>
    public class TilesetConverterVerticalRM2K3 : TilesetConverterVertical
    {
        /// <summary>Default constructor.</summary>
        /// <param name="inputMaker">Tileset type to be converted</param>
        /// <param name="mode">Mode how sprites should be pasted into the converted image.</param>
        /// <param name="ignoreAlpha">Flag to ignore empty sprites.</param>
        public TilesetConverterVerticalRM2K3(ITileset inputMaker, SpriteMode mode, bool ignoreAlpha) : base(inputMaker, mode, ignoreAlpha) { }

        private Bitmap PasteAnimatedObjectInMV(Bitmap bmp)
        {
            Bitmap[] bmps = GetAnimatedObj(bmp);
            Bitmap fbmp = new Bitmap(48, 64);

            fbmp = Paste(fbmp, bmps[0], 0, 0, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            fbmp = Paste(fbmp, bmps[1], 16, 0, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            fbmp = Paste(fbmp, bmps[2], 32, 0, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);

            fbmp = Paste(fbmp, bmps[4], 0, 16, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            fbmp = Paste(fbmp, bmps[5], 16, 16, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            fbmp = Paste(fbmp, bmps[6], 32, 16, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);

            fbmp = Paste(fbmp, bmps[8], 0, 32, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            fbmp = Paste(fbmp, bmps[9], 16, 32, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            fbmp = Paste(fbmp, bmps[10], 32, 32, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);

            fbmp = Paste(fbmp, bmps[3], 0, 48, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            fbmp = Paste(fbmp, bmps[7], 16, 48, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            fbmp = Paste(fbmp, bmps[11], 32, 48, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);

            if (mode == SpriteMode.RESIZE) fbmp = Stretch(fbmp, 144, 192);

            return fbmp;
        }
        
        private Bitmap[] GetAnimatedObj(Bitmap bmp)
        {
            Bitmap[] bmps = new Bitmap[12];

            bmps[0] = Crop(bmp, 48, 64, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            bmps[1] = Crop(bmp, 48, 80, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            bmps[2] = Crop(bmp, 48, 96, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            bmps[3] = Crop(bmp, 48, 112, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);

            bmps[4] = Crop(bmp, 64, 64, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            bmps[5] = Crop(bmp, 64, 80, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            bmps[6] = Crop(bmp, 64, 96, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            bmps[7] = Crop(bmp, 64, 112, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);

            bmps[8] = Crop(bmp, 80, 64, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            bmps[9] = Crop(bmp, 80, 80, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            bmps[10] = Crop(bmp, 80, 96, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);
            bmps[11] = Crop(bmp, 80, 112, Maker.R2k_2k3.SPRITE_SIZE, Maker.R2k_2k3.SPRITE_SIZE);

            return bmps;
        }

        private Bitmap PasteAutotileInMV(Bitmap bmp)
        {
            Bitmap[] sprites = SetMVAutotile(bmp);
            Bitmap img = new Bitmap(Maker.MV_A12.SIZE_WIDTH, Maker.MV_A12.SIZE_HEIGHT);
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
                Bitmap bmp1 = Stretch(Crop(autotiles[i], 16, 0, 16, 16), Maker.MV_A12.SPRITE_SIZE);
                Bitmap bmp2 = Stretch(Crop(autotiles[i], 32, 0, 16, 16), Maker.MV_A12.SPRITE_SIZE);
                Bitmap bmp3 = Stretch(Crop(autotiles[i], 0, 16, 48, 48), 96);
                temp = Paste(temp, bmp1, 0, 0, Maker.MV_A12.SPRITE_SIZE, Maker.MV_A12.SPRITE_SIZE);
                temp = Paste(temp, bmp2, 48, 0, Maker.MV_A12.SPRITE_SIZE, Maker.MV_A12.SPRITE_SIZE);
                temp = Paste(temp, bmp3, 0, 48, 96, 96);
                sprites.Add(temp);
            }
            return sprites.ToArray();
        }

        private Bitmap[] GetAutotiles(Bitmap bmp)
        {
            Bitmap[] bmps = new Bitmap[12];
            Bitmap temp = Crop(bmp, 0, 0, 192, Maker.R2k_2k3_AB.SIZE_HEIGHT);

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
            Bitmap temp = Crop(bmp, 192, 0, 288, Maker.R2k_2k3_AB.SIZE_HEIGHT);
            Bitmap bmp1 = Crop(temp, 0, 0, 96, Maker.R2k_2k3_AB.SIZE_HEIGHT / 2);
            Bitmap bmp2 = Crop(temp, 0, Maker.R2k_2k3_AB.SIZE_HEIGHT / 2, 96, Maker.R2k_2k3_AB.SIZE_HEIGHT / 2);
            Bitmap bmp3 = Crop(temp, 96, 0, 96, Maker.R2k_2k3_AB.SIZE_HEIGHT / 2);

            temp = new Bitmap(96, 384);
            temp = Paste(temp, bmp1, 0, 0, 192, 197);
            temp = Paste(temp, bmp2, 0, Maker.R2k_2k3_AB.SIZE_HEIGHT / 2, 191, 197);
            temp = Paste(temp, bmp3, 0, Maker.R2k_2k3_AB.SIZE_HEIGHT, 191, 197);
            return temp;
        }

        private Bitmap GetTilesetB(Bitmap bmp)
        {
            Bitmap temp = Crop(bmp, 288, 00, 192, Maker.R2k_2k3_AB.SIZE_HEIGHT);
            Bitmap bmp1 = Crop(temp, 00, Maker.R2k_2k3_AB.SIZE_HEIGHT / 2, 96, Maker.R2k_2k3_AB.SIZE_HEIGHT / 2);
            Bitmap bmp2 = Crop(temp, 96, 00, 96, Maker.R2k_2k3_AB.SIZE_HEIGHT / 2);
            Bitmap bmp3 = Crop(temp, 96, Maker.R2k_2k3_AB.SIZE_HEIGHT / 2, 96, Maker.R2k_2k3_AB.SIZE_HEIGHT / 2);

            temp = new Bitmap(96, 384);
            temp = Paste(temp, bmp1, 0, 0, 192, 197);
            temp = Paste(temp, bmp2, 0, Maker.R2k_2k3_AB.SIZE_HEIGHT / 2, 191, 197);
            temp = Paste(temp, bmp3, 0, Maker.R2k_2k3_AB.SIZE_HEIGHT, 191, 197);
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

            switch (inputTileset.TilesetName())
            {
                case Maker.R2k_2k3_Auto.NAME:
                    images.Add(PasteAutotileInMV(img as Bitmap));
                    return images.ToArray();
                case Maker.R2k_2k3_AnimObj.NAME:
                    images.Add(PasteAnimatedObjectInMV(img as Bitmap));
                    return images.ToArray();
                case Maker.R2k_2k3_AB.NAME:
                    img = GetTilesetAB(img as Bitmap);
                    break;
                case Maker.R2k_2k3_A.NAME:
                    img = GetTilesetA(img as Bitmap);
                    break;
                case Maker.R2k_2k3_B.NAME:
                    img = GetTilesetB(img as Bitmap);
                    break;
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
