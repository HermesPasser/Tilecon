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

        #region Custom
        [TestMethod()]
        public void Editor_CustomToMV_A12()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Custom.Custom22px_in.png", "Tests.Images.Custom.Editor.Custom22px_out_MVA12.png", Tileset.Custom(22), Tileset.MV_A12));
        }

        [TestMethod()]
        public void Editor_CustomToMV_A3()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Custom.Custom22px_in.png", "Tests.Images.Custom.Editor.Custom22px_out_MVA3.png", Tileset.Custom(22), Tileset.MV_A3));
        }

        [TestMethod()]
        public void Editor_CustomToMV_A4()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Custom.Custom22px_in.png", "Tests.Images.Custom.Editor.Custom22px_out_MVA4.png", Tileset.Custom(22), Tileset.MV_A4));
        }

        [TestMethod()]
        public void Editor_CustomToMV_A5()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Custom.Custom22px_in.png", "Tests.Images.Custom.Editor.Custom22px_out_MVA5.png", Tileset.Custom(22), Tileset.MV_A5));
        }

        [TestMethod()]
        public void Editor_CustomToMV_BC()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Custom.Custom22px_in.png", "Tests.Images.Custom.Editor.Custom22px_out_MVBE.png", Tileset.Custom(22), Tileset.MV_BE));
        }
        #endregion

        #region Alpha
        [TestMethod()]
        public void Editor_AlphaToMV_A12()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Alpha.Alpha_in.png", "Tests.Images.Alpha.Editor.Alpha_out_MVA12.png", Tileset.Alpha, Tileset.MV_A12));
        }

        [TestMethod()]
        public void Editor_AlphaToMV_A3()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Alpha.Alpha_in.png", "Tests.Images.Alpha.Editor.Alpha_out_MVA3.png", Tileset.Alpha, Tileset.MV_A3));
        }

        [TestMethod()]
        public void Editor_AlphaToMV_A4()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Alpha.Alpha_in.png", "Tests.Images.Alpha.Editor.Alpha_out_MVA4.png", Tileset.Alpha, Tileset.MV_A4));
        }

        [TestMethod()]
        public void Editor_AlphaToMV_A5()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Alpha.Alpha_in.png", "Tests.Images.Alpha.Editor.Alpha_out_MVA5.png", Tileset.Alpha, Tileset.MV_A5));
        }

        [TestMethod()]
        public void Editor_AlphaToMV_BC()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.Alpha.Alpha_in.png", "Tests.Images.Alpha.Editor.Alpha_out_MVBE.png", Tileset.Alpha, Tileset.MV_BE));
        }
        #endregion

        #region 95
        [TestMethod()]
        public void Editor_R95ToMV_A12()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.R95.95_in.bmp", "Tests.Images.R95.Editor.95_out_MVA12.png", Tileset.R95, Tileset.MV_A12));
        }

        [TestMethod()]
        public void Editor_R95ToMV_A3()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.R95.95_in.bmp", "Tests.Images.R95.Editor.95_out_MVA3.png", Tileset.R95, Tileset.MV_A3));
        }

        [TestMethod()]
        public void Editor_R95ToMV_A4()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.R95.95_in.bmp", "Tests.Images.R95.Editor.95_out_MVA4.png", Tileset.R95, Tileset.MV_A4));
        }

        [TestMethod()]
        public void Editor_R95ToMV_A5()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.R95.95_in.bmp", "Tests.Images.R95.Editor.95_out_MVA5.png", Tileset.R95, Tileset.MV_A5));
        }

        [TestMethod()]
        public void Editor_R95ToMV_BC()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.R95.95_in.bmp", "Tests.Images.R95.Editor.95_out_MVBE.png", Tileset.R95, Tileset.MV_BE));
        }
        #endregion

        #region 97
        [TestMethod()]
        public void Editor_S97ToMV_A12()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.S97.97_in.bmp", "Tests.Images.S97.Editor.97_out_MVA12.png", Tileset.S97, Tileset.MV_A12));
        }

        [TestMethod()]
        public void Editor_S97ToMV_A3()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.S97.97_in.bmp", "Tests.Images.S97.Editor.97_out_MVA3.png", Tileset.S97, Tileset.MV_A3));
        }

        [TestMethod()]
        public void Editor_S97ToMV_A4()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.S97.97_in.bmp", "Tests.Images.S97.Editor.97_out_MVA4.png", Tileset.S97, Tileset.MV_A4));
        }

        [TestMethod()]
        public void Editor_S97ToMV_A5()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.S97.97_in.bmp", "Tests.Images.S97.Editor.97_out_MVA5.png", Tileset.S97, Tileset.MV_A5));
        }

        [TestMethod()]
        public void Editor_S97ToMV_BC()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.S97.97_in.bmp", "Tests.Images.S97.Editor.97_out_MVBE.png", Tileset.S97, Tileset.MV_BE));
        }
        #endregion

        #region R2k
        [TestMethod()]
        public void Editor_R2kToMV_A12()
        {
            bool b1 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA12.png", Tileset.R2k_2k3_AB, Tileset.MV_A12);
            bool b2 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA12.png", Tileset.R2k_2k3_A, Tileset.MV_A12);
            bool b3 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA12.png", Tileset.R2k_2k3_B, Tileset.MV_A12);
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_R2kToMV_A3()
        {
            bool b1 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA3.png", Tileset.R2k_2k3_AB, Tileset.MV_A3);
            bool b2 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA3.png", Tileset.R2k_2k3_A, Tileset.MV_A3);
            bool b3 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA3.png", Tileset.R2k_2k3_B, Tileset.MV_A3);
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_R2kToMV_A4()
        {
            bool b1 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA4.png", Tileset.R2k_2k3_AB, Tileset.MV_A4);
            bool b2 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA4.png", Tileset.R2k_2k3_A, Tileset.MV_A4);
            bool b3 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA4.png", Tileset.R2k_2k3_B, Tileset.MV_A4);
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_R2kToMV_A5()
        {
            bool b1 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA5.png", Tileset.R2k_2k3_AB, Tileset.MV_A5);
            bool b2 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA5.png", Tileset.R2k_2k3_A, Tileset.MV_A5);
            bool b3 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVA5.png", Tileset.R2k_2k3_B, Tileset.MV_A5);
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_R2kToMV_BC()
        {
            bool b1 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVBE.png", Tileset.R2k_2k3_AB, Tileset.MV_BE);
            bool b2 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVBE.png", Tileset.R2k_2k3_A, Tileset.MV_BE);
            bool b3 = EmulateEditor("Tests.Images.R2k.R2k3_in.png", "Tests.Images.R2k.Editor.R2k3_out_MVBE.png", Tileset.R2k_2k3_B, Tileset.MV_BE);
            Assert.IsTrue(b1 && b2 && b3);
        }
        #endregion

        #region XP
        [TestMethod()]
        public void Editor_XPToMV_A12()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.XP.XP_in.png", "Tests.Images.XP.Editor.XP_Out_MVA12.png", Tileset.XP_Tile, Tileset.MV_A12));
        }

        [TestMethod()]
        public void Editor_XPToMV_A3()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.XP.XP_in.png", "Tests.Images.XP.Editor.XP_Out_MVA3.png", Tileset.XP_Tile, Tileset.MV_A3));
        }

        [TestMethod()]
        public void Editor_XPToMV_A4()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.XP.XP_in.png", "Tests.Images.XP.Editor.XP_Out_MVA4.png", Tileset.XP_Tile, Tileset.MV_A4));
        }

        [TestMethod()]
        public void Editor_XPToMV_A5()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.XP.XP_in.png", "Tests.Images.XP.Editor.XP_Out_MVA5.png", Tileset.XP_Tile, Tileset.MV_A5));
        }

        [TestMethod()]
        public void Editor_XPToMV_BC()
        {
            Assert.IsTrue(EmulateEditor("Tests.Images.XP.XP_in.png", "Tests.Images.XP.Editor.XP_Out_MVBE.png", Tileset.XP_Tile, Tileset.MV_BE));
        }
        #endregion

        #region VX
        [TestMethod()]
        public void Editor_VX12ToMV_A12()
        {
            bool b1 = EmulateEditor("Tests.Images.VX.VX_a12_in.png", "Tests.Images.VX.Editor.VX_Out_MVA12.png", Tileset.R2k_2k3_AB, Tileset.MV_A12);
            bool b2 = EmulateEditor("Tests.Images.VX.VX_a12_in.png", "Tests.Images.VX.Editor.VX_Out_MVA12.png", Tileset.R2k_2k3_A, Tileset.MV_A12);
            bool b3 = EmulateEditor("Tests.Images.VX.VX_a12_in.png", "Tests.Images.VX.Editor.VX_Out_MVA12.png", Tileset.R2k_2k3_B, Tileset.MV_A12);
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_VXToMV_A3()
        {
            bool b1 = EmulateEditor("Tests.Images.VX.VX_a3_in.png", "Tests.Images.VX.Editor.VX_Out_MVA3.png", Tileset.R2k_2k3_AB, Tileset.MV_A3);
            bool b2 = EmulateEditor("Tests.Images.VX.VX_a3_in.png", "Tests.Images.VX.Editor.VX_Out_MVA3.png", Tileset.R2k_2k3_A, Tileset.MV_A3);
            bool b3 = EmulateEditor("Tests.Images.VX.VX_a3_in.png", "Tests.Images.VX.Editor.VX_Out_MVA3.png", Tileset.R2k_2k3_B, Tileset.MV_A3);
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_VXToMV_A4()
        {
            bool b1 = EmulateEditor("Tests.Images.VX.VX_a4_in.png", "Tests.Images.VX.Editor.VX_Out_MVA4.png", Tileset.R2k_2k3_AB, Tileset.MV_A4);
            bool b2 = EmulateEditor("Tests.Images.VX.VX_a4_in.png", "Tests.Images.VX.Editor.VX_Out_MVA4.png", Tileset.R2k_2k3_A, Tileset.MV_A4);
            bool b3 = EmulateEditor("Tests.Images.VX.VX_a4_in.png", "Tests.Images.VX.Editor.VX_Out_MVA4.png", Tileset.R2k_2k3_B, Tileset.MV_A4);
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_VXToMV_A5()
        {
            bool b1 = EmulateEditor("Tests.Images.VX.VX_a5_in.png", "Tests.Images.VX.Editor.VX_Out_MVA5.png", Tileset.R2k_2k3_AB, Tileset.MV_A5);
            bool b2 = EmulateEditor("Tests.Images.VX.VX_a5_in.png", "Tests.Images.VX.Editor.VX_Out_MVA5.png", Tileset.R2k_2k3_A, Tileset.MV_A5);
            bool b3 = EmulateEditor("Tests.Images.VX.VX_a5_in.png", "Tests.Images.VX.Editor.VX_Out_MVA5.png", Tileset.R2k_2k3_B, Tileset.MV_A5);
            Assert.IsTrue(b1 && b2 && b3);
        }

        [TestMethod()]
        public void Editor_VXToMV_BC()
        {
            bool b1 = EmulateEditor("Tests.Images.VX.VX_be_in.png", "Tests.Images.VX.Editor.VX_Out_MVBE.png", Tileset.R2k_2k3_AB, Tileset.MV_BE);
            bool b2 = EmulateEditor("Tests.Images.VX.VX_be_in.png", "Tests.Images.VX.Editor.VX_Out_MVBE.png", Tileset.R2k_2k3_A, Tileset.MV_BE);
            bool b3 = EmulateEditor("Tests.Images.VX.VX_be_in.png", "Tests.Images.VX.Editor.VX_Out_MVBE.png", Tileset.R2k_2k3_B, Tileset.MV_BE);
            Assert.IsTrue(b1 && b2 && b3);
        }
        #endregion
    }
}