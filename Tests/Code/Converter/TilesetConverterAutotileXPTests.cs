using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace tilecon.Converter.Tests
{
    [TestClass()]
    public class TilesetConverterAutotileXPTests : TilesetConverterTestBase
    {
        [TestInitialize]
        public void Initalize()
        {
            converter = new TilesetConverterAutotileXP(new Maker.XP_Auto(), SpriteMode.ALIGN_TOP_LEFT, false);
        }

        [TestMethod()]
        public void ConvertToMVTest()
        {
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.XP.XPAuto_in.png"))[0];
            Bitmap XPOut = BitmapFromResourceStream("Tests.Images.XP.XPAuto_out_success.png");
            Assert.IsTrue(ImageProcessing.IsEqual(converted, XPOut));
        }

        [TestMethod()]
        public void ConvertAnimatedToMVTest()
        {
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.XP.XPAutoAnim_in.png"))[0];
            Bitmap XPOut = BitmapFromResourceStream("Tests.Images.XP.XPAutoAnim_out_success.png");
            Assert.IsTrue(ImageProcessing.IsEqual(converted, XPOut));
        }
    }
}