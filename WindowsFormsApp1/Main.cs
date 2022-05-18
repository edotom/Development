using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocal nForm = new frmLocal();
            nForm.TopLevel = true;
            nForm.Show();
        }

        private void remoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRemote nForm = new frmRemote();
            nForm.TopLevel = true;
            nForm.Show();
        }
    }
}
