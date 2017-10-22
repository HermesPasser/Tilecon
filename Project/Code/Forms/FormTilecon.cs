using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using tilecon.Converter;

namespace tilecon
{
    /// <summary>Form of interface for mv convertions and editor.</summary>
    public partial class FormTilecon : Form
    {
        // General
        private string filepath;
        /// <summary>Object reference to be called by other forms. </summary>
        public static FormTilecon formTileconController;
        //SpriteMode mode;

        // Editor
        List<Button> inputGrid;
        List<Button> outputGrid;
        Image currentImageRaw;
        Image currentImage;
        
        // Converter
        private Bitmap[] bitmaps;
        private int bmpCurrentIndex;
        //private Thread t;

        /// <summary>Default constructor.</summary>
        public FormTilecon()
        {
            formTileconController = this;
            InitializeComponent();
            cbMaker.SelectedIndexChanged += new EventHandler(cbMaker_SelectedIndexChanged);
            ChangeLang(Vocab.lang.en);
            cbMaker.SelectedIndex = 8;
            cbMode.SelectedIndex = 0;
            btnOpen.Select();

            cbOutput.SelectedIndex = 0;
            currentImage = currentImageRaw = null;
        }

        #region General

        private void ChangeLang(Vocab.lang l)
        {
            Vocab.currentLanguage = l;

            saveFileDialog1.Filter = Vocab.GetText("pngFiles")   + " (*.png) | *.png";
            openFileDialog1.Filter = Vocab.GetText("imageFiles") + " (*.gif, *.bmp, *.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.gif; *bmp; *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            btnConvert.Text        = convertAndSaveItem.Text  = Vocab.GetText("convert");
            btnSaveEachSprite.Text = saveEachSpritesItem.Text = Vocab.GetText("saveEachSprite");
            btnOpen.Text           = openTilesetItem.Text     = Vocab.GetText("openTileset");
            btnSave.Text           = saveToolStripItem.Text   = Vocab.GetText("save");
            btnTransparency.Text   = setTransparentItem.Text  = Vocab.GetText("setTransparency");
            ignoreItem.Text        = checkIgnore.Text         = Vocab.GetText("ignoreAlpha");

            groupConversion.Text = Vocab.GetText("conversion");

            cbMode.Items[0] = topLeftItem.Text      = Vocab.GetText("topLeftAlign");
            cbMode.Items[1] = topCenterItem.Text    = Vocab.GetText("topCenterAlign");
            cbMode.Items[2] = topRightItem.Text     = Vocab.GetText("topRightAlign");
            cbMode.Items[3] = middleLeftItem.Text   = Vocab.GetText("middleLeftAlign");
            cbMode.Items[4] = middleCenterItem.Text = Vocab.GetText("middleCenterAlign");
            cbMode.Items[5] = middleRightItem.Text  = Vocab.GetText("middleRightAlign");
            cbMode.Items[6] = bottomLeftItem.Text   = Vocab.GetText("bottomLeftAlign");
            cbMode.Items[7] = bottomCenterItem.Text = Vocab.GetText("bottomCenterAlign");
            cbMode.Items[8] = bottomRightItem.Text  = Vocab.GetText("bottomRightAlign");
            cbMode.Items[9] = resizeItem.Text       = Vocab.GetText("resize");

            menuStrip1.Items[0].Text = Vocab.GetText("file");
            modeMenu.Text = Vocab.GetText("mode");
           
            exitItem.Text = Vocab.GetText("exit");
            
            menuStrip1.Items[1].Text = tabConverter.Text = Vocab.GetText("converter");

            menuStrip1.Items[2].Text = tabEditor.Text = Vocab.GetText("editor");
            setTileseItem.Text = Vocab.GetText("setTileset");
            outputTilesetItem.Text = Vocab.GetText("outputTileset");
            clearAndSetOutputTilesetItem.Text = Vocab.GetText("clearAndSetTileset");

            menuStrip1.Items[3].Text = Vocab.GetText("language");
            englishToolStripMenuItem.Text = Vocab.GetText("english");
            portugueseToolStripMenuItem.Text = Vocab.GetText("portuguese");

            menuStrip1.Items[4].Text = Vocab.GetText("help");
            aboutToolStripMenuItem.Text = Vocab.GetText("about");

            btnClearAndSet.Text = Vocab.GetText("clearAndSetTileset");
            btnSetInput.Text = Vocab.GetText("setTileset");
        }

        private void OnTilesetLoad(String filepath)
        {
            // Set the new filepath and load
            this.filepath = filepath;
            pictureBoxInput.Image = Image.FromFile(filepath);

            // Enable all controls
            btnSaveEachSprite.Enabled = true;
            btnConvert.Enabled = true;

            saveEachSpritesItem.Enabled = true;
            convertAndSaveItem.Enabled = true;

            btnTransparency.Enabled = true;
            setTransparentItem.Enabled = true;

            ignoreItem.Enabled = true;
            checkIgnore.Enabled = true;

            btnSetInput.Enabled = true;
            setTileseItem.Enabled = true;

            // Load the grid
            LoadGrid();
        }

        private bool LoadTilesetByDialog()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OnTilesetLoad(openFileDialog1.FileName);
                return true;
            }
            return false;
        }

        private void UIThread(MethodInvoker code) // Modified of: www.codeproject.com/Articles/37642/Avoiding-InvokeRequired
        {
            if (this.InvokeRequired) this.Invoke(code);
            else code.Invoke();
        }

        private void CutSave()
        {
            ITileset tile = GetTileset();
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            btnSaveEachSprite.Enabled = false;

            (new Thread(() =>
            {
                try
                {
                    TilesetConverterVertical cc = new TilesetConverterVertical(tile, SpriteMode.ALIGN_TOP_LEFT, false);
                    cc.SaveEachSubimage(Image.FromFile(filepath), saveFileDialog1.FileName);
                    MessageBox.Show(Vocab.GetText("done"), "Tilecon");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
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
                case Maker.R95.NAME:             return new Maker.R95();
                case Maker.S97.NAME:             return new Maker.S97();
                case Maker.Alpha.NAME:           return new Maker.Alpha();
                case Maker.R2k_2k3_AnimObj.NAME: return new Maker.R2k_2k3_AnimObj();
                case Maker.R2k_2k3_Auto.NAME:    return new Maker.R2k_2k3_Auto(); 
                case Maker.R2k_2k3_AB.NAME:      return new Maker.R2k_2k3_AB();
                case Maker.R2k_2k3_A.NAME:       return new Maker.R2k_2k3_A();
                case Maker.R2k_2k3_B.NAME:       return new Maker.R2k_2k3_B();
                case Maker.VX_Ace_A12.NAME:      return new Maker.VX_Ace_A12();
                case Maker.VX_Ace_A3.NAME:       return new Maker.VX_Ace_A3();
                case Maker.VX_Ace_A4.NAME:       return new Maker.VX_Ace_A4();
                case Maker.VX_Ace_A5.NAME:       return new Maker.VX_Ace_A5();
                case Maker.VX_Ace_BE.NAME:       return new Maker.VX_Ace_BE();
                case Maker.XP_Auto.NAME:         return new Maker.XP_Auto();
                case Maker.XP_Tile.NAME:
                default:                         return new Maker.XP_Tile();
            }
        }
        
        private void Save()
        {
            if (bitmaps == null && tabControl1.SelectedIndex == 0) return;

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            if (saveFileDialog1.FileName == filepath)
            {
                MessageBox.Show(Vocab.GetText("saveErrorMsg"));
                return;
            }

            if (tabControl1.SelectedIndex == 0) SaveConverter();
            else SaveEditor();
        }

        private void OnIndexChange()
        {
            TilesetConverterVX con = new TilesetConverterVX(GetTileset(), (SpriteMode) cbMode.SelectedIndex, checkIgnore.Checked);

            switch (con.SetOutputTileset().TilesetName())
            {
                case Maker.MV_A12.NAME:
                    labelMVTilesetName.Text = "A1-2";
                    break;
                case Maker.MV_A3.NAME:
                    labelMVTilesetName.Text = "A3";
                    break;
                case Maker.MV_A4.NAME:
                    labelMVTilesetName.Text = "A4";
                    break;
                case Maker.MV_A5.NAME:
                    labelMVTilesetName.Text = "A5";
                    break;
                case Maker.MV_BE.NAME:
                    labelMVTilesetName.Text = "B-E";
                    break;
                default:
                    labelMVTilesetName.Text = "Character";
                    break;
            }
        }

        private void FormTilecon_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string extension = System.IO.Path.GetExtension(files[0]).ToLower();

                if (extension == ".jpg" || extension == ".gif" || extension == ".bmp" || extension == ".png" || extension == ".jpeg" ||extension == ".jfif")
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadTilesetByDialog();
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
            ChangeLang(Vocab.lang.en);
        }

        private void portugueseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLang(Vocab.lang.pt);
        }

        private void openTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadTilesetByDialog();
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
        #endregion
        
        #region Editor
        private void UpdateImage()
        {
            currentImage = currentImageRaw;

            if (currentImage != null)
            {
                TilesetConverterBase con;
                ITileset tileset = GetTileset();

                switch (tileset.TilesetName())
                {
                    case Maker.Alpha.NAME:
                        con = new TilesetConverterVerticalApha(tileset, (SpriteMode)cbMode.SelectedIndex, false);
                        break;
                    case Maker.R95.NAME:
                    case Maker.S97.NAME:
                    case Maker.XP_Tile.NAME:
                        con = new TilesetConverterVertical(tileset, (SpriteMode)cbMode.SelectedIndex, false);
                        break;
                    case Maker.XP_Auto.NAME:
                        con = new TilesetConverterAutotileXP(tileset, (SpriteMode)cbMode.SelectedIndex, false);
                        break;
                    case Maker.R2k_2k3_A.NAME:
                    case Maker.R2k_2k3_B.NAME:
                    case Maker.R2k_2k3_AB.NAME:
                    case Maker.R2k_2k3_Auto.NAME:
                        con = new TilesetConverterVerticalRM2K3(tileset, (SpriteMode)cbMode.SelectedIndex, false);
                        break;
                    default:
                        con = new TilesetConverterVX(tileset, (SpriteMode)cbMode.SelectedIndex, false);
                        break;
                }

                currentImage = con.SetModeInSprite(currentImage, Maker.MV_A12.SPRITE_SIZE);
                pictureBoxPreview.Image = currentImage;
            }
        }

        private void SaveEditor()
        {
            List<Bitmap> list = new List<Bitmap>();
            foreach (Button b in outputGrid) list.Add(b.BackgroundImage as Bitmap);

            Bitmap bmp = (new TilesetConverterVertical()).TilesToTileset(list, Maker.MV_A12.SIZE_WIDTH, Maker.MV_A12.SIZE_HEIGHT, Maker.MV_A12.SPRITE_SIZE);
            bmp.Save(saveFileDialog1.FileName);
        }

        private void LoadGrid()
        {
            Image img = Image.FromFile(filepath);
            SetInputGrid(img, GetTileset());

            if (outputGrid == null) SetOutputGrid();
        }

        private void SetInputGrid(Image img, ITileset tileset)
        {
            int spriteSize = tileset.SpriteSize();
            int height = tileset.SizeHeight();
            int width = tileset.SizeWidth();
            if (height == -1) height = img.Height;

            TilesetConverterVerticalRM2K3 con = new TilesetConverterVerticalRM2K3(tileset, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap[] tiles = con.GetSprites(img as Bitmap).ToArray();

            inputPanel.Controls.Clear();
            inputGrid = new List<Button>();
            int i = 0;

            try
            {
                for (int y = 0; y < height; y += spriteSize)
                {
                    for (int x = 0; x < width; i++, x += spriteSize)
                    {
                        Button btn = NewButton(tiles[i], spriteSize);
                        btn.Click += new EventHandler(GetTileInButton);
                        inputGrid.Add(btn);
                        inputPanel.Controls.Add(btn);
                        btn.Location = new Point(x, y);
                    }
                }
            }
            catch (IndexOutOfRangeException) { }
        }

        private Button NewButton(Image img, int spriteSize)
        {
            Button btn = new Button();
            btn.BackColor = Color.Transparent;
            btn.FlatStyle = FlatStyle.Flat;
            btn.ForeColor = Color.Transparent;
            btn.Size = new Size(spriteSize, spriteSize);
            btn.UseVisualStyleBackColor = false;
            btn.BackgroundImage = img;
            btn.BackgroundImageLayout = ImageLayout.Center;
            return btn;
        }

        private void SetOutputGrid()
        {
            TilesetConverterVX con = new TilesetConverterVX(GetTileset(), (SpriteMode)cbMode.SelectedIndex, false);
            var tileset = con.SetOutputTileset();

            int spriteSize = tileset.SpriteSize();
            int height = tileset.SizeHeight();
            int width = tileset.SizeWidth();

            outputPanel.Controls.Clear();
            outputGrid = new List<Button>();

            for (int y = 0, i = 0; y < height; y += spriteSize)
            {
                for (int x = 0; x < width; i++, x += spriteSize)
                {
                    Button btn = NewButton(null, spriteSize);
                    btn.Click += new EventHandler(SetTileInButton);
                    outputGrid.Add(btn);
                    outputPanel.Controls.Add(btn);
                    btn.Location = new Point(x, y);
                }
            }
        }

        private void GetTileInButton(object sender, EventArgs e)
        {
            currentImageRaw = ((Button)sender).BackgroundImage;
            UpdateImage();
        }

        private void SetTileInButton(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = currentImage;
        }

        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void btnSetInput_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btnClearAndSet_Click(object sender, EventArgs e)
        {
            SetOutputGrid();
        }

        private void setTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void clearAndSetOutputTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetOutputGrid();
        }

        private void rPGMakerMVTilesetA12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbOutput.SelectedIndex = 0;
        }

        private void rPGMakerMVTilesetA3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbOutput.SelectedIndex = 1;
        }

        private void rPGMakerMVTilesetA4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbOutput.SelectedIndex = 2;
        }

        private void rPGMakerMVTilesetA5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbOutput.SelectedIndex = 3;
        }

        private void rPGMakerMVTilesetBCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbOutput.SelectedIndex = 4;
        }
        #endregion

        #region Converter
        private void Convert()
        {
            TilesetConverterBase con;
            ITileset tileset = GetTileset();
            btnConvert.Text = Vocab.GetText("wait");

            try
            {
                switch (tileset.TilesetName())
                {
                    case Maker.Alpha.NAME:
                        con = new TilesetConverterVerticalApha(tileset, (SpriteMode)cbMode.SelectedIndex, checkIgnore.Checked);
                        break;

                    case Maker.R95.NAME:
                    case Maker.S97.NAME:
                    case Maker.XP_Tile.NAME:
                        con = new TilesetConverterVertical(tileset, (SpriteMode)cbMode.SelectedIndex, checkIgnore.Checked);
                        break;

                    case Maker.XP_Auto.NAME:
                        con = new TilesetConverterAutotileXP(tileset, (SpriteMode)cbMode.SelectedIndex, checkIgnore.Checked);
                        break;

                    case Maker.R2k_2k3_A.NAME:
                    case Maker.R2k_2k3_B.NAME:
                    case Maker.R2k_2k3_AB.NAME:
                    case Maker.R2k_2k3_Auto.NAME:
                    case Maker.R2k_2k3_AnimObj.NAME:
                        con = new TilesetConverterVerticalRM2K3(tileset, (SpriteMode)cbMode.SelectedIndex, checkIgnore.Checked);
                        break;

                    default:
                        con = new TilesetConverterVX(tileset, (SpriteMode)cbMode.SelectedIndex, checkIgnore.Checked);
                        break;
                }
                bitmaps = con.ConvertToMV(Image.FromFile(filepath));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            if (bitmaps == null)
            {
                btnConvert.Text = Vocab.GetText("converter");
                return;
            }

            pictureBoxOutput.Image = bitmaps[0];
            btnNextImg.Enabled = btnPreviusImg.Enabled = false;
            btnTransparency.Enabled = true;
            setTransparentItem.Enabled = true;

            if (bitmaps.Length > 1)
                btnNextImg.Enabled = btnPreviusImg.Enabled = true;
            else btnNextImg.Enabled = btnPreviusImg.Enabled = false;

            bmpCurrentIndex = 0;
            labelMVPagesNumber.Text = bmpCurrentIndex + 1 + "/" + bitmaps.Length;
            btnConvert.Text = Vocab.GetText("converter");
        }

        private void SaveConverter()
        {
            string dir = saveFileDialog1.FileName;

            if (bitmaps.Length == 1) // one bitmap
            {
                // If the bitmap is a character (player sprite)
                if ((bitmaps[0].Width == 48 && bitmaps[0].Height == 64) || (bitmaps[0].Width == 144 && bitmaps[0].Height == 192))
                    dir = Path.GetDirectoryName(dir) + @"\!$" + Path.GetFileName(dir);
                bitmaps[0].Save(dir);
            }
            else // various bitmaps
            {
                dir = Path.GetDirectoryName(dir) + @"\" + Path.GetFileNameWithoutExtension(dir);
                for (int i = 0; i < bitmaps.Length; i++)
                    bitmaps[i].Save(dir + "_" + (i + 1) + ".png");
            }
        }

        private void SetTransparentPixel()
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < bitmaps.Length; i++)
                    bitmaps[i] = ImageProcessing.ChangePixelsColor(bitmaps[i], colorDialog1.Color);
                pictureBoxOutput.Image = bitmaps[bmpCurrentIndex];
            }
        }

        private void NextImage()
        {
            if (++bmpCurrentIndex >= bitmaps.Length)
                bmpCurrentIndex = 0;

            pictureBoxOutput.Image = bitmaps[bmpCurrentIndex];
            labelMVPagesNumber.Text = bmpCurrentIndex + 1 + "/" + bitmaps.Length;
        }

        private void PreviusImage()
        {
            if (--bmpCurrentIndex < 0)
                bmpCurrentIndex = bitmaps.Length - 1;

            pictureBoxOutput.Image = bitmaps[bmpCurrentIndex];
            labelMVPagesNumber.Text = bmpCurrentIndex + 1 + "/" + bitmaps.Length;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            Convert();
        }

        private void btnNextImg_Click(object sender, EventArgs e)
        {
            NextImage();
        }

        private void btnPreviusImg_Click(object sender, EventArgs e)
        {
            PreviusImage();
        }

        private void btnSetPixelTransparent_Click(object sender, EventArgs e)
        {
            SetTransparentPixel();
        }
        #endregion

    }
}
