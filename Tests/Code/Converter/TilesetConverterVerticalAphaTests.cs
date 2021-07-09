using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using tilecon.Tileset.Tests;

namespace tilecon.Tileset.Converter.Tests
{
    [TestClass()]
    public class TilesetConverterVerticalAphaTests : TilesetTestBase
    {
        [TestInitialize]
        public void Initalize()
        {
            converter = new TilesetConverterVerticalApha(Tileset.Alpha, SpriteMode.ALIGN_TOP_LEFT, false);
        }

        [TestMethod()]
        public void Convert_AlphaToMVTest()
        {
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.Alpha.Alpha_in.png"))[0];
            Bitmap AlphaOut = BitmapFromResourceStream("Tests.Images.Alpha.Converter.Alpha_out_success.png");
            Assert.IsTrue(ImageEditor.IsEqual(converted, AlphaOut));
        }
    }
}