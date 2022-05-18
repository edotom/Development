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
    public partial class frmRemote : Form
    {
        public frmRemote()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ASGitHub.ASGitRepository oGit = new ASGitHub.ASGitRepository(txtLocalRepository.Text);
            //oGit.ChangeRemote(txtRemoteRepoCollection.Text, txtLocalRepository.Text);
            oGit.PushChanges(txtUserName.Text, txtUserPassword.Text, txtUserEmail.Text, txtLocalRepository.Text, txtRemoteRepoCollection.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ASGitHub.ASGitRepository oGit = new ASGitHub.ASGitRepository(txtLocalRepository.Text);
            oGit.Clone(txtTargetDirectory.Text, txtLocalRepository.Text, txtRemoteRepoCollection.Text);
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            ASGitHub.ASGitRepository oGit = new ASGitHub.ASGitRepository(txtLocalRepository.Text);
            oGit.CheckoutChanges(txtUserName.Text, txtUserPassword.Text, txtUserEmail.Text, txtLocalRepository.Text, txtRemoteRepoCollection.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ASGitHub.ASGitRepository oGit = new ASGitHub.ASGitRepository(txtLocalRepository.Text);
            oGit.FecthChanges(txtUserName.Text, txtUserPassword.Text, txtUserEmail.Text, txtLocalRepository.Text, txtRemoteRepoCollection.Text);
        }
    }
}
