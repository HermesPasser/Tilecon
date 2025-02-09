using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using tilecon.Tileset.Tests;
using tilecon.Core.Converter;
using tilecon.Core;

namespace tilecon.Tileset.Converter.Tests
{
    [TestClass()]
    public class TilesetConverterAutotileXPTests : TilesetTestBase
    {
        [TestInitialize]
        public void Initalize()
        {
            converter = new TilesetConverterAutotileXP(Core.Tileset.XP_Auto, SpriteMode.ALIGN_TOP_LEFT, false);
        }

        [TestMethod()]
        public void Convert_XPAutotileToMVTest()
        {
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.XP.XPAuto_in.png"))[0];
            Bitmap XPOut = BitmapFromResourceStream("Tests.Images.XP.Converter.XPAuto_out_success.png");
            Assert.IsTrue(ImageEditor.IsEqual(converted, XPOut));
        }

        [TestMethod()]
        public void Convert_XPAnimatedToMVTest()
        {
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.XP.XPAutoAnim_in.png"))[0];
            Bitmap XPOut = BitmapFromResourceStream("Tests.Images.XP.Converter.XPAutoAnim_out_success.png");
            Assert.IsTrue(ImageEditor.IsEqual(converted, XPOut));
        }
    }
}