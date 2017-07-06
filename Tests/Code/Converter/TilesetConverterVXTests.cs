using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace tilecon.Converter.Tests
{
    [TestClass()]
    public class TilesetConverterVXTests : TilesetConverterTestBase
    {
        [TestMethod()]
        public void VX12ToMVTest()
        {
            converter = new TilesetConverterVX(new Maker.VX_Ace_A12(), SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.VX.VX_a12_in.png"))[0];
            Bitmap VXOut = BitmapFromResourceStream("Tests.Images.VX.VX_a12_out_success.png");
            Assert.IsTrue(ImageProcessing.IsEqual(converted, VXOut));
        }

        [TestMethod()]
        public void VX3ToMVTest()
        {
            converter = new TilesetConverterVX(new Maker.VX_Ace_A3(), SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.VX.VX_a3_in.png"))[0];
            Bitmap VXOut = BitmapFromResourceStream("Tests.Images.VX.VX_a3_out_success.png");
        }

        [TestMethod()]
        public void VX4ToMVTest()
        {
            converter = new TilesetConverterVX(new Maker.VX_Ace_A4(), SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.VX.VX_a4_in.png"))[0];
            Bitmap VXOut = BitmapFromResourceStream("Tests.Images.VX.VX_a4_out_success.png");
        }

        [TestMethod()]
        public void VX5ToMVTest()
        {
            converter = new TilesetConverterVX(new Maker.VX_Ace_A5(), SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.VX.VX_a5_in.png"))[0];
            Bitmap VXOut = BitmapFromResourceStream("Tests.Images.VX.VX_a5_out_success.png");
        }

        [TestMethod()]
        public void VXBEToMVTest()
        {
            converter = new TilesetConverterVX(new Maker.VX_Ace_BE(), SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.VX.VX_be_in.png"))[0];
            Bitmap VXOut = BitmapFromResourceStream("Tests.Images.VX.VX_be_out_success.png");
        }
    }
}