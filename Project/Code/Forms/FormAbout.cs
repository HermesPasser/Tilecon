using System.Windows.Forms;

namespace tilecon
{
    /// <summary>Form of about with contact information.</summary>
    public partial class FormAbout : Form
    {
        /// <summary>Default constructor.</summary>
        public FormAbout()
        {
            InitializeComponent();
            labelTitle.Text = "Tileset Converter MV" + Vocab.version;
            labelAbout.Text = Vocab.aboutHelpText;
            linkLabel2.Text = Vocab.GetText("sourceCode");
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
            System.Diagnostics.Process.Start("https://hermespasser.github.io/pages/tilecon.html");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/HermesPasser/Tilecon");
        }
    }
}
