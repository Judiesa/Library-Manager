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
    public partial class DetermineStatusAndDeleteMemberfrm : Form
    {
        string localPath;
        public DetermineStatusAndDeleteMemberfrm()
        {
            InitializeComponent();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            btnStatus.Enabled = false;
            btnDeleteMember.Enabled = false;
            txtStatus.Clear();
            //select the excel file and save the path of the excel file
            openFileDialog1.Title = "Choose Excel file";
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.localPath = openFileDialog1.FileName;
            }
            txtId.Enabled = true;
            btnDeleteMember.Enabled = true;
            btnStatus.Enabled = true;
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            btnStatus.Enabled = false;
            try
            {
                ////make an object of class member
                Member user = new Member(int.Parse(txtId.Text));
                ////check that the id that entered is exist or not
                if (UserMethods.ExistMemberId(localPath, user)) 
                {
                    //delete the member with the given id
                    UserMethods.DeleteMember(localPath, user);
                    MessageBox.Show("book with given Id is deleted succsssfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //the id that entered was not exsit in excel file
                else
                {
                    MessageBox.Show("the Id that you enter is not exsit in the choosen excel file"+"\nplease enter a valid Id", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtId.Clear();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("please a enter number in id field", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Clear();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Clear();
                btnDeleteMember.Enabled = false;
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            //detemining the status of the member with the given id
            btnDeleteMember.Enabled = false;
            try
            {
                //make an object of class member
                Member user = new Member(int.Parse(txtId.Text));
                //check that the id that entered is exist or not
                if (UserMethods.ExistMemberId(localPath, user)) 
                {
                    txtStatus.Text = UserMethods.StatusOfMember(localPath, user);
                }
                //the id that entered was not exsit in excel file
                else
                {
                    MessageBox.Show("the ID that you enter is not exsit in the choosen excel file \nplease enter again", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtId.Clear();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Clear();
                btnDeleteMember.Enabled = false;
                btnStatus.Enabled = false;
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}