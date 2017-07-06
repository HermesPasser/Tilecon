using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace tilecon.Converter.Tests
{
    [TestClass()]
    public class TilesetConverterVerticalAphaTests : TilesetConverterTestBase
    {
        [TestInitialize]
        public void Initalize()
        {
            converter = new TilesetConverterVerticalApha(new Maker.Alpha(), SpriteMode.ALIGN_TOP_LEFT, false);
        }

        [TestMethod()]
        public void ConvertToMVTest()
        {
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.Alpha.Alpha_in.png"))[0];
            Bitmap AlphaOut = BitmapFromResourceStream("Tests.Images.Alpha.Alpha_out_success.png");
            Assert.IsTrue(ImageProcessing.IsEqual(converted, AlphaOut));
        }
    }
}