using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using tilecon.Tileset.Tests;
using tilecon.Core.Converter;
using tilecon.Core;

namespace tilecon.Tileset.Converter.Tests
{
    [TestClass()]
    public class TilesetConverterVXTests : TilesetTestBase
    {
        [TestMethod()]
        public void Convert_VX12ToMVTest()
        {
            converter = new TilesetConverterVX(Core.Tileset.VX_Ace_A12, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.VX.VX_a12_in.png"))[0];
            Bitmap VXOut = BitmapFromResourceStream("Tests.Images.VX.Converter.VX_a12_out_success.png");
            Assert.IsTrue(ImageEditor.IsEqual(converted, VXOut));
        }

        [TestMethod()]
        public void Convert_VX3ToMVTest()
        {
            converter = new TilesetConverterVX(Core.Tileset.VX_Ace_A3, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.VX.VX_a3_in.png"))[0];
            Bitmap VXOut = BitmapFromResourceStream("Tests.Images.VX.Converter.VX_a3_out_success.png");
        }

        [TestMethod()]
        public void Convert_VX4ToMVTest()
        {
            converter = new TilesetConverterVX(Core.Tileset.VX_Ace_A4, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.VX.VX_a4_in.png"))[0];
            Bitmap VXOut = BitmapFromResourceStream("Tests.Images.VX.Converter.VX_a4_out_success.png");
        }

        [TestMethod()]
        public void Convert_VX5ToMVTest()
        {
            converter = new TilesetConverterVX(Core.Tileset.VX_Ace_A5, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.VX.VX_a5_in.png"))[0];
            Bitmap VXOut = BitmapFromResourceStream("Tests.Images.VX.Converter.VX_a5_out_success.png");
        }

        [TestMethod()]
        public void Convert_VXBEToMVTest()
        {
            converter = new TilesetConverterVX(Core.Tileset.VX_Ace_BE, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.VX.VX_be_in.png"))[0];
            Bitmap VXOut = BitmapFromResourceStream("Tests.Images.VX.Converter.VX_be_out_success.png");
        }
    }
}