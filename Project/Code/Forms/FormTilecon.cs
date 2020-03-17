using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using tilecon.Tileset.Converter;
using tilecon.Tileset.Editor;

namespace tilecon
{
    /// <summary>Form of interface for mv convertions and editor.</summary>
    public partial class FormTilecon : Form
    {
        // General
        private string filepath;

        /// <summary>Object reference to be called by other forms. </summary>
        public static FormTilecon controller;

        // Editor
        TilesetEditorIntput gridInp;
        TilesetEditorOutput gridOut;

        /// <summary>Default constructor.</summary>
        public FormTilecon()
        {
            InitializeComponent();

            ChangeLang(Vocab.Lang.en);
            btnOpen.Select();
            controller = this;

            // Options not available in Visual Studio properties
            cbMaker.SelectedIndexChanged += new EventHandler(OnIndexChange);
            cbMaker.SelectedIndex = 8;
            cbMode.SelectedIndex = 0;
            cbOutput.SelectedIndex = 4;
        }

        #region General
        // TODO: make it work in convertercontrol too
        private void ChangeLang(Vocab.Lang l)
        {
            Vocab.currentLanguage = l;

            saveFileDialog1.Filter = Vocab.GetText("pngFiles") + " (*.png) | *.png";
            openFileDialog1.Filter = Vocab.GetText("imageFiles") + " (*.gif, *.bmp, *.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.gif; *bmp; *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            /*
               TODO: Move this to ConverterControl
            btnConvert.Text = convertAndSaveItem.Text = Vocab.GetText("convert");
            btnTransparency.Text = setTransparentItem.Text = Vocab.GetText("setTransparency");
            checkIgnore.Text = Vocab.GetText("ignoreAlpha");
            groupConversion.Text = Vocab.GetText("conversion");
            */

            labelSpriteSize.Text = Vocab.GetText("spriteSize");
            btnSaveEachSprite.Text = saveEachSpritesItem.Text = Vocab.GetText("saveEachSprite");
            btnOpen.Text = openTilesetItem.Text = Vocab.GetText("openTileset");
            btnSave.Text = saveToolStripItem.Text = Vocab.GetText("save");
            ignoreItem.Text = Vocab.GetText("ignoreAlpha");
            
            cbMode.Items[0] = topLeftItem.Text = Vocab.GetText("topLeftAlign");
            cbMode.Items[1] = topCenterItem.Text = Vocab.GetText("topCenterAlign");
            cbMode.Items[2] = topRightItem.Text = Vocab.GetText("topRightAlign");
            cbMode.Items[3] = middleLeftItem.Text = Vocab.GetText("middleLeftAlign");
            cbMode.Items[4] = middleCenterItem.Text = Vocab.GetText("middleCenterAlign");
            cbMode.Items[5] = middleRightItem.Text = Vocab.GetText("middleRightAlign");
            cbMode.Items[6] = bottomLeftItem.Text = Vocab.GetText("bottomLeftAlign");
            cbMode.Items[7] = bottomCenterItem.Text = Vocab.GetText("bottomCenterAlign");
            cbMode.Items[8] = bottomRightItem.Text = Vocab.GetText("bottomRightAlign");
            cbMode.Items[9] = resizeItem.Text = Vocab.GetText("resize");

            menuStrip1.Items[0].Text = Vocab.GetText("file");
            modeMenu.Text = Vocab.GetText("mode");
            exitItem.Text = Vocab.GetText("exit");

            menuStrip1.Items[1].Text = tabConverter.Text = Vocab.GetText("converter");
            menuStrip1.Items[2].Text = tabEditor.Text = Vocab.GetText("editor");

            setInputTilesetItem.Text = btnSetInputTileset.Text =  Vocab.GetText("setTileset");
            clearPreviewItem.Text = btnClearPreview.Text = Vocab.GetText("clearSelectedTile");
            outputTilesetItem.Text = Vocab.GetText("outputTileset");
            clearAndSetOutputTilesetItem.Text = Vocab.GetText("clearAndSetTileset");

            menuStrip1.Items[3].Text = Vocab.GetText("language");
            englishToolStripMenuItem.Text = Vocab.GetText("english");
            portugueseToolStripMenuItem.Text = Vocab.GetText("portuguese");

            menuStrip1.Items[4].Text = Vocab.GetText("help");
            aboutToolStripMenuItem.Text = Vocab.GetText("about");

            btnClearAndSet.Text = Vocab.GetText("clearAndSetTileset");
        }

        private void OnTilesetLoad(String filepath)
        {
            this.filepath = filepath;

            // Enable all controls
            btnSaveEachSprite.Enabled = true;

            saveEachSpritesItem.Enabled = true;
            convertAndSaveItem.Enabled = true;

            setTransparentItem.Enabled = true;

            ignoreItem.Enabled = true;

            btnSetInputTileset.Enabled = true;
            setInputTilesetItem.Enabled = true;

            converterControl1.LoadTileset(filepath);

            // Load/Reset the grid
            LoadGrid(null, null);
        }

        private void LoadTilesetByDialog(object sender = null, EventArgs e = null)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                OnTilesetLoad(openFileDialog1.FileName);
        }

        private void UIThread(MethodInvoker code) // Modified of: www.codeproject.com/Articles/37642/Avoiding-InvokeRequired
        {
            if (this.InvokeRequired) this.Invoke(code);
            else code.Invoke();
        }

        private void CutSave(object sender, EventArgs e)
        {
            ITileset tile = GetTileset();

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            btnSaveEachSprite.Enabled = false;

            (new Thread(() =>
            {
                try
                {
                    int size = tile.TilesetName() == Maker.Custom.NAME ? Int32.Parse(textCustomSize.Text) : tile.SpriteSize();
                    TilesetConverterVertical tilecon = new TilesetConverterCustom(SpriteMode.ALIGN_TOP_LEFT, false, size);
                    tilecon.SaveEachSubimage(Image.FromFile(filepath), saveFileDialog1.FileName);

                    MessageBox.Show(Vocab.GetText("done"), "Tilecon");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    UIThread(delegate { btnSaveEachSprite.Enabled = true; });
                }
            })).Start();
        }

        private ITileset GetTileset()
        {
            switch (cbMaker.SelectedItem.ToString())
            {
                // TODO: add reflection here
                case Maker.R95.NAME: return new Maker.R95();
                case Maker.S97.NAME: return new Maker.S97();
                case Maker.Alpha.NAME: return new Maker.Alpha();
                case Maker.R2k_2k3_AnimObj.NAME: return new Maker.R2k_2k3_AnimObj();
                case Maker.R2k_2k3_Auto.NAME: return new Maker.R2k_2k3_Auto();
                case Maker.R2k_2k3_AB.NAME: return new Maker.R2k_2k3_AB();
                case Maker.R2k_2k3_A.NAME: return new Maker.R2k_2k3_A();
                case Maker.R2k_2k3_B.NAME: return new Maker.R2k_2k3_B();
                case Maker.XP_Auto.NAME: return new Maker.XP_Auto();
                case Maker.XP_Tile.NAME: return new Maker.XP_Tile();
                case Maker.VX_Ace_A12.NAME: return new Maker.VX_Ace_A12();
                case Maker.VX_Ace_A3.NAME: return new Maker.VX_Ace_A3();
                case Maker.VX_Ace_A4.NAME: return new Maker.VX_Ace_A4();
                case Maker.VX_Ace_A5.NAME: return new Maker.VX_Ace_A5();
                case Maker.VX_Ace_BE.NAME: return new Maker.VX_Ace_BE();
                case Maker.Custom.NAME:
                default: return new Maker.Custom();
            }
        }

        private void Save(object sender, EventArgs e)
        {
            if (converterControl1.Bitmaps == null && tabControl1.SelectedIndex == 0) return;

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            if (saveFileDialog1.FileName == filepath)
            {
                MessageBox.Show(Vocab.GetText("sizeNotMatchErrorMsg"));
                return;
            }

            if (tabControl1.SelectedIndex == 0)
                SaveConverter();
            else SaveEditor();
        }

        private void OnIndexChange(object sender, EventArgs e)
        {
            textCustomSize.Enabled = GetTileset().TilesetName() == Maker.Custom.NAME;
            converterControl1.UpdateOutputLabel((SpriteMode)cbMode.SelectedIndex);
        }

        private void FormTilecon_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string extension = Path.GetExtension(files[0]).ToLower();

                if (extension == ".jpg" || extension == ".gif" || extension == ".bmp" || extension == ".png" || extension == ".jpeg" || extension == ".jfif")
                {
                    e.Effect = DragDropEffects.All;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void FormTilecon_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            OnTilesetLoad(files[0]);
        }

        private void SetModeByMenuItem(object sender, EventArgs e)
        {
            switch (sender.ToString())
            {
                case "Top Left Align": cbMode.SelectedIndex = 0; break;
                case "Top Center Align": cbMode.SelectedIndex = 1; break;
                case "Top Right Align": cbMode.SelectedIndex = 2; break;

                case "Middle Left Align": cbMode.SelectedIndex = 3; break;
                case "Middle Center Align": cbMode.SelectedIndex = 4; break;
                case "Middle Right Align": cbMode.SelectedIndex = 5; break;

                case "Bottom Left Align": cbMode.SelectedIndex = 6; break;
                case "Bottom Center Align": cbMode.SelectedIndex = 7; break;
                case "Bottom Right Align": cbMode.SelectedIndex = 8; break;

                default: cbMode.SelectedIndex = 9; break;
            }
        }

