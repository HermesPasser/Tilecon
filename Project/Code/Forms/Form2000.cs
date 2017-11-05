using System.Windows.Forms;

namespace tilecon
{
    /// <summary>Form to explains how 2000 conversion works.</summary>
    public partial class Form2000 : Form
    {
        /// <summary>Default constructor.</summary>
        public Form2000()
        {
            InitializeComponent();
        }

        private void Form2000_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormTilecon.controller.Enabled = true;
            FormTilecon.controller.Focus();
        }
    }
}
