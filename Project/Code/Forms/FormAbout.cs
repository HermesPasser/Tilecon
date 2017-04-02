using System.Windows.Forms;

namespace tilecon
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            label1.Text = Vocab.aboutHelpText;
            label2.Text += " " + Vocab.version;
        }

        private void OnClose(object sender, FormClosingEventArgs e)
        {
            FormXpMv.xpmvController.Enabled = true;
            FormXpMv.xpmvController.Focus();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
