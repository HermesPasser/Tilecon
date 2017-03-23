using System;
using System.Drawing;
using System.Windows.Forms;

namespace tilecon
{
    public partial class FormXpMv : Form
    {
        private string filepath;
        private bool filepathExists;

        public static FormXpMv xpmvController;

        public FormXpMv()
        {
            xpmvController = this;
            InitializeComponent();
            btnCutSave.Enabled = false;
            btnConvert.Enabled = false;
            changeLang(Vocab.lang.eng);
        }

        private void changeLang(Vocab.lang l)
        {
            Vocab.changeLang(l);
            saveFileDialog1.Filter = Vocab.pgnFilesText + " (*.png) | *.png";
            openFileDialog1.Filter = Vocab.imageFilesText + " (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.webp) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.webp";
            btnConvert.Text = Vocab.btnConvert;
            btnCutSave.Text = Vocab.btnCut;
            btnSearch.Text = Vocab.btnSearch;

            menuStrip1.Items[0].Text = Vocab.archiche;
            exitToolStripMenuItem.Text = Vocab.archiveExit;

            menuStrip1.Items[1].Text = Vocab.convert;
            cutsaveIndividualFramesToolStripMenuItem.Text = Vocab.btnCut;
            convertAndSaveToolStripMenuItem.Text = Vocab.btnConvert;

            menuStrip1.Items[2].Text = Vocab.language;
            englishToolStripMenuItem.Text = Vocab.languageEng;
            portugueseToolStripMenuItem.Text = Vocab.languagePtbr;

            menuStrip1.Items[3].Text = Vocab.help;
            aboutToolStripMenuItem.Text = Vocab.helpAbout;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            Convert();
        }

        private bool Search()
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
            Search();
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
            //MessageBox.Show(Vocab.aboutHelpText, "Tilecon");
        }

        private void CutSave()
        {
            ImageCrop.SaveEachSubimage(Image.FromFile(filepath), filepath);
            MessageBox.Show(Vocab.doneMessage, "Tilecon");
        }

        private void cutsaveIndividualFramesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filepath == null) 
            {
                filepathExists = Search();
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
                filepathExists = Search();
                if (filepathExists)
                {
                    convertAndSaveToolStripMenuItem_Click(sender, e);
                    return;
                }
            }
            if (filepathExists)
                Convert();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLang(Vocab.lang.eng);
        }

        private void portugueseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLang(Vocab.lang.ptbr);
        }
    }
}