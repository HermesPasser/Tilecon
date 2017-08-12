using System;
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
        /// <summary>Reference of the object to be called by other forms. </summary>
        public static FormTilecon formTileconController;
       // SpriteMode mode;

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
            ChangeLang(Vocab.lang.eng);
            cbMaker.SelectedIndex = 8;
            cbMode.SelectedIndex = 0;
            btnOpen.Select();
            
            cbOutput.SelectedIndex = 0;
            currentImage = currentImageRaw = null;
        }

        #region General

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
           
            menuStrip1.Items[1].Text = tabConverter.Text = Vocab.convert;
            ignoreItem.Text = Vocab.cbIgnore;
            convertAndSaveItem.Text = Vocab.btnConvert;
            setTransparenItem.Text = Vocab.btnTransparency;

            menuStrip1.Items[2].Text = tabEditor.Text = Vocab.editor;
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
            ITileset tile = GetTileset();
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            var thread = new Thread(() =>
            {
                try
                {
                    TilesetConverterVertical cc = new TilesetConverterVertical(tile, SpriteMode.ALIGN_TOP_LEFT, false);
                    cc.SaveEachSubimage(Image.FromFile(filepath), saveFileDialog1.FileName);
                    MessageBox.Show(Vocab.doneMessage, "Tilecon");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            });
            thread.Start();
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
                MessageBox.Show(Vocab.SaveErrorMessage);
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
        
        private void timer1_Tick(object sender, EventArgs e)
        {

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
            btnConvert.Text = Vocab.waitMessage;

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
                btnConvert.Text = Vocab.btnConvert;
                return;
            }

            pictureBoxOutput.Image = bitmaps[0];
            btnNextImg.Enabled = btnPreviusImg.Enabled = false;
            btnTransparency.Enabled = true;
            setTransparenItem.Enabled = true;

            if (bitmaps.Length > 1)
                btnNextImg.Enabled = btnPreviusImg.Enabled = true;
            else btnNextImg.Enabled = btnPreviusImg.Enabled = false;

            bmpCurrentIndex = 0;
            labelMVPagesNumber.Text = bmpCurrentIndex + 1 + "/" + bitmaps.Length;
            btnConvert.Text = Vocab.btnConvert;
        }

        private void SaveConverter()
        {
            int index = saveFileDialog1.FileName.LastIndexOf(".");
            string fileDir = saveFileDialog1.FileName.Substring(0, index) + "_";

            if (bitmaps.Length != 1)
            {
                for (int i = 0; i < bitmaps.Length; i++)
                    bitmaps[i].Save(fileDir + i + ".png");
            }
            else
            {
                string path = saveFileDialog1.FileName;

                // If the bitmap is a character
                if ((bitmaps[0].Width == 48 && bitmaps[0].Height == 64)  || (bitmaps[0].Width == 144 && bitmaps[0].Height == 192))
                {
                    string nameWithoutPath = path.Substring(path.LastIndexOf("\\") + 1, path.Length - (path.LastIndexOf("\\") + 1));
                    path = path.Replace(nameWithoutPath, @"\!$" + nameWithoutPath);
                }
                
                bitmaps[0].Save(path);
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