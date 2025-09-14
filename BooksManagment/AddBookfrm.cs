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
    public partial class AddBookfrm : Form
    {
        string localPath;
        public AddBookfrm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
            btnAddBook.Enabled = true;
            txtAuthor.Enabled = true;
            txtGenre.Enabled = true;
            txtName.Enabled = true;
            comboStatus.Enabled = true;
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            try
            {
                //find the id for recording the new book in library and show it
                int idBook = BookMethods.FindTheLastId(this.localPath) + 1;
                //check the fields have been fill correctly
                if (comboStatus.Text == "none" || comboStatus.Text == null || comboStatus.Text == "")
                {
                    MessageBox.Show("please entre the status field correctly", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //make an object of class book
                Book book = new Book(idBook, txtName.Text, txtAuthor.Text, txtGenre.Text, comboStatus.Text);
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
                //add new book to excel file
                BookMethods.AddBookToExcel(this.localPath, book);
                txtId.Text = Convert.ToString(book.BookId);
                labelID.Visible = true;
                txtId.Visible = true;
                MessageBox.Show("book Info successfully added to Excel" + "\nyour new book ID is" + Convert.ToString(book.BookId), "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtName.Clear();
                txtAuthor.Clear();
                comboStatus.Text = "none";
                txtGenre.Clear();
            }
        }
    }
}
