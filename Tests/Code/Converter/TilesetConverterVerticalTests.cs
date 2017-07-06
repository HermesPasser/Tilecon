using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace tilecon.Converter.Tests
{
    [TestClass()]
    public class TilesetConverterVerticalTests : TilesetConverterTestBase
    {
        [TestMethod()]
        public void R95ToMVTest()
        {
            converter = new TilesetConverterVertical(new Maker.R95(), SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.R95.95_in.bmp"))[0];
            Bitmap R95out = BitmapFromResourceStream("Tests.Images.R95.95_out_success.png");
            Assert.IsTrue(ImageProcessing.IsEqual(converted, R95out));
        }

        [TestMethod()]
        public void S97ToMVTest()
        {
            converter = new TilesetConverterVertical(new Maker.S97(), SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap[] converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.S97.97_in.bmp"));
            Bitmap S97out1 = BitmapFromResourceStream("Tests.Images.S97.97_out1_success.png");
            Bitmap S97out2 = BitmapFromResourceStream("Tests.Images.S97.97_out2_success.png");

            bool isTrue = ImageProcessing.IsEqual(converted[0], S97out1) && ImageProcessing.IsEqual(converted[1], S97out2);
            Assert.IsTrue(isTrue);
        }

        [TestMethod()]
        public void XPToMVTest()
        {
            converter = new TilesetConverterVertical(new Maker.XP_Tile(), SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.XP.XP_in.png"))[0];
            Bitmap XPOut = BitmapFromResourceStream("Tests.Images.XP.XP_out_success.png");
            Assert.IsTrue(ImageProcessing.IsEqual(converted, XPOut));
        }
    }
}