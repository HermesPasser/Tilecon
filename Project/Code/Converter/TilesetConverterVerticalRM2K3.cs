using System.Collections.Generic;
using System.Drawing;

namespace tilecon.Converter
{
    class TilesetConverterVerticalRM2K3 : TilesetConverterVertical
    {
        public TilesetConverterVerticalRM2K3(Maker.Tileset inputMaker, SpriteMode mode, bool ignoreAlpha) : base(inputMaker, mode, ignoreAlpha) { }

        private Bitmap PasteAutotileInMV(Bitmap bmp)
        {
            Bitmap[] sprites = SetMVAutotile(bmp);
            Bitmap img = new Bitmap(Maker.MV.A12.SIZE_WIDTH, Maker.MV.A12.SIZE_HEIGHT);
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
                Bitmap bmp1 = Stretch(Crop(autotiles[i], 16, 0, 16, 16), Maker.MV.SPRITE_SIZE);
                Bitmap bmp2 = Stretch(Crop(autotiles[i], 32, 0, 16, 16), Maker.MV.SPRITE_SIZE);
                Bitmap bmp3 = Stretch(Crop(autotiles[i], 0, 16, 48, 48), 96);
                temp = Paste(temp, bmp1, 0, 0, Maker.MV.SPRITE_SIZE, Maker.MV.SPRITE_SIZE);
                temp = Paste(temp, bmp2, 48, 0, Maker.MV.SPRITE_SIZE, Maker.MV.SPRITE_SIZE);
                temp = Paste(temp, bmp3, 0, 48, 96, 96);
                sprites.Add(temp);
            }
            return sprites.ToArray();
        }

        private Bitmap[] GetAutotiles(Bitmap bmp)
        {
            Bitmap[] bmps = new Bitmap[12];
            Bitmap temp = Crop(bmp, 0, 0, 192, Maker.R2000_2003.SIZE_HEIGHT);

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
            Bitmap temp = Crop(bmp, 192, 0, 288, Maker.R2000_2003.SIZE_HEIGHT);
            Bitmap bmp1 = Crop(temp, 0, 0, 96, Maker.R2000_2003.SIZE_HEIGHT / 2);
            Bitmap bmp2 = Crop(temp, 0, Maker.R2000_2003.SIZE_HEIGHT / 2, 96, Maker.R2000_2003.SIZE_HEIGHT / 2);
            Bitmap bmp3 = Crop(temp, 96, 0, 96, Maker.R2000_2003.SIZE_HEIGHT / 2);

            temp = new Bitmap(96, 384);
            temp = Paste(temp, bmp1, 0, 0, 192, 197);
            temp = Paste(temp, bmp2, 0, Maker.R2000_2003.SIZE_HEIGHT / 2, 191, 197);
            temp = Paste(temp, bmp3, 0, Maker.R2000_2003.SIZE_HEIGHT, 191, 197);
            return temp;
        }

        private Bitmap GetTilesetB(Bitmap bmp)
        {
            Bitmap temp = Crop(bmp, 288, 00, 192, Maker.R2000_2003.SIZE_HEIGHT);
            Bitmap bmp1 = Crop(temp, 00, Maker.R2000_2003.SIZE_HEIGHT / 2, 96, Maker.R2000_2003.SIZE_HEIGHT / 2);
            Bitmap bmp2 = Crop(temp, 96, 00, 96, Maker.R2000_2003.SIZE_HEIGHT / 2);
            Bitmap bmp3 = Crop(temp, 96, Maker.R2000_2003.SIZE_HEIGHT / 2, 96, Maker.R2000_2003.SIZE_HEIGHT / 2);

            temp = new Bitmap(96, 384);
            temp = Paste(temp, bmp1, 0, 0, 192, 197);
            temp = Paste(temp, bmp2, 0, Maker.R2000_2003.SIZE_HEIGHT / 2, 191, 197);
            temp = Paste(temp, bmp3, 0, Maker.R2000_2003.SIZE_HEIGHT, 191, 197);
            return temp;
        }

        protected override int GetCentralizeNumber()
        {
            return 16;
        }

        public override Bitmap[] ConvertToMV(Image img)
        {
            if (!IsConvertible(img)) return null;
            List<Bitmap> images = new List<Bitmap>();

            switch (inputMaker)
            {
                case Maker.Tileset.R2000_2003_Auto:
                    images.Add(PasteAutotileInMV(img as Bitmap));
                    return images.ToArray();
                case Maker.Tileset.R2000_2003_AB:
                    img = GetTilesetAB(img as Bitmap);
                    break;
                case Maker.Tileset.R2000_2003_A:
                    img = GetTilesetA(img as Bitmap);
                    break;
                case Maker.Tileset.R2000_2003_B:
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
