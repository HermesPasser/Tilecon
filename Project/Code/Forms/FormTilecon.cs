using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using tilecon.Core.Converter;
using tilecon.Core;

namespace tilecon
{
    /// <summary>Form of interface for mv convertions and editor.</summary>
    public partial class FormTilecon : Form
    {
        // General
        private string filepath;

        /// <summary>Object reference to be called by other forms. </summary>
        public static FormTilecon controller;

        /// <summary>Default constructor.</summary>
        public FormTilecon()
        {
            InitializeComponent();
            BindDataSourceToCbMakerComboBox();

            ChangeLang(Vocab.Lang.en);
            btnOpen.Select();
            controller = this;

            // Options not available in Visual Studio properties
            cbMaker.SelectedIndexChanged += new EventHandler(OnIndexChange);
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

            convertAndSaveItem.Text = Vocab.GetText("convert");
            setTransparentItem.Text = Vocab.GetText("setTransparency");
            Vocab.GetText("ignoreAlpha");
            Vocab.GetText("conversion");

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
            converterControl1.ChangeLang();
            editor.ChangeLang();
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
            editor.Enabled = true;

            converterControl1.LoadTileset(filepath);

            editor.LoadTileset(filepath, GetSelectedInputTileset());
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
            ITileset tile = GetSelectedInputTileset();

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            btnSaveEachSprite.Enabled = false;

            (new Thread(() =>
            {
                try
                {
                    // FIXME: prevent stack overflow
                    tile = tile.TilesetName() == Core.Tileset.Custom(0).Name ?
                        Core.Tileset.Custom(byte.Parse(textCustomSize.Text))
                        : tile;

                    TilesetConverterVertical tilecon = new TilesetConverterCustom(tile, SpriteMode.ALIGN_TOP_LEFT, false);
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

        private ITileset GetSelectedInputTileset()
            => (ITileset) cbMaker.SelectedItem;

        private void Save(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Assert(converterControl1.ConvertedTilesets != null);
            if (converterControl1.ConvertedTilesets.Length == 0 && tabControl1.SelectedIndex == 0) return;

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            if (saveFileDialog1.FileName == filepath)
            {
                // Is this message really right?
                MessageBox.Show(Vocab.GetText("sizeNotMatchErrorMsg"));
                return;
            }

            if (tabControl1.SelectedIndex == 0)
                SaveConverter(saveFileDialog1.FileName);
            else
                editor.Save(saveFileDialog1.FileName);
        }

        private void OnIndexChange(object sender, EventArgs e)
        {
            textCustomSize.Enabled = GetSelectedInputTileset().TilesetName() == Core.Tileset.Custom(0).Name;
            converterControl1.UpdateOutputLabel((ITileset)cbMaker.SelectedItem);
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
            string tilesetName = sender.ToString();
            if (tilesetName == Core.Tileset.R95.Name) cbMaker.SelectedIndex = 0;
            else if (tilesetName == Core.Tileset.S97.Name) cbMaker.SelectedIndex = 1;
            else if (tilesetName == Core.Tileset.Alpha.Name) cbMaker.SelectedIndex = 2;
            else if (tilesetName == Core.Tileset.R2k_2k3_Auto.Name) cbMaker.SelectedIndex = 3;
            else if (tilesetName == Core.Tileset.R2k_2k3_AnimObj.Name) cbMaker.SelectedIndex = 4;
            else if (tilesetName == Core.Tileset.R2k_2k3_AB.Name) cbMaker.SelectedIndex = 5;
            else if (tilesetName == Core.Tileset.R2k_2k3_A.Name) cbMaker.SelectedIndex = 6;
            else if (tilesetName == Core.Tileset.R2k_2k3_B.Name) cbMaker.SelectedIndex = 7;
            else if (tilesetName == Core.Tileset.XP_Tile.Name) cbMaker.SelectedIndex = 8;
            else if (tilesetName == Core.Tileset.VX_Ace_A12.Name) cbMaker.SelectedIndex = 10;
            else if (tilesetName == Core.Tileset.VX_Ace_A3.Name) cbMaker.SelectedIndex = 11;
            else if (tilesetName == Core.Tileset.VX_Ace_A4.Name) cbMaker.SelectedIndex = 12;
            else if (tilesetName == Core.Tileset.VX_Ace_A5.Name) cbMaker.SelectedIndex = 13;
            else if (tilesetName == Core.Tileset.VX_Ace_BE.Name) cbMaker.SelectedIndex = 14;
            else if (tilesetName == Core.Tileset.XP_Auto.Name) cbMaker.SelectedIndex = 9;
            else cbMaker.SelectedIndex = 15; // custom
        }

        private void textCustomSize_TextChanged(object sender, EventArgs e)
        {
            // Since custom tilesets can have any size, why limit them to RM's tileset size?
            // maybe revert back the sprite size and the tileset size to an int?

            textCustomSize.Text = Regex.Replace(textCustomSize.Text, "[^0-9]", "");

            if (textCustomSize.Text == "")
                textCustomSize.Text = "0";

            int intVal = Int32.Parse(textCustomSize.Text);
            if (intVal > Byte.MaxValue)
            {
                textCustomSize.Text = Byte.MaxValue.ToString();
                editor.CustomSpriteSize = Byte.MaxValue;
            }
            else
                editor.CustomSpriteSize = Byte.Parse(textCustomSize.Text);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
            => Application.Exit();

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
            => ChangeLang(Vocab.Lang.en);


        private void portugueseToolStripMenuItem_Click(object sender, EventArgs e)
            => ChangeLang(Vocab.Lang.pt);
        

        private void ignoreAlphaToolStripMenuItem_Click(object sender, EventArgs e)
            => converterControl1.IgnoreAlpha = ignoreItem.Checked;

        private void checkIgnore_CheckedChanged(object sender, EventArgs e)
            => ignoreItem.Checked = converterControl1.IgnoreAlpha;

        #endregion

        #region Editor
        
        private void UpdateImage(object sender, EventArgs e) 
            => editor.UpdateImage((SpriteMode)cbMode.SelectedIndex);

        // set output tileset menustip
        private void LoadGrid(object sender, EventArgs e)
        {
            // Prevents from loading tileset in the editor when the user is using the converter
			if (tabControl1.SelectedIndex == 1)
                editor.LoadGrid();
        }

        // set output tileset tool stip
        private void SetOutputGrid(object sender, EventArgs e) => editor.SetOutputGrid();

        private void ClearPreview(object sender, EventArgs e) => editor.ClearPreview();

        private void outputTilesetItemChildItems_Click(object sender, EventArgs e)
        {
            int index;
            switch (sender.ToString())
            {
                case "RPG Maker MV (Tileset A1-2)": index = 0; break;
                case "RPG Maker MV (Tileset A3)": index = 1; break;
                case "RPG Maker MV (Tileset A4)": index = 2; break;
                case "RPG Maker MV (Tileset A5)": index = 3; break;
                default: index = 4; break;
            }

            editor.OutputComboBoxCurrentIndex = index;
        }
        #endregion

        private void SaveConverter(string filename) => converterControl1.SaveTilesets(filename);

        private void Convert(object sender, EventArgs e)
        {
            var tileset = GetSelectedInputTileset();

            // replaces the custom tileset with a new custom with the selected size
            if (tileset.TilesetName() == Core.Tileset.Custom(0).Name)
                tileset = Core.Tileset.Custom(Byte.Parse(textCustomSize.Text));

            converterControl1.Convert((SpriteMode)cbMode.SelectedIndex, tileset); 
            setTransparentItem.Enabled = true;
        }

        private void SetTransparentPixel(object sender, EventArgs e) 
            => converterControl1.SetTransparentColor();

        private void clearPreviewItem_Click(object sender, EventArgs e)
            => editor.ClearPreview();
    }
}