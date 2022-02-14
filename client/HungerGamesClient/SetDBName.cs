﻿using System;
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
    public partial class SetDBName : Form
    {
        public SetDBName()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.db_name = textBox1.Text;
            Properties.Settings.Default.Save();
            Cursor.Current = Cursors.WaitCursor;
        }
    }
}
