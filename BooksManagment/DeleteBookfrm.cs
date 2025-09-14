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
    public partial class DeleteBookfrm : Form
    {
        string localPath;
        public DeleteBookfrm()
        {
            InitializeComponent();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            //select the excel file and save the path of the excel file
            openFileDialog1.Title = "Choose Excel file";
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.localPath = openFileDialog1.FileName;
            }
            txtId.Enabled = true;
            btnDeleteBook.Enabled = true;
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            //delete the book with the given id
            try
            {
                //check validation
                if (txtId.Text == null || txtId.Text == "") 
                {
                    MessageBox.Show("please a filled up field correctly", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtId.Clear();
                }
                for (int i = 0; i < txtId.Text.Length; i++)
                {
                    if (!char.IsLetterOrDigit(txtId.Text[i])) 
                    {
                        MessageBox.Show("please a filled up field correctly, filed just should includ letter or digit ", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtId.Clear();
                    }
                }
                //all books
                var allBooks = BookMethods.ReadBookFromExcel(this.localPath);
                //check that the id that entered is exist or not
                Book book = allBooks
                    .Where(x => x.BookName == txtId.Text)
                    .Select(x => x).FirstOrDefault();
                if (book != null) 
                {
                    //delete the selected book from excel file
                    BookMethods.DeleteBook(localPath, book);
                    MessageBox.Show("book with given Id is deleted succsssfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //the id that entered was not exsit in excel file
                else
                {
                    MessageBox.Show("the book name that is entered is not exsit in the choosen excel file" + "\nplease enter a valid Id", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