        private void SetTilesetByMenuItem(object sender, EventArgs e)
        {
            switch (sender.ToString())
            {
                case Maker.R95.NAME: cbMaker.SelectedIndex = 0; break;
                case Maker.S97.NAME: cbMaker.SelectedIndex = 1; break;
                case Maker.Alpha.NAME: cbMaker.SelectedIndex = 2; break;
                case Maker.R2k_2k3_Auto.NAME: cbMaker.SelectedIndex = 3; break;
                case Maker.R2k_2k3_AnimObj.NAME: cbMaker.SelectedIndex = 4; break;
                case Maker.R2k_2k3_AB.NAME: cbMaker.SelectedIndex = 5; break;
                case Maker.R2k_2k3_A.NAME: cbMaker.SelectedIndex = 6; break;
                case Maker.R2k_2k3_B.NAME: cbMaker.SelectedIndex = 7; break;
                case Maker.XP_Tile.NAME: cbMaker.SelectedIndex = 8; break;
                case Maker.VX_Ace_A12.NAME: cbMaker.SelectedIndex = 10; break;
                case Maker.VX_Ace_A3.NAME: cbMaker.SelectedIndex = 11; break;
                case Maker.VX_Ace_A4.NAME: cbMaker.SelectedIndex = 12; break;
                case Maker.VX_Ace_A5.NAME: cbMaker.SelectedIndex = 13; break;
                case Maker.VX_Ace_BE.NAME: cbMaker.SelectedIndex = 14; break;
                case Maker.XP_Auto.NAME: cbMaker.SelectedIndex = 9; break;
                default: cbMaker.SelectedIndex = 15; break; // custom
            }
        }

        private void textCustomSize_TextChanged(object sender, EventArgs e)
        {
            textCustomSize.Text = Regex.Replace(textCustomSize.Text, "[^0-9]", "");

            if (textCustomSize.Text == "")
                textCustomSize.Text = "0";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2000 f = new Form2000();
            f.Show();
            this.Enabled = false;
        }

        private void rMXPAutotileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormXPAuto f = new FormXPAuto();
            f.Show();
            this.Enabled = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout form = new FormAbout();
            form.Show();
            this.Enabled = false;
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLang(Vocab.Lang.en);
        }

        private void portugueseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLang(Vocab.Lang.pt);
        }

        private void ignoreAlphaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            converterControl1.IgnoreAlpha = ignoreItem.Checked;
        }

        private void checkIgnore_CheckedChanged(object sender, EventArgs e)
        {
            ignoreItem.Checked = converterControl1.IgnoreAlpha;
        }
        #endregion

        #region Editor
        private void SaveEditor()
        {
            if (gridOut != null)
                (gridOut.TilesToTileset()).Save(saveFileDialog1.FileName);
        }
        
        private void UpdateImage(object sender, EventArgs e)
        {
            if (gridInp != null)
                gridInp.UpdateSelectedImage((SpriteMode)cbMode.SelectedIndex);   
        }

        private void LoadGrid(object sender, EventArgs e)
        {
			if (tabControl1.SelectedIndex != 1)
				return;
			
            Maker.Custom.SPRITE_SIZE = Int32.Parse(textCustomSize.Text);

            if (GetTileset().TilesetName() == Maker.Custom.NAME && Maker.Custom.SPRITE_SIZE == 0)
            {
                MessageBox.Show(Vocab.GetText("sizeIsZeroErrorMsg"));
                return;
            }

            Image img = Image.FromFile(filepath);
            pictureBoxPreview.Image = null;

            try
            {
                gridInp = new TilesetEditorIntput(GetTileset(), inputPanel, img, pictureBoxPreview);
                SetOutputGrid(null, null);
            }
            catch (ConvertException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetOutputGrid(object sender, EventArgs e)
        {
            ITileset tileset;

            switch (cbOutput.SelectedIndex)
            {
                case 0: tileset = new Maker.MV_A12(); break;
                case 1: tileset = new Maker.MV_A3(); break;
                case 2: tileset = new Maker.MV_A4(); break;
                case 3: tileset = new Maker.MV_A5(); break;
                case 4:
                default: tileset = new Maker.MV_BE(); break;
            }
            gridOut = new TilesetEditorOutput(tileset, outputPanel, gridInp);
        }

        private void ClearPreview(object sender, EventArgs e)
        {
            pictureBoxPreview.Image = null;
            if (gridInp != null)
                gridInp.selectedImage = null;
        }

        // Used in tool menu
        private void SetOutputTileset(object sender, EventArgs e)
        {
            switch (sender.ToString())
            {
                case "RPG Maker MV (Tileset A1-2)": cbOutput.SelectedIndex = 0; break;
                case "RPG Maker MV (Tileset A3)":   cbOutput.SelectedIndex = 1; break;
                case "RPG Maker MV (Tileset A4)":   cbOutput.SelectedIndex = 2; break;
                case "RPG Maker MV (Tileset A5)":   cbOutput.SelectedIndex = 3; break;
                case "RPG Maker MV (Tileset B-C)":  cbOutput.SelectedIndex = 4; break;
            }
        }
        #endregion

        private void SaveConverter()
        {
            converterControl1.SaveTilesets();
        }

        private void Convert(object sender, EventArgs e)
        {
            converterControl1.Convert((SpriteMode)cbMode.SelectedIndex, GetTileset(), int.Parse(textCustomSize.Text)); 
            setTransparentItem.Enabled = true;
        }

        private void SetTransparentPixel(object sender, EventArgs e)
        {
            converterControl1.SetTransparentColor();
        }
    }
}
