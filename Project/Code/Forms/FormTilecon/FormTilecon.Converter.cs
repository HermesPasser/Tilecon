using System;
using System.Drawing;
using System.Windows.Forms;
using tilecon.Converter;

namespace tilecon
{
    public partial class FormTilecon : Form
    {
        private Bitmap[] bitmaps;
        private int bmpCurrentIndex;

        private void Convert()
        {
            TilesetConverterBase con;
            Maker.Tileset maker = GetTileset();
            SpriteMode mode = GetMode();
            btnConvert.Text = Vocab.waitMessage;

            try
            {
                switch (maker)
                {
                    case Maker.Tileset.Alpha:
                        con = new TilesetConverterVerticalApha(maker, mode, checkIgnore.Checked);
                        break;

                    case Maker.Tileset.R95:
                    case Maker.Tileset.S97:
                    case Maker.Tileset.XP:
                        con = new TilesetConverterVertical(maker, mode, checkIgnore.Checked);
                        break;

                    case Maker.Tileset.XP_Auto:
                        con = new TilesetConverterAutotileXP(maker, mode, checkIgnore.Checked);
                        break;

                    case Maker.Tileset.R2000_2003_A:
                    case Maker.Tileset.R2000_2003_B:
                    case Maker.Tileset.R2000_2003_AB:
                    case Maker.Tileset.R2000_2003_Auto:
                        con = new TilesetConverterVerticalRM2K3(maker, mode, checkIgnore.Checked);
                        break;

                    default:
                        con = new TilesetConverterVX(maker, mode, checkIgnore.Checked);
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
            else bitmaps[0].Save(saveFileDialog1.FileName);
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
            bmpCurrentIndex++;
            if (bmpCurrentIndex >= bitmaps.Length)
                bmpCurrentIndex = 0;

            pictureBoxOutput.Image = bitmaps[bmpCurrentIndex];
            labelMVPagesNumber.Text = bmpCurrentIndex + 1 + "/" + bitmaps.Length;
        }

        private void PreviusImage()
        {
            bmpCurrentIndex--;
            if (bmpCurrentIndex < 0)
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
    }
}
