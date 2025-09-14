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
    public partial class EditBookfrm : Form
    {
        string localPath;
        public EditBookfrm()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            //select the excel file and save the path of the excel file
            openFileDialog1.Title = "Choose Excel file";
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.localPath = openFileDialog1.FileName;
            }
            txtId.Enabled = true;
            btnSearch.Enabled = true;
            btnOpenFile.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //search the book with the given id and show the book information in the form
            try
            {
                txtId.Enabled = false;
                //make an object of class book
                Book wantedBook = new Book(int.Parse(txtId.Text));
                //check that the id that entered is exist or not
                if (BookMethods.ExistBookId(this.localPath, wantedBook)) 
                {
                    //get book info by given id and allow to user to change the info in textbox
                    wantedBook = BookMethods.GetBookInfo(localPath, wantedBook.BookId);
                    txtName.Text = wantedBook.BookName;
                    txtAuthor.Text = wantedBook.Author;
                    txtGenre.Text = wantedBook.Genre;
                    txtStatus.Text = wantedBook.Status;
                    txtName.Enabled = true;
                    txtAuthor.Enabled = true;
                    txtGenre.Enabled = true;
                    btnSearch.Enabled = false;
                    btnApply.Enabled = true;
                    MessageBox.Show("now you can change the info", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //the id that entered was not exsit in excel file
                else
                {
                    MessageBox.Show("the ID that you enterd is not exsit in the choosen excel file" + "\nplease enter a valid ID", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtId.Clear();
                    txtId.Enabled = true;
                    btnOpenFile.Enabled = true;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("please a enter number in id field", "invalid value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Clear();
                txtId.Enabled = true;
                btnOpenFile.Enabled = true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "error" + "\nplease try again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Clear();
                txtId.Enabled = true;
                btnOpenFile.Enabled = true;
                btnOpenFile.Enabled = true;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //save the change of the book info in excel file with new entered details
            try
            {
                //make an object of class book
                Book book = new Book(int.Parse(txtId.Text), txtName.Text, txtAuthor.Text, txtGenre.Text, txtStatus.Text);
                //check that the fields have valid value
                for (int i = 1; i < book.BookInfo.Length - 1; i++)
                {
                    for (int j = 0; j < book.BookInfo[i].Length; j++)
                    {
                        if (!Char.IsLetter(book.BookInfo[i][j]) && i != 1)
                        {
                            MessageBox.Show("invalid data" + "\nplease try again and fill the fields correctly", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtAuthor.Clear();
                            txtGenre.Clear();
                            return;
                        }
                        else if (!Char.IsLetterOrDigit(book.BookInfo[i][j]) && i == 1) 
                        {
                            MessageBox.Show("invalid data" + "\nplease try again and fill the fields correctly", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtName.Clear();
                            return;
                        }
                    }
                }
                //save the changes book info in excel file
                BookMethods.ApplyBookChanges(localPath, book);
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
