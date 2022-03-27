using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HungerGamesClient
{
    public partial class EditSim : Form
    {
        public EditSim()
        {
            InitializeComponent();
        }

        private void EditSim_Load(object sender, EventArgs e)
        {
            MainForm.reference.Cursor = Cursors.Default;
            MainForm.reference.editSimButtonRef.Enabled = true;
        }
    }
}
