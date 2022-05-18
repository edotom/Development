using ASGitHub;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibGit2Sharp;

namespace WindowsFormsApp1
{
    public partial class frmLocal : Form
    {
        private ASGitRepository oGit;
        List<string> lstFileNames;
        public frmLocal()
        {
            InitializeComponent();
            if (string.IsNullOrWhiteSpace(txtRepoCollection.Text))
            {
                MessageBox.Show("Please select Local Repository");
                folderBrowserDialog1.ShowDialog();
                txtRepoCollection.Text = folderBrowserDialog1.SelectedPath;
                
            }
            oGit = new ASGitRepository(txtRepoCollection.Text);
            lstFileNames = new List<string>();
        }

        private void button1_Click(object sender, EventArgs e)
        {                        
            oGit.GitInit();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var commits = oGit.GetCommits().ToList();
                dataGridView1.DataSource = commits;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            //foreach (Commit item in commits)
            //{
            //    listBox2.Items.Add(string.Format("id: {0} creator: {1} tag: {2}", item.Id, item.Author, item.Message));
            //}
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var entries = oGit.GetLogForFile("New Text Document.txt");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.ShowDialog();            
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            foreach(string files in openFileDialog1.FileNames)
            {
                lstFileNames.Add(files);
                listBox1.Items.Add(files);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lblResult.Text = oGit.CommitFile(txtComment.Text, txtUserName.Text, txtUserEmail.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtRepoCollection.Text))
            {
                foreach (string fileName in listBox1.Items)
                {
                    lblResult.Text = oGit.AddSingleFileToRepo(fileName);
                }
            }
            else 
            {
                MessageBox.Show("Please select Local Repository");
                folderBrowserDialog1.ShowDialog();
                txtRepoCollection.Text = folderBrowserDialog1.SelectedPath;
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //folderBrowserDialog1.ShowDialog();
            
            //txtTargetDirectory.Text = folderBrowserDialog1.SelectedPath;

            //string id = string.Empty;
            //Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            //if (selectedRowCount > 0)
            //{
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //    for (int i = 0; i < selectedRowCount; i++)
            //    {
            //        id = dataGridView1.SelectedRows[i].Cells[8].Value.ToString();                    
            //    }                
            //    MessageBox.Show(id);
            //    oGit.GetRepository(txtTargetDirectory.Text);
            //    //oGit.RestoreFile(txtTargetDirectory.Text, "POIE.txt", id);
            //}
            
        }
    }
}
