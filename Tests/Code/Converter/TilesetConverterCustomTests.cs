using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using tilecon.Tileset.Tests;

namespace tilecon.Tileset.Converter.Tests
{
    [TestClass()]
    public class TilesetConverterCustomTests : TilesetTestBase
    {
         [TestMethod()]
        public void Convert_CustomToMVTest()
        {
            converter = new TilesetConverterCustom(Tileset.Custom(22), SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.Custom.Custom22px_in.png"))[0];
            Bitmap AlphaOut = BitmapFromResourceStream("Tests.Images.Custom.Converter.Custom22px_out_success.png");
            Assert.IsTrue(ImageEditor.IsEqual(converted, AlphaOut));
        }
    }
}