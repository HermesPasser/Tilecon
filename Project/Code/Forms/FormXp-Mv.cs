using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using tilecon.Converter;

namespace tilecon
{
    public partial class FormXpMv : Form
    {
        private string filepath;
        private Bitmap[] bitmaps;
        private int bmpCurrentIndex;

        public static FormXpMv xpmvController;

        public FormXpMv()
        {
            xpmvController = this;
            InitializeComponent();
            cbMaker.SelectedIndexChanged += new EventHandler(cbMaker_SelectedIndexChanged);
            ChangeLang(Vocab.lang.eng);
            cbMaker.SelectedIndex = 7;
            cbMode.SelectedIndex = 0;
        }

        private void ChangeLang(Vocab.lang l)
        {
            Vocab.changeLang(l);
            saveFileDialog1.Filter = Vocab.pgnFilesText + " (*.png) | *.png";
            openFileDialog1.Filter = Vocab.imageFilesText + " (*.gif, *.bmp, *.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.gif; *bmp; *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            btnConvert.Text = Vocab.btnConvert;
            btnCutSave.Text = Vocab.btnCut;
            btnSearch.Text = Vocab.btnOpen;
            btnSave.Text = Vocab.btnSave;
            btnTransparency.Text = Vocab.btnTransparency;
            checkIgnore.Text = Vocab.cbIgnore;

            groupConversion.Text = Vocab.groupConversion;
            groupUtilities.Text = Vocab.groupUtilities;

            cbMode.Items[0] = Vocab.comboNone;
            cbMode.Items[1] = Vocab.comboCentralize;
            cbMode.Items[2] = Vocab.comboResize;

            menuStrip1.Items[0].Text = Vocab.file;
            openTilesetToolStripMenuItem.Text = Vocab.btnOpen;
            saveToolStripMenuItem.Text = Vocab.btnSave;
            exitToolStripMenuItem.Text = Vocab.archiveExit;

            menuStrip1.Items[1].Text = Vocab.groupUtilities;
            cutsaveIndividualFramesToolStripMenuItem.Text = Vocab.btnCut;

            menuStrip1.Items[2].Text = Vocab.convert;
            ignoreToolStripMenuItem.Text = Vocab.cbIgnore;
            modeToolStripMenuItem.Text = Vocab.mode;
            noneToolStripMenuItem.Text = Vocab.comboNone;
            centralizeToolStripMenuItem.Text = Vocab.comboCentralize;
            resizeToolStripMenuItem.Text = Vocab.comboResize;
              
            convertAndSaveToolStripMenuItem.Text = Vocab.btnConvert;
            setTransparentPixelToolStripMenuItem.Text = Vocab.btnTransparency;

            menuStrip1.Items[3].Text = Vocab.language;
            englishToolStripMenuItem.Text = Vocab.languageEng;
            portugueseToolStripMenuItem.Text = Vocab.languagePtbr;

            menuStrip1.Items[4].Text = Vocab.help;
            aboutToolStripMenuItem.Text = Vocab.helpAbout;
        }

        private bool OpenTileset()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                btnCutSave.Enabled = btnConvert.Enabled = true;
                checkIgnore.Enabled = cbMode.Enabled = true;
                cutsaveIndividualFramesToolStripMenuItem.Enabled = true;
                convertAndSaveToolStripMenuItem.Enabled = true;
                modeToolStripMenuItem.Enabled = true;
                ignoreToolStripMenuItem.Enabled = true;

                filepath = openFileDialog1.FileName;
                pictureBoxXP.Image = Image.FromFile(filepath);
                return true;
            }
            return false;
        }

        private void CutSave()
        {
            Maker.Tileset tile = GetTileset();
            var thread = new Thread(() =>
            {
                try
                {
                    TilesetConverterVertical cc = new TilesetConverterVertical(tile, spriteMode.NONE, false);
                    cc.SaveEachSubimage(Image.FromFile(filepath), filepath);
                    MessageBox.Show(Vocab.doneMessage, "Tilecon");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            });
            thread.Start();
        }

        private spriteMode GetMode()
        {
            if (cbMode.SelectedIndex == 1)
                return spriteMode.CENTRALIZE;
            else if (cbMode.SelectedIndex == 2)
                return spriteMode.RESIZE;
            else return spriteMode.NONE;
        }

        private Maker.Tileset GetTileset()
        {
            switch (cbMaker.SelectedItem.ToString())
            {
                case "RPG Maker 95":
                    return Maker.Tileset.R95;
                case "Sim RPG Maker 97":
                    return Maker.Tileset.S97;
                case "RPG Maker Alpha":
                    return Maker.Tileset.Alpha;
                case "RPG Maker 2000/2003 (Autotiles)":
                    return Maker.Tileset.R2000_2003_Auto;
                case "RPG Maker 2000/2003 (Tileset A-B)":
                    return Maker.Tileset.R2000_2003_AB;
                case "RPG Maker 2000/2003 (Tileset A)":
                    return Maker.Tileset.R2000_2003_A;
                case "RPG Maker 2000/2003 (Tileset B)":
                    return Maker.Tileset.R2000_2003_B;
                case "RPG Maker VX/Ace (Tileset A1-2)":
                    return Maker.Tileset.VX_Ace_A12;
                case "RPG Maker VX/Ace (Tileset A3)":
                    return Maker.Tileset.VX_Ace_A3;
                case "RPG Maker VX/Ace (Tileset A4)":
                    return Maker.Tileset.VX_Ace_A4;
                case "RPG Maker VX/Ace (Tileset A5)":
                    return Maker.Tileset.VX_Ace_A5;
                case "RPG Maker VX/Ace (Tileset B-E)":
                    return Maker.Tileset.VX_BE_Ace_BC;
                case "RPG Maker XP (Autotile)":
                    return Maker.Tileset.XP_Auto;
                case "RPG Maker XP":
                default:
                    return Maker.Tileset.XP;
            }
        }

        private void Convert()
        {          
            Maker.Tileset maker = GetTileset();
            spriteMode mode = GetMode();
            btnConvert.Text = Vocab.waitMessage;
            
            try
            {
                switch (maker)
                {
                    case Maker.Tileset.Alpha:
                        TilesetConverterVerticalApha con = new TilesetConverterVerticalApha(maker, mode, checkIgnore.Checked);
                        bitmaps = con.ConvertToMV(Image.FromFile(filepath));
                        break;

                    case Maker.Tileset.R95:
                    case Maker.Tileset.S97:
                    case Maker.Tileset.XP:
                        TilesetConverterVertical con1 = new TilesetConverterVertical(maker, mode, checkIgnore.Checked);
                        bitmaps = con1.ConvertToMV(Image.FromFile(filepath));
                        break;

                    case Maker.Tileset.XP_Auto:
                        TilesetConverterAutotileXP conXPAuto = new TilesetConverterAutotileXP(maker, mode, checkIgnore.Checked);
                        bitmaps = conXPAuto.ConvertToMV(Image.FromFile(filepath));
                        break;

                    case Maker.Tileset.R2000_2003_A:
                    case Maker.Tileset.R2000_2003_B:
                    case Maker.Tileset.R2000_2003_AB:
                    case Maker.Tileset.R2000_2003_Auto:
                        TilesetConverterVerticalRM2K3 con2 = new TilesetConverterVerticalRM2K3(maker, mode, checkIgnore.Checked);
                        bitmaps = con2.ConvertToMV(Image.FromFile(filepath));
                        break;

                    default:
                        TilesetConverterVX con3 = new TilesetConverterVX(maker, mode, checkIgnore.Checked);
                        bitmaps = con3.ConvertToMV(Image.FromFile(filepath));
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            
            if (bitmaps == null)
            {
                btnConvert.Text = Vocab.btnConvert;
                return;
            }

            pictureBox2.Image = bitmaps[0];
            btnSave.Enabled = saveToolStripMenuItem.Enabled = true;
            btnNextImg.Enabled = btnPreviusImg.Enabled = false;
            btnTransparency.Enabled = true;
            setTransparentPixelToolStripMenuItem.Enabled = true;

            if (bitmaps.Length > 1)
                btnNextImg.Enabled = btnPreviusImg.Enabled = true;
            else btnNextImg.Enabled = btnPreviusImg.Enabled = false;

            bmpCurrentIndex = 0;
            labelMVPagesNumber.Text = bmpCurrentIndex + 1 + "/" + bitmaps.Length;
            btnConvert.Text = Vocab.btnConvert;
        }

        private void Save()
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            if (saveFileDialog1.FileName == filepath)
            {
                MessageBox.Show(Vocab.SaveErrorMessage);
                return;
            }

            int index = saveFileDialog1.FileName.LastIndexOf(".");
            string fileDir = saveFileDialog1.FileName.Substring(0, index) + "_";

            if (bitmaps.Length != 1)
            {
                for (int i = 0; i < bitmaps.Length; i++)
                    bitmaps[i].Save(fileDir + i + ".png");
            }
            else bitmaps[0].Save(saveFileDialog1.FileName);
        }

        private void OnIndexChange()
        {
            TilesetConverterVX con = new TilesetConverterVX(GetTileset(), GetMode(), checkIgnore.Checked);
            switch (con.GetOutputMaker())
            {
                case Maker.Tileset.MV_A12:
                    labelMVTilesetName.Text = "A1-2";
                    break;
                case Maker.Tileset.MV_A3:
                    labelMVTilesetName.Text = "A3";
                    break;
                case Maker.Tileset.MV_A4:
                    labelMVTilesetName.Text = "A4";
                    break;
                case Maker.Tileset.MV_A5:
                    labelMVTilesetName.Text = "A5";
                    break;
                default:
                    labelMVTilesetName.Text = "B-E";
                    break;
            }
        }

        private void SetTransparentPixel()
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < bitmaps.Length; i++)
                    bitmaps[i] = ImageProcessing.ChangePixelsColor(bitmaps[i], colorDialog1.Color);
                pictureBox2.Image = bitmaps[bmpCurrentIndex];
            }
        }

        private void NextImage()
        {
            bmpCurrentIndex++;
            if (bmpCurrentIndex >= bitmaps.Length)
                bmpCurrentIndex = 0;

            pictureBox2.Image = bitmaps[bmpCurrentIndex];
            labelMVPagesNumber.Text = bmpCurrentIndex + 1 + "/" + bitmaps.Length;
        }

        private void PreviusImage()
        {
            bmpCurrentIndex--;
            if (bmpCurrentIndex < 0)
                bmpCurrentIndex = bitmaps.Length - 1;

            pictureBox2.Image = bitmaps[bmpCurrentIndex];
            labelMVPagesNumber.Text = bmpCurrentIndex + 1 + "/" + bitmaps.Length;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            Convert();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            OpenTileset();
        }

        private void btnCutSave_Click(object sender, EventArgs e)
        {
            CutSave();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void AboutTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2000 f = new Form2000();
            f.Show();
        }
        
        private void rMXPAutotileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormXPAuto f = new FormXPAuto();
            f.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout form = new FormAbout();
            form.Show();
            this.Enabled = false;
        }

        private void cutsaveIndividualFramesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutSave();
        }

        private void convertAndSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Convert();
        }

        private void setTransparentPixelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTransparentPixel();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLang(Vocab.lang.eng);
        }

        private void portugueseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLang(Vocab.lang.ptbr);
        }

        private void openTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTileset();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void cutsaveIndividualFramesToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            CutSave();
        }

        private void ignoreAlphaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkIgnore.Checked = ignoreToolStripMenuItem.Checked;
        }

        private void checkIgnore_CheckedChanged(object sender, EventArgs e)
        {
            ignoreToolStripMenuItem.Checked = checkIgnore.Checked;
        } 

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 0;
        }

        private void centralizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 1;
        }

        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 2;
        }

        private void loopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 2;
        }

        private void interpolationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 3;
        }

        private void rPGMaker95ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 0;
        }

        private void simRPGMaker97_SMItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 1;
        }

        private void rPGMakerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 2;
        }

        private void rPGMaker20002003AutotilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 3;
        }

        private void rPGMaker20002003TilesetAB_SMItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 4;
        }

        private void rPGMaker20002003TilesetA_SMItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 5;
        }

        private void rPGMaker20002003TilesetB_SMItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 6;
        }

        private void rPGMakerXPAutotileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 7;
        }

        private void rPGMakerXPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 8;
        }

        private void rPGMakerVXAceTilesetA12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 9;
        }

        private void rPGMakerVXTilesetA3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 10;
        }

        private void rPGMakerVXAceTilesetA4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 11;
        }

        private void rPGMakerVXAceTilesetA5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 12;
        }

        private void rPGMakerVXAceTilesetBEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 13;
        }

        private void btnNextImg_Click(object sender, EventArgs e)
        {
            NextImage();
        }

        private void btnPreviusImg_Click(object sender, EventArgs e)
        {
            PreviusImage();   
        }
        
        private void cbMaker_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnIndexChange();
        }

        private void btnSetPixelTransparent_Click(object sender, EventArgs e)
        {
            SetTransparentPixel();
        }
    }
}