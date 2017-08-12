using System.Windows.Forms;

namespace tilecon
{
    /// <summary>Form to explains how autotile xp conversion works.</summary>
    public partial class FormXPAuto : Form
    {
        /// <summary>Default constructor.</summary>
        public FormXPAuto()
        {
            InitializeComponent();
        }

        private void FormXPAuto_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormTilecon.formTileconController.Enabled = true;
            FormTilecon.formTileconController.Focus();
        }
    }
}
