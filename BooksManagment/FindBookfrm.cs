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
    public partial class FindBookfrm : Form
    {
        string localPath;
        public FindBookfrm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            //clear the dataGrid for new file info
            dataGridView1.Rows.Clear();
            try
            {
                //select the excel file and save the path of file
                openFileDialog1.Title = "Choose Excel file";
                openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.localPath = openFileDialog1.FileName;
                }
                if (this.localPath != null || this.localPath != "")
                {
                    //show the excel file in form by book objects
                    List<Book> books = BookMethods.ShowExcelBooks(this.localPath);
                    for (int i = 0; i < books.Count; i++)
                    {
                        dataGridView1.Rows.Add(books[i].BookId, books[i].BookName, books[i].Author, books[i].Genre, books[i].Status);
                    }
                    btnFindBook.Enabled = true;
                    combSearch.Enabled = true;
                    txtSearch.Enabled = true;
                }
                else
                {
                    MessageBox.Show("please choose a file", "file not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nplease choose the file again", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFindBook_Click(object sender, EventArgs e)
        {
            //filter the excel result based on wanted item
            try
            {
                txtSearch.Enabled = false;
                combSearch.Enabled = false;
                btnFindBook.Enabled = false;
                //check the boxes be valid
                if (txtSearch.Text == null || txtSearch.Text == "")
                {
                    MessageBox.Show("please fill the search fileds correctly", "textBox field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSearch.Clear();
                    txtSearch.Enabled = true;
                    combSearch.Enabled = true;
                    btnFindBook.Enabled = true;
                    return;
                }
                if (combSearch.Text == null || combSearch.Text == "none" || combSearch.Text == "")
                {
                    MessageBox.Show("please fill the search fileds correctly", "status field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    combSearch.Text = "none";
                    txtSearch.Enabled = true;
                    combSearch.Enabled = true;
                    btnFindBook.Enabled = true;
                    return;
                }
                //clear the dataGrid for new search result
                dataGridView1.Rows.Clear();
                //filter the book objects based on wanted item     
                List<Book> filteredBooks = BookMethods.FilterExcelBooks(this.localPath, txtSearch.Text, combSearch.Text);
                for (int i = 0; i < filteredBooks.Count; i++)
                {
                    //check if the book object is null or not
                    if (filteredBooks[i].BookId != null)
                    {
                        dataGridView1.Rows.Add(filteredBooks[i].BookId, filteredBooks[i].BookName, filteredBooks[i].Author, filteredBooks[i].Genre, filteredBooks[i].Status);
                    }
                    else
                    {
                        continue;
                    }
                }
                //check that any book object has found with wanted items or not
                if (dataGridView1.Rows.Count == 0)
                    MessageBox.Show("no match found", "no match found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSearch.Enabled = true;
                combSearch.Enabled = true;
                btnFindBook.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearch.Enabled = true;
                txtSearch.Clear();
                combSearch.Enabled = true;
                btnFindBook.Enabled = true;
            }
        }
    }
}