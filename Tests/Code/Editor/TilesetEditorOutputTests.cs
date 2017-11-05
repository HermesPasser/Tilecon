using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using tilecon.Tileset.Tests;

namespace tilecon.Tileset.Editor.Tests
{
    [TestClass()]
    public class TilesetEditorTests : TilesetTestBase
    {
        private bool EmulateEditor(string inputResPath, string outResPath, ITileset inputTileset, ITileset outputTileset)
        {
            Bitmap tile = BitmapFromResourceStream(inputResPath);
            Bitmap imgOut = BitmapFromResourceStream(outResPath);
            editorInput = new TilesetEditorIntput(inputTileset, null, tile, null);
            editorOutput = new TilesetEditorOutput(outputTileset, null, editorInput);
            editorInput.SetSelectedImage(0);

            for (int i = 0; i < 10; i++) // Number of SpriteModes
            {
                editorInput.UpdateSelectedImage(((SpriteMode)i));
                editorOutput.SetGridImage(i, editorInput.selectedImage);
            }
            editorOutput.SetGridImage(-1, editorInput.selectedImage);
            return ImageEditor.IsEqual(imgOut, editorOutput.TilesToTileset());
        }

        #region Alpha
        [TestMethod()]
        public void Editor_AlphaToMV_A12()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Alpha.Alpha_in.png", "Tests.Images.Alpha.Editor.Alpha_out_MVA12.png", new Maker.Alpha(), new Maker.MV_A12()));
        }

        [TestMethod()]
        public void Editor_AlphaToMV_A3()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Alpha.Alpha_in.png", "Tests.Images.Alpha.Editor.Alpha_out_MVA3.png", new Maker.Alpha(), new Maker.MV_A3()));
        }

        [TestMethod()]
        public void Editor_AlphaToMV_A4()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Alpha.Alpha_in.png", "Tests.Images.Alpha.Editor.Alpha_out_MVA4.png", new Maker.Alpha(), new Maker.MV_A4()));
        }

        [TestMethod()]
        public void Editor_AlphaToMV_A5()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Alpha.Alpha_in.png", "Tests.Images.Alpha.Editor.Alpha_out_MVA5.png", new Maker.Alpha(), new Maker.MV_A5()));
        }

        [TestMethod()]
        public void Editor_AlphaToMV_BC()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Alpha.Alpha_in.png", "Tests.Images.Alpha.Editor.Alpha_out_MVBE.png", new Maker.Alpha(), new Maker.MV_BE()));
        }
        #endregion

        #region 95
        [TestMethod()]
        public void Editor_R95ToMV_A12()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.R95.95_in.bmp", "Tests.Images.R95.Editor.95_out_MVA12.png", new Maker.R95(), new Maker.MV_A12()));
        }

        [TestMethod()]
        public void Editor_R95ToMV_A3()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.R95.95_in.bmp", "Tests.Images.R95.Editor.95_out_MVA3.png", new Maker.R95(), new Maker.MV_A3()));
        }

        [TestMethod()]
        public void Editor_R95ToMV_A4()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.R95.95_in.bmp", "Tests.Images.R95.Editor.95_out_MVA4.png", new Maker.R95(), new Maker.MV_A4()));
        }

        [TestMethod()]
        public void Editor_R95ToMV_A5()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.R95.95_in.bmp", "Tests.Images.R95.Editor.95_out_MVA5.png", new Maker.R95(), new Maker.MV_A5()));
        }

        [TestMethod()]
        public void Editor_R95ToMV_BC()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.R95.95_in.bmp", "Tests.Images.R95.Editor.95_out_MVBE.png", new Maker.R95(), new Maker.MV_BE()));
        }
        #endregion

        #region 97
        [TestMethod()]
        public void Editor_S97ToMV_A12()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.S97.97_in.bmp", "Tests.Images.S97.Editor.97_out_MVA12.png", new Maker.S97(), new Maker.MV_A12()));
        }

        [TestMethod()]
        public void Editor_S97ToMV_A3()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.S97.97_in.bmp", "Tests.Images.S97.Editor.97_out_MVA3.png", new Maker.S97(), new Maker.MV_A3()));
        }

        [TestMethod()]
        public void Editor_S97ToMV_A4()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.S97.97_in.bmp", "Tests.Images.S97.Editor.97_out_MVA4.png", new Maker.S97(), new Maker.MV_A4()));
        }

        [TestMethod()]
        public void Editor_S97ToMV_A5()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.S97.97_in.bmp", "Tests.Images.S97.Editor.97_out_MVA5.png", new Maker.S97(), new Maker.MV_A5()));
        }

        [TestMethod()]
        public void Editor_S97ToMV_BC()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.S97.97_in.bmp", "Tests.Images.S97.Editor.97_out_MVBE.png", new Maker.S97(), new Maker.MV_BE()));
        }
        #endregion

        #region R2k
        [TestMethod()]
        public void Editor_R2kToMV_A12()
        {
            bool b1 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA12.png", new Maker.R2k_2k3_AB(), new Maker.MV_A12());
            bool b2 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA12.png", new Maker.R2k_2k3_A(), new Maker.MV_A12());
            bool b3 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA12.png", new Maker.R2k_2k3_B(), new Maker.MV_A12());
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_R2kToMV_A3()
        {
            bool b1 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA3.png", new Maker.R2k_2k3_AB(), new Maker.MV_A3());
            bool b2 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA3.png", new Maker.R2k_2k3_A(), new Maker.MV_A3());
            bool b3 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA3.png", new Maker.R2k_2k3_B(), new Maker.MV_A3());
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_R2kToMV_A4()
        {
            bool b1 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA4.png", new Maker.R2k_2k3_AB(), new Maker.MV_A4());
            bool b2 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA4.png", new Maker.R2k_2k3_A(), new Maker.MV_A4());
            bool b3 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA4.png", new Maker.R2k_2k3_B(), new Maker.MV_A4());
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_R2kToMV_A5()
        {
            bool b1 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA5.png", new Maker.R2k_2k3_AB(), new Maker.MV_A5());
            bool b2 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA5.png", new Maker.R2k_2k3_A(), new Maker.MV_A5());
            bool b3 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA5.png", new Maker.R2k_2k3_B(), new Maker.MV_A5());
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_R2kToMV_BC()
        {
            bool b1 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVBE.png", new Maker.R2k_2k3_AB(), new Maker.MV_BE());
            bool b2 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVBE.png", new Maker.R2k_2k3_A(), new Maker.MV_BE());
            bool b3 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVBE.png", new Maker.R2k_2k3_B(), new Maker.MV_BE());
            Assert.IsTrue(b1 && b2 && b3);
        }
        #endregion

        #region XP
        [TestMethod()]
        public void Editor_XPToMV_A12()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.XP.XP_in.png", "Tests.Images.XP.Editor.XP_Out_MVA12.png", new Maker.XP_Tile(), new Maker.MV_A12()));
        }

        [TestMethod()]
        public void Editor_XPToMV_A3()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.XP.XP_in.png", "Tests.Images.XP.Editor.XP_Out_MVA3.png", new Maker.XP_Tile(), new Maker.MV_A3()));
        }

        [TestMethod()]
        public void Editor_XPToMV_A4()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.XP.XP_in.png", "Tests.Images.XP.Editor.XP_Out_MVA4.png", new Maker.XP_Tile(), new Maker.MV_A4()));
        }

        [TestMethod()]
        public void Editor_XPToMV_A5()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.XP.XP_in.png", "Tests.Images.XP.Editor.XP_Out_MVA5.png", new Maker.XP_Tile(), new Maker.MV_A5()));
        }

        [TestMethod()]
        public void Editor_XPToMV_BC()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.XP.XP_in.png", "Tests.Images.XP.Editor.XP_Out_MVBE.png", new Maker.XP_Tile(), new Maker.MV_BE()));
        }
        #endregion

        #region VX
        [TestMethod()]
        public void Editor_VX12ToMV_A12()
        {
            bool b1 = EmulateEditor("Tests.Images.VX.VX_a12_in.png", "Tests.Images.VX.Editor.VX_Out_MVA12.png", new Maker.R2k_2k3_AB(), new Maker.MV_A12());
            bool b2 = EmulateEditor("Tests.Images.VX.VX_a12_in.png", "Tests.Images.VX.Editor.VX_Out_MVA12.png", new Maker.R2k_2k3_A(), new Maker.MV_A12());
            bool b3 = EmulateEditor("Tests.Images.VX.VX_a12_in.png", "Tests.Images.VX.Editor.VX_Out_MVA12.png", new Maker.R2k_2k3_B(), new Maker.MV_A12());
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_VXToMV_A3()
        {
            bool b1 = EmulateEditor("Tests.Images.VX.VX_a3_in.png", "Tests.Images.VX.Editor.VX_Out_MVA3.png", new Maker.R2k_2k3_AB(), new Maker.MV_A3());
            bool b2 = EmulateEditor("Tests.Images.VX.VX_a3_in.png", "Tests.Images.VX.Editor.VX_Out_MVA3.png", new Maker.R2k_2k3_A(), new Maker.MV_A3());
            bool b3 = EmulateEditor("Tests.Images.VX.VX_a3_in.png", "Tests.Images.VX.Editor.VX_Out_MVA3.png", new Maker.R2k_2k3_B(), new Maker.MV_A3());
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_VXToMV_A4()
        {
            bool b1 = EmulateEditor("Tests.Images.VX.VX_a4_in.png", "Tests.Images.VX.Editor.VX_Out_MVA4.png", new Maker.R2k_2k3_AB(), new Maker.MV_A4());
            bool b2 = EmulateEditor("Tests.Images.VX.VX_a4_in.png", "Tests.Images.VX.Editor.VX_Out_MVA4.png", new Maker.R2k_2k3_A(), new Maker.MV_A4());
            bool b3 = EmulateEditor("Tests.Images.VX.VX_a4_in.png", "Tests.Images.VX.Editor.VX_Out_MVA4.png", new Maker.R2k_2k3_B(), new Maker.MV_A4());
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_VXToMV_A5()
        {
            bool b1 = EmulateEditor("Tests.Images.VX.VX_a5_in.png", "Tests.Images.VX.Editor.VX_Out_MVA5.png", new Maker.R2k_2k3_AB(), new Maker.MV_A5());
            bool b2 = EmulateEditor("Tests.Images.VX.VX_a5_in.png", "Tests.Images.VX.Editor.VX_Out_MVA5.png", new Maker.R2k_2k3_A(), new Maker.MV_A5());
            bool b3 = EmulateEditor("Tests.Images.VX.VX_a5_in.png", "Tests.Images.VX.Editor.VX_Out_MVA5.png", new Maker.R2k_2k3_B(), new Maker.MV_A5());
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_VXToMV_BC()
        {
            bool b1 = EmulateEditor("Tests.Images.VX.VX_be_in.png", "Tests.Images.VX.Editor.VX_Out_MVBE.png", new Maker.R2k_2k3_AB(), new Maker.MV_BE());
            bool b2 = EmulateEditor("Tests.Images.VX.VX_be_in.png", "Tests.Images.VX.Editor.VX_Out_MVBE.png", new Maker.R2k_2k3_A(), new Maker.MV_BE());
            bool b3 = EmulateEditor("Tests.Images.VX.VX_be_in.png", "Tests.Images.VX.Editor.VX_Out_MVBE.png", new Maker.R2k_2k3_B(), new Maker.MV_BE());
            Assert.IsTrue(b1 && b2 && b3);
        }
        #endregion
    }
}