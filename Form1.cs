using System;
using System.Drawing;
using System.Windows.Forms;

namespace tilecon
{
    public partial class Form1 : Form
    {
        private string filepath;
        private bool filepathExists;
        public Form1()
        {
            InitializeComponent();
            btnCutSave.Enabled = false;
            btnConvert.Enabled = false;
            saveFileDialog1.Filter = "Png files (*.png) | *.png";
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.webp) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.webp";
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            Convert();
        }

        private bool search()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                btnConvert.Enabled = true;
                btnCutSave.Enabled = true;
                filepathExists = true;
                filepath = openFileDialog1.FileName;
                pictureBoxXP.Image = Image.FromFile(filepath);
                return true;
            }
            return false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
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
            MessageBox.Show("Tileset XP to MV Converter (tilecon)\n\nby Hermes Passer\ngladiocitrico.blogspot.com", "Tilecon");
        }

        private void CutSave()
        {
            ImageCrop.SaveEachSubimage(Image.FromFile(filepath), filepath);
            MessageBox.Show("Done", "Tilecon");
        }

        private void cutsaveIndividualFramesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filepath == null) 
            {
                filepathExists = search();
                if (filepathExists)
                {
                    cutsaveIndividualFramesToolStripMenuItem_Click(sender, e);
                    return;
                }
            }
            if (filepathExists)
                CutSave();
        }

        private void Convert()
        {
            Bitmap bitmap = ImageCrop.ConvertToMV(Image.FromFile(filepath));
            pictureBoxXP.Image = bitmap;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmap.Save(saveFileDialog1.FileName);
            }
        }

        private void convertAndSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filepath == null)
            {
                filepathExists = search();
                if (filepathExists)
                {
                    convertAndSaveToolStripMenuItem_Click(sender, e);
                    return;
                }
            }
            if (filepathExists)
                Convert();
        }
    }
}