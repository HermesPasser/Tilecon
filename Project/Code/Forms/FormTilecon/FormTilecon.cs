using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using tilecon.Converter;

namespace tilecon
{
    public partial class FormTilecon : Form
    {
        private string filepath;

        public static FormTilecon formTileconController;

        public FormTilecon()
        {
            formTileconController = this;
            InitializeComponent();
            cbMaker.SelectedIndexChanged += new EventHandler(cbMaker_SelectedIndexChanged);
            ChangeLang(Vocab.lang.eng);
            cbMaker.SelectedIndex = 7;
            cbMode.SelectedIndex = 0;
            btnOpen.Select();
            
            cbOutput.SelectedIndex = 0;
            currentImage = currentImageRaw = null;
        }

        private void ChangeLang(Vocab.lang l)
        {
            Vocab.changeLang(l);
            saveFileDialog1.Filter = Vocab.pgnFilesText + " (*.png) | *.png";
            openFileDialog1.Filter = Vocab.imageFilesText + " (*.gif, *.bmp, *.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.gif; *bmp; *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            btnConvert.Text = Vocab.btnConvert;
            btnCutSave.Text = Vocab.btnCut;
            btnOpen.Text = Vocab.btnOpen;
            btnSave.Text = Vocab.btnSave;
            btnTransparency.Text = Vocab.btnTransparency;
            checkIgnore.Text = Vocab.cbIgnore;

            groupConversion.Text = Vocab.groupConversion;

            cbMode.Items[0] = topLeftItem.Text      = Vocab.comboTopLeft;
            cbMode.Items[1] = topCenterItem.Text    = Vocab.comboTopCenter;
            cbMode.Items[2] = topRightItem.Text     = Vocab.comboTopRight;
            cbMode.Items[3] = middleLeftItem.Text   = Vocab.comboMiddleLeft;
            cbMode.Items[4] = middleCenterItem.Text = Vocab.comboMiddleCenter;
            cbMode.Items[5] = middleRightItem.Text  = Vocab.comboMiddleRight;
            cbMode.Items[6] = bottomLeftItem.Text   = Vocab.comboBottomLeft;
            cbMode.Items[7] = bottomCenterItem.Text = Vocab.comboBottomCenter;
            cbMode.Items[8] = bottomRightItem.Text  = Vocab.comboBottomRight;
            cbMode.Items[9] = resizeItem.Text       = Vocab.comboResize;

            menuStrip1.Items[0].Text = Vocab.file;
            openTilesetToolStripMenuItem.Text = Vocab.btnOpen;
            modeMenu.Text = Vocab.mode;
            saveIndividualFramesItem.Text = Vocab.btnCut;
            saveToolStripMenuItem.Text = Vocab.btnSave;
            exitToolStripMenuItem.Text = Vocab.fileExit;
           
            menuStrip1.Items[1].Text = editor.Text = Vocab.convert;
            ignoreItem.Text = Vocab.cbIgnore;
            convertAndSaveItem.Text = Vocab.btnConvert;
            setTransparenItem.Text = Vocab.btnTransparency;

            menuStrip1.Items[2].Text = converter.Text = Vocab.editor;
            setTileseItem.Text = Vocab.btnSetTileset;
            outputTilesetItem.Text = Vocab.btnOutputTileset;
            clearAndSetOutputTilesetItem.Text = Vocab.btnClearAndSetOutputTileset;

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
                saveIndividualFramesItem.Enabled = true;
                convertAndSaveItem.Enabled = true;
                btnTransparency.Enabled = setTransparenItem.Enabled = true;
                ignoreItem.Enabled = checkIgnore.Enabled = true;
                btnSetInput.Enabled = true;
                
                filepath = openFileDialog1.FileName;
                pictureBoxInput.Image = Image.FromFile(filepath);

                btnSetInput.Enabled = setTileseItem.Enabled = true;
                LoadGrid();
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
                    TilesetConverterVertical cc = new TilesetConverterVertical(tile, SpriteMode.ALIGN_TOP_LEFT, false);
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

        private SpriteMode GetMode()
        {
            switch (cbMode.SelectedIndex){
                case 0: return SpriteMode.ALIGN_TOP_LEFT;
                case 1: return SpriteMode.ALIGN_TOP_CENTER;
                case 2: return SpriteMode.ALIGN_TOP_RIGHT;
                case 3: return SpriteMode.ALIGN_MIDDLE_LEFT;
                case 4: return SpriteMode.ALIGN_MIDDLE_CENTER;
                case 5: return SpriteMode.ALIGN_MIDDLE_RIGHT;
                case 6: return SpriteMode.ALIGN_BOTTOM_LEFT;
                case 7: return SpriteMode.ALIGN_BOTTOM_CENTER;
                case 8: return SpriteMode.ALIGN_BOTTOM_RIGHT;
                case 9: return SpriteMode.RESIZE;
                default: return SpriteMode.ALIGN_TOP_LEFT;
            }
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

        private void Save()
        {
            if (bitmaps == null && tabControl1.SelectedIndex == 0) return;

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            if (saveFileDialog1.FileName == filepath)
            {
                MessageBox.Show(Vocab.SaveErrorMessage);
                return;
            }

            if (tabControl1.SelectedIndex == 0)  SaveConverter();
            else SaveEditor();
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
        
        private void saveIndividualFramesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutSave();
        }
        
        private void ignoreAlphaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkIgnore.Checked = ignoreItem.Checked;
        }

        private void checkIgnore_CheckedChanged(object sender, EventArgs e)
        {
            ignoreItem.Checked = checkIgnore.Checked;
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
        
        private void cbMaker_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnIndexChange();
        }

        private void topLeftItem_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 0;
        }

        private void topCenterItem_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 1;
        }

        private void topRightItem_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 2;
        }
        private void middleLeftItem_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 3;
        }

        private void middleCenterItem_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 4;
        }

        private void middleRightItem_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 5;
        }

        private void bottomLeftItem_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 6;
        }

        private void bottomCenterItem_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 7;
        }

        private void bottomRightItem_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 8;
        }

        private void resizeItem_Click(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 9;
        }
    }
}