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
            linkLabel1.Text = Vocab.GetText("projectPage");
            linkLabel2.Text = Vocab.GetText("sourceCode");
        }

        private void OnClose(object sender, FormClosingEventArgs e)
        {
            FormTilecon.controller.Enabled = true;
            FormTilecon.controller.Focus();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://hermespasser.github.io/p/tilecon.html");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/HermesPasser/Tilecon");
        }
    }
}
