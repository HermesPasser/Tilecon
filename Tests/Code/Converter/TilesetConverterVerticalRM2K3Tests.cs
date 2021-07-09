using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using tilecon.Tileset.Tests;

namespace tilecon.Tileset.Converter.Tests
{
    [TestClass()]
    public class TilesetConverterVerticalRM2K3Tests : TilesetTestBase
    {
        [TestMethod()]
        public void Convert_RM2K3AutoToMVTest()
        {
            converter = new TilesetConverterVerticalRM2K3(Tileset.R2k_2k3_Auto, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.R2k.R2k3_in.png"))[0];
            Bitmap R2kOut = BitmapFromResourceStream("Tests.Images.R2k.Converter.R2k3_out_auto_success.png");
            Assert.IsTrue(ImageEditor.IsEqual(converted, R2kOut));
        }

        [TestMethod()]
        public void Convert_RM2K3AnimToMVTest()
        {
            converter = new TilesetConverterVerticalRM2K3(Tileset.R2k_2k3_AnimObj, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.R2k.R2k3_in.png"))[0];
            Bitmap R2kOut = BitmapFromResourceStream("Tests.Images.R2k.Converter.R2k3_out_anim_success.png");
            Assert.IsTrue(ImageEditor.IsEqual(converted, R2kOut));
        }

        [TestMethod()]
        public void Convert_RM2K3ABToMVTest()
        {
            converter = new TilesetConverterVerticalRM2K3(Tileset.R2k_2k3_AB, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap[] converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.R2k.R2k3_in.png"));
            Bitmap R2kOut1 = BitmapFromResourceStream("Tests.Images.R2k.Converter.R2k3_out_ab_success_0.png");
            Bitmap R2kOut2 = BitmapFromResourceStream("Tests.Images.R2k.Converter.R2k3_out_ab_success_1.png");

            bool isTrue = ImageEditor.IsEqual(converted[0], R2kOut1) && ImageEditor.IsEqual(converted[1], R2kOut2);
            Assert.IsTrue(isTrue);
        }

        [TestMethod()]
        public void Convert_RM2K3AToMVTest()
        {
            converter = new TilesetConverterVerticalRM2K3(Tileset.R2k_2k3_A, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.R2k.R2k3_in.png"))[0];
            Bitmap R2kOut = BitmapFromResourceStream("Tests.Images.R2k.Converter.R2k3_out_a_success.png");
            Assert.IsTrue(ImageEditor.IsEqual(converted, R2kOut));
        }

        [TestMethod()]
        public void Convert_RM2K3BToMVTest()
        {
            converter = new TilesetConverterVerticalRM2K3(Tileset.R2k_2k3_B, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap converted = converter.ConvertToMV(BitmapFromResourceStream("Tests.Images.R2k.R2k3_in.png"))[0];
            Bitmap R2kOut = BitmapFromResourceStream("Tests.Images.R2k.Converter.R2k3_out_b_success.png");
            Assert.IsTrue(ImageEditor.IsEqual(converted, R2kOut));
        }
    }
}