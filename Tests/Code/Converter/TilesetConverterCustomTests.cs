using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using tilecon.Tileset.Tests;

namespace tilecon.Tileset.Converter.Tests
{
    [TestClass()]
    public class TilesetConverterCustomTests : TilesetTestBase
    {
        [TestInitialize]
        public void Initalize()
        {
            converter = new TilesetConverterCustom(SpriteMode.ALIGN_TOP_LEFT, false, 22);
        }

        [TestMethod()]
        public void Convert_CustomToMVTest()
        {
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.Custom.Custom22px_in.png"))[0];
            Bitmap AlphaOut = BitmapFromResourceStream("Tests.Images.Custom.Converter.Custom22px_out_success.png");
            Assert.IsTrue(ImageEditor.IsEqual(converted, AlphaOut));
        }
    }
}