using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;

namespace tilecon.Conversor
{
    class Converter
    {
        private static Bitmap Crop(Bitmap src, int x, int y, int width, int height)
        {
            Rectangle rect = new Rectangle(x, y, width, height);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            using (Graphics gph = Graphics.FromImage(bmp))
            {
                gph.DrawImage(src, new Rectangle(0, 0, bmp.Width, bmp.Height), rect, GraphicsUnit.Pixel);
                gph.Dispose();
            }

            return bmp;
        }

        public static void SaveEachSubimage(Image img, string fileDir, int spriteSize)
        {
            int index = fileDir.LastIndexOf(".");
            fileDir = fileDir.Substring(0, index) + "_";

            for (int i = 0, y = 0; y < img.Height; y += spriteSize)
            {
                for (int x = 0; x < img.Width; x += spriteSize)
                {
                    int ipp = i + 1;
                    string s = ipp > 99 ? s = ipp.ToString() : ipp > 9 ? s = ipp.ToString() : s = "0" + ipp;
                    Bitmap b = Converter.Crop(img as Bitmap, x, y, spriteSize, spriteSize);
                    b.Save(fileDir + s + ".png");
                    i++;
                }
            }
        }

        private static Bitmap[] GetSprites(Image img, int spriteSize)
        {
            int cropSize = ((img.Height / spriteSize) * (img.Width / spriteSize));
            Bitmap[] sprites = new Bitmap[cropSize];

            for (int y = 0, i = 0; y < img.Height; y += spriteSize)
            {
                for (int x = 0; x < img.Width; x += spriteSize)
                {
                    sprites[i] = Converter.Crop(img as Bitmap, x, y, spriteSize, spriteSize);
                    i++;
                }
            }
            return sprites;
        }

        //Colocar opção, ignorar sprite se for transparente.
        //Colocar opção para redimencionar a imagem
        //Colocar opção para centralizar imagem se ela não for redimensinada.
        //Imprimir em uma metade da imagem e depois na outra ou colocando de oito em oito como no xp
        public static Bitmap[] ConvertToMV(Image img)
        {
            List<Bitmap> images = new List<Bitmap>();
            List<Bitmap> sprites = GetSprites(img, Maker.XP.SPRITE_SIZE).ToList<Bitmap>();
            Rectangle rect = new Rectangle(0, 0, sprites[0].Width, sprites[0].Height);
            Graphics graphics;
            int iSprite = 0;

            //For each image
            while (iSprite < sprites.Count) {
                
                //Draw image
                Bitmap tempBmp = new Bitmap(Maker.MV.BE.SIZE, Maker.MV.BE.SIZE);
                tempBmp.SetPixel(0, 0, Color.White);
                graphics = Graphics.FromImage(tempBmp);

                //Draw in first part of bitmap
                for (int y = 0; y < tempBmp.Height; y += Maker.MV.SPRITE_SIZE)
                {
                    for (int x = 0; x < tempBmp.Width / 2; x += Maker.MV.SPRITE_SIZE)
                    {
                        if (iSprite >= sprites.Count || sprites[iSprite] == null)
                            break;

                        graphics.DrawImage(sprites[iSprite], x, y, rect, GraphicsUnit.Pixel);
                        iSprite++;
                    }
                }

                //Draw in second part of bitmap
                for (int y = 0; y < tempBmp.Height; y += Maker.MV.SPRITE_SIZE)
                {
                    for (int x = tempBmp.Width / 2; x < tempBmp.Width; x += Maker.MV.SPRITE_SIZE)
                    {
                        if (iSprite >= sprites.Count || sprites[iSprite] == null)
                            break;

                        graphics.DrawImage(sprites[iSprite], x, y, rect, GraphicsUnit.Pixel);
                        iSprite++;
                    }
                }

                //Add image in the list
                images.Add(tempBmp);
            }
            
            return images.ToArray();
        }
    }
}
