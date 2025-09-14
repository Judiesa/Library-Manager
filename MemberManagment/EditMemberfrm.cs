using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class EditMemberfrm : Form
    {
        string localPath;
        public EditMemberfrm()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click_1(object sender, EventArgs e)
        {
            //select the excel file and save the path of the excel file
            openFileDialog1.Title = "Choose Excel file";
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.localPath = openFileDialog1.FileName;
            }
            txtId.Enabled = true;
            btnOpenFile.Enabled = false;
            btnSearch.Enabled = true;
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            //search the member with the given id and show the member information in the form
            try
            {
                txtId.Enabled = false;
                //make an object of class book
                Member user = new Member(int.Parse(txtId.Text));
                //check that the id that entered is exist or not
                if (UserMethods.ExistMemberId(localPath, user))
                {
                    //get member info by given id and allow to user to change the info in textbox
                    user = UserMethods.GetMemberInfo(localPath, user.MemberId);
                    txtFirstName.Text = user.FirstName;
                    txtLastName.Text = user.LastName;
                    txtStatus.Text = user.Status;
                    txtPoint.Text = Convert.ToString(user.MemberPoint);
                    txtFirstName.Enabled = true;
                    txtLastName.Enabled = true;
                    txtStatus.Enabled = false;
                    btnSearch.Enabled = false;
                    btnApply.Enabled = true;
                    MessageBox.Show("now you can change the info", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                //the id that entered was not exsit in excel file
                {
                    MessageBox.Show("the ID that you enterd is not exsit in the choosen excel file" + "\n please enter a valid ID", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtId.Clear(); txtId.Enabled = true;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("please a enter number in id field", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Clear();
                txtId.Enabled = true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "error" + "/nplease try again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Clear();
                btnOpenFile.Enabled = true;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //save the change of the member info in excel file with new entered details
            try
            {
                //make an object of class book
                Member user = new Member(int.Parse(txtId.Text), txtFirstName.Text, txtLastName.Text, txtStatus.Text, double.Parse(txtPoint.Text));
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
                //save the changes book info in excel file
                UserMethods.ApplyMemberChanges(localPath, user);
                MessageBox.Show("the changes applied", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
