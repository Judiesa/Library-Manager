using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Project
{
    public partial class AddMemberfrm : Form
    {
        string localPath;
        public AddMemberfrm()
        {
            InitializeComponent();
        }

        private void btnChooseFile_Click_1(object sender, EventArgs e)
        {
            //select the excel file and save the path of the excel file
            openFileDialog1.Title = "Choose Excel file";
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.localPath = openFileDialog1.FileName;
            }
            btnAddMember.Enabled = true;
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            comboStatus.Enabled = true;
        }

        private void btnAddMember_Click_1(object sender, EventArgs e)
        {
            //find the id for recording the new member in library and show it
            int idMember = UserMethods.FindTheLastId(this.localPath) + 1;
            if (comboStatus.Text == "none" || comboStatus.Text == null || comboStatus.Text == "")
            {
                MessageBox.Show("please entre the status field correctly", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                //make an object of class book
                Member user = new Member(idMember, txtFirstName.Text, txtLastName.Text, comboStatus.Text, double.Parse(txtPoint.Text));
                //check that the fields have valid value
                for (int i = 1; i < user.MemberInfo.Length - 1; i++)
                {
                    for (int j = 0; j < user.MemberInfo[i].Length; j++)
                    {
                        if (!Char.IsLetter(user.MemberInfo[i][j]))
                        {
                            MessageBox.Show("invalid data" + "\nplease try again and fill the fields correctly", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtFirstName.Clear();
                            txtLastName.Clear();
                            return;
                        }
                    }
                }
                //add new book to excel file 
                UserMethods.AddMemberToExcel(this.localPath, user);
                txtId.Text = Convert.ToString(idMember);
                labelID.Visible = true;
                txtId.Visible = true;
                MessageBox.Show("member Info successfully added to Excel" + "\nyour new member ID is" + Convert.ToString(user.MemberId), "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("please a enter number in id field", "invalid value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Clear();
                txtId.Enabled = true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Clear();
                txtLastName.Clear();
                comboStatus.Text = "none";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
