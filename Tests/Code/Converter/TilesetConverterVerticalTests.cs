using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using tilecon.Tileset.Tests;
using tilecon.Core.Converter;
using tilecon.Core;

namespace tilecon.Tileset.Converter.Tests
{
    [TestClass()]
    public class TilesetConverterVerticalTests : TilesetTestBase
    {
        [TestMethod()]
        public void Convert_R95ToMVTest()
        {
            converter = new TilesetConverterVertical(Core.Tileset.R95, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.R95.95_in.bmp"))[0];
            Bitmap R95out = BitmapFromResourceStream("Tests.Images.R95.Converter.95_out_success.png");
            Assert.IsTrue(ImageEditor.IsEqual(converted, R95out));
        }

        [TestMethod()]
        public void Convert_S97ToMVTest()
        {
            converter = new TilesetConverterVertical(Core.Tileset.S97, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap[] converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.S97.97_in.bmp"));
            Bitmap S97out1 = BitmapFromResourceStream("Tests.Images.S97.Converter.97_out1_success.png");
            Bitmap S97out2 = BitmapFromResourceStream("Tests.Images.S97.Converter.97_out2_success.png");

            bool isTrue = ImageEditor.IsEqual(converted[0], S97out1) && ImageEditor.IsEqual(converted[1], S97out2);
            Assert.IsTrue(isTrue);
        }

        [TestMethod()]
        public void Convert_XPToMVTest()
        {
            converter = new TilesetConverterVertical(Core.Tileset.XP_Tile, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.XP.XP_in.png"))[0];
            Bitmap XPOut = BitmapFromResourceStream("Tests.Images.XP.Converter.XP_out_success.png");
            Assert.IsTrue(ImageEditor.IsEqual(converted, XPOut));
        }
    }
}