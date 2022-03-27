using System;
using System.Windows.Forms;

namespace HungerGamesClient
{
    public partial class SetURL : Form
    {
        public SetURL()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.api_url = textBox1.Text;
            Properties.Settings.Default.Save();
            Cursor.Current = Cursors.WaitCursor;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void SetURL_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.api_url.Length > 0)
               textBox1.Text = Properties.Settings.Default.api_url;
        }
    }
}
