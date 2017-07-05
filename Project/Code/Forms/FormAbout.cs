using System.Windows.Forms;

namespace tilecon
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            labelTitle.Text = "Tileset Converter to MV" + Vocab.version;
            labelAbout.Text = Vocab.aboutHelpText;
        }

        private void OnClose(object sender, FormClosingEventArgs e)
        {
            FormTilecon.formTileconController.Enabled = true;
            FormTilecon.formTileconController.Focus();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gladiocitrico.blogspot.com");
        }
    }
}
