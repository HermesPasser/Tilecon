using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using tilecon.Conversor;

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
            cbMaker.SelectedIndex = 6;
            cbMode.SelectedIndex = 0;
        }

        private void ChangeLang(Vocab.lang l)
        {
            Vocab.changeLang(l);
            saveFileDialog1.Filter = Vocab.pgnFilesText + " (*.png) | *.png";
            openFileDialog1.Filter = Vocab.imageFilesText + " (*.gif, *.bmp, *.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.webp) | *.gif; *bmp; *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.webp";
            btnConvert.Text = Vocab.btnConvert;
            btnCutSave.Text = Vocab.btnCut;
            btnSearch.Text = Vocab.btnOpen;
            btnSave.Text = Vocab.btnSave;
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
                btnCutSave.Enabled = true;
                btnConvert.Enabled = true;
                checkIgnore.Enabled = true;
                cbMode.Enabled = true;

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
            Maker.Tileset v = GetTileset();
            Converter con = new Converter(v);

            if (v == Maker.Tileset.R2000_2003_B)
                MessageBox.Show(Vocab.r2kMessageCut);

            var thread = new Thread(() =>
            {
                try
                { 
                    con.SaveEachSubimage(Image.FromFile(filepath), filepath);
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
                case "RPG Maker 2000/2003 (Tileset A-B)":
                    return Maker.Tileset.R2000_2003_AB;
                case "RPG Maker 2000/2003 (Tileset A)":
                    return Maker.Tileset.R2000_2003_A;
                case "RPG Maker 2000/2003 (Tileset B)":
                    return Maker.Tileset.R2000_2003_B;
                case "RPG Maker VX/Ace (Tileset A1-2)":
                    return Maker.Tileset.VX_Ace_A12;
                case "RPG Maker VX (Tileset A3)":
                    return Maker.Tileset.VX_A3;
                case "RPG Maker VX/Ace (Tileset A4)":
                    return Maker.Tileset.VX_Ace_A4;
                case "RPG Maker VX/Ace (Tileset A5)":
                    return Maker.Tileset.VX_Ace_A5;
                case "RPG Maker VX/Ace (Tileset B-E)":
                    return Maker.Tileset.VX_BE_Ace_BC;
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

            if (maker == Maker.Tileset.R2000_2003_B)
                MessageBox.Show(Vocab.r2kMessageConvert);

            Converter con = new Converter(maker, mode, checkIgnore.Checked);
            var thread = new Thread(() =>
            {
                try
                {
                    bitmaps = con.ConvertToMV(Image.FromFile(filepath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex+"");
                    return;
                }
            });

            thread.Start();
            thread.Join();

            if (bitmaps == null || bitmaps[0] == null) return;

            pictureBox2.Image = bitmaps[0];
            btnSave.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            btnNextImg.Enabled = false;
            btnPreviusImg.Enabled = false;

            if (bitmaps.Length > 1)
            {
                btnNextImg.Enabled = true;
                btnPreviusImg.Enabled = true;
            }
            btnConvert.Text = Vocab.btnConvert;
        }

        private void Save()
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
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
            Converter con = new Converter(GetTileset(), GetMode(), checkIgnore.Checked);
            switch (con.GetOutputMaker())
            {
                case Maker.Tileset.MV_A12:
                    labelMVTilesetName.Text = "A1-2";
                    break;
                case Maker.Tileset.MV_A4:
                    labelMVTilesetName.Text = "A4";
                    break;
                case Maker.Tileset.MV_A5:
                    labelMVTilesetName.Text = "A5";
                    break;
                case Maker.Tileset.MV_BC:
                    labelMVTilesetName.Text = "B-C";
                    break;
                default:
                    labelMVTilesetName.Text = "B-C";
                    break;
            }
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
            centralizeToolStripMenuItem.Checked = false;
            resizeToolStripMenuItem.Checked = false;
            cbMode.SelectedIndex = 0;
        }

        private void centralizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resizeToolStripMenuItem.Checked = false;
            noneToolStripMenuItem.Checked = false;
            cbMode.SelectedIndex = 1;
        }

        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            centralizeToolStripMenuItem.Checked = false;
            noneToolStripMenuItem.Checked = false;
            cbMode.SelectedIndex = 2;
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

        private void rPGMaker20002003TilesetAB_SMItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 3;
        }

        private void rPGMaker20002003TilesetA_SMItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 4;
        }

        private void rPGMaker20002003TilesetB_SMItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 5;
        }

        private void rPGMakerXP_SMItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 6;
        }

        private void rPGMakerVXAceTilesetA12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 7;
        }

        private void rPGMakerVXTilesetA3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 8;
        }

        private void rPGMakerVXAceTilesetA4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 9;
        }

        private void rPGMakerVXAceTilesetA5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 10;
        }

        private void rPGMakerVXAceTilesetBEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbMaker.SelectedIndex = 11;
        }

        private void btnNextImg_Click(object sender, EventArgs e)
        {
            bmpCurrentIndex++;
            if (bmpCurrentIndex >= bitmaps.Length)
                bmpCurrentIndex = 0;

            pictureBox2.Image = bitmaps[bmpCurrentIndex];
        }

        private void btnPreviusImg_Click(object sender, EventArgs e)
        {
            bmpCurrentIndex--;
            if (bmpCurrentIndex < 0)
                bmpCurrentIndex = bitmaps.Length -1;

            pictureBox2.Image = bitmaps[bmpCurrentIndex];
        }

        private void cbMaker_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnIndexChange();
        }
    }
}