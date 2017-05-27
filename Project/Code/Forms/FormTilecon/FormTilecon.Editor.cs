using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using tilecon.Converter;

namespace tilecon
{
    public partial class FormTilecon : Form
    {
        List<Button> inputGrid;
        List<Button> outputGrid;
        Image currentImageRaw;
        Image currentImage;
        SpriteMode mode; // isso pode ir para a parte principal da classe, ou retirar e deixar modes locais

        private void UpdateImage()
        {
            currentImage = currentImageRaw;

            if (currentImage != null)
            {
                TilesetConverterBase con;
                Maker.Tileset maker = GetTileset();
                mode = GetMode();

                switch (maker)
                {
                    case Maker.Tileset.Alpha:
                        con = new TilesetConverterVerticalApha(maker, mode, false);
                        break;
                    case Maker.Tileset.R95:
                    case Maker.Tileset.S97:
                    case Maker.Tileset.XP:
                        con = new TilesetConverterVertical(maker, mode, false);
                        break;
                    case Maker.Tileset.XP_Auto:
                        con = new TilesetConverterAutotileXP(maker, mode, false);
                        break;
                    case Maker.Tileset.R2000_2003_A:
                    case Maker.Tileset.R2000_2003_B:
                    case Maker.Tileset.R2000_2003_AB:
                    case Maker.Tileset.R2000_2003_Auto:
                        con = new TilesetConverterVerticalRM2K3(maker, mode, false);
                        break;
                    default:
                        con = new TilesetConverterVX(maker, mode, false);
                        break;
                }

                currentImage = con.SetModeInSprite(currentImage, Maker.MV.SPRITE_SIZE);
                pictureBoxPreview.Image = currentImage;
            }
        }

        private void SaveEditor()
        {
            List<Bitmap> list = new List<Bitmap>();
            foreach (Button b in outputGrid)
                list.Add(b.BackgroundImage as Bitmap);

            TilesetConverterVertical con = new TilesetConverterVertical();
            Bitmap bmp = con.TilesToTileset(list, Maker.MV.A12.SIZE_WIDTH, Maker.MV.A12.SIZE_HEIGHT, Maker.MV.SPRITE_SIZE);
            bmp.Save(saveFileDialog1.FileName);
        }

        private void LoadGrid()
        {
            Maker.Tileset toBeMaker = GetTileset();
            Image img = Image.FromFile(filepath);
            SetInputGrid(img, toBeMaker);

            if (outputGrid == null) SetOutputGrid();
        }

        private void SetInputGrid(Image img, Maker.Tileset tileset)
        {
            int spriteSize = Maker.GetSpriteSize(tileset);
            int width = Maker.GetSizeWidth(tileset);
            int height = Maker.GetSizeWidth(tileset);
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
            TilesetConverterVX con = new TilesetConverterVX(GetTileset(), GetMode(), false);
            var tileset = con.GetOutputMaker();

            int spriteSize = Maker.GetSpriteSize(tileset);
            int height = Maker.GetSizeHeight(tileset);
            int width = Maker.GetSizeWidth(tileset);

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
    }
}
