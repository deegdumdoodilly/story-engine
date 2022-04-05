using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HungerGamesClient
{
    public partial class StatusDialog : Form
    {
        public StatusDialog()
        {
            InitializeComponent();
        }

        private void StatusDialog_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditSim.statusListBoxRef.Items.Add(textBox1.Text);
            this.Close();
        }
    }
}
