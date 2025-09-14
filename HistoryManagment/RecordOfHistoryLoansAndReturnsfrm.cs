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
    public partial class RecordOfHistoryLoansAndReturnsfrm : Form
    {
        string localPath = @"D:\New folder\ExcelMemberFiles\ex.xlsx";
        public RecordOfHistoryLoansAndReturnsfrm()
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
                    btnShow.Enabled = true;
                    txtSearch.Enabled = true;
                    comboSearch.Enabled = true;
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

        private void btnShow_Click_1(object sender, EventArgs e)
        {
            //filter the excel result based on wanted item
            try
            {
                txtSearch.Enabled = false;
                comboSearch.Enabled = false;
                //check the boxes be valid
                if (txtSearch.Text == null || txtSearch.Text == "")
                {
                    MessageBox.Show("please fill the search fileds correctly", "invalid textBox field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSearch.Clear();
                    txtSearch.Enabled = true;
                    comboSearch.Enabled = true;
                    return;
                }
                if (comboSearch.Text == null || comboSearch.Text == "none" || comboSearch.Text == "")
                {
                    MessageBox.Show("please fill the search fileds correctly", "invalid status field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboSearch.Text = "none";
                    txtSearch.Enabled = true;
                    comboSearch.Enabled = true;
                    return;
                }
                //clear the dataGrid for new search result
                dataGridView1.Rows.Clear();
                //filter the loan objects based on wanted item 
                var libraryBooks = BookMethods.ReadBookFromExcel(this.localPath);
                if (comboSearch.Text == "Book Name")
                {
                    var filterByBookName = libraryBooks
                        .Where(x => x.BookName == txtSearch.Text).ToArray();
                    //if book name does not exist in library
                    if (filterByBookName == null)
                    {
                        MessageBox.Show("the entered book Name does not exist in book library" + "\nplease try again", "invalid book name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        comboSearch.Text = "none";
                        txtSearch.Clear();
                        txtSearch.Enabled = true;
                        comboSearch.Enabled = true;
                    }
                    else
                    {
                        //find loans that match with this book id
                        var listLoans = LoanMethods.ReadLoansFromExcel(this.localPath);
                        List<Loan> filteredLoans = new List<Loan>();
                        for (int i = 0; i < listLoans.Count(); i++)
                        {
                            for (int j = 0; j < filterByBookName.Count(); j++)
                            {
                                if (listLoans[i].BookId == filterByBookName[j].BookId)
                                {
                                    filteredLoans.Add(listLoans[i]);
                                }
                            }
                        }
                        // if the book has never borrowed
                        if (!filteredLoans.Any())
                        {
                            MessageBox.Show("the entered Book Name has never been borrowed yet" + "\nyou can not record books with no history in loans", "book has not been borrowed yet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtSearch.Enabled = true;
                            comboSearch.Enabled = true;
                        }
                        else
                        {
                            //show the datas
                            foreach (var loan in filteredLoans)
                            {
                                dataGridView1.Rows.Add(loan.LoanId, loan.MemberId, loan.BookId, loan.IssueDate, loan.ReturnDate, loan.Status);
                            }
                            MessageBox.Show("the filtered applied" + "\nyou can choose thr loans to record them in excel", "changed applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnSave.Enabled = true;
                        }
                    }
                }
                else if (comboSearch.Text == "Book Author Name")
                {
                    var filterByAuthorkName = libraryBooks
                        .Where(x => x.Author == txtSearch.Text).ToArray();
                    //if author name was not exist in library
                    if (filterByAuthorkName == null)
                    {
                        MessageBox.Show("the entered book author name does not exist in book library" + "\nplease try again", "invalid book name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        comboSearch.Text = "none";
                        txtSearch.Clear();
                        txtSearch.Enabled = true;
                        comboSearch.Enabled = true;
                    }
                    else
                    {
                        //find loans that match with this book id
                        var listLoans = LoanMethods.ReadLoansFromExcel(this.localPath);
                        List<Loan> filteredLoans = new List<Loan>();
                        for (int i = 0; i < listLoans.Count(); i++)
                        {
                            for (int j = 0; j < filterByAuthorkName.Count(); j++)
                            {
                                if (listLoans[i].BookId == filterByAuthorkName[j].BookId)
                                {
                                    filteredLoans.Add(listLoans[i]);
                                }
                            }
                        }
                        // if the book has never borrowed
                        if (!filteredLoans.Any())
                        {
                            MessageBox.Show("the entered Book Author Name is not exist in loan list this book has never been borrowed yet" + "\nyou can not record books with no history in loans", "book has not been borrowed yet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtSearch.Enabled = true;
                            comboSearch.Enabled = true;
                        }
                        else
                        {
                            //show the datas
                            foreach (var loan in filteredLoans)
                            {
                                dataGridView1.Rows.Add(loan.LoanId, loan.MemberId, loan.BookId, loan.IssueDate, loan.ReturnDate, loan.Status);
                            }
                            MessageBox.Show("the filtered applied" + "\nyou can choose thr loans to record them in excel", "changed applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnSave.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboSearch.Text = "none";
                txtSearch.Clear();
                txtSearch.Enabled = true;
                comboSearch.Enabled = true;
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            //save the records in history
            try
            {
                //a list for saving the loans that should record in history
                List<Loan> selectedLoans = new List<Loan>();
                //read the rows in datagrid
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(r.Cells[6].Value) == true)
                    {
                        Loan loan = new Loan(Convert.ToInt32(r.Cells[0].Value), Convert.ToInt32(r.Cells[1].Value), Convert.ToInt32(r.Cells[2].Value), Convert.ToString(r.Cells[3].Value), Convert.ToString(r.Cells[4].Value), Convert.ToString(r.Cells[5].Value));
                        selectedLoans.Add(loan);
                    }
                }
                if (selectedLoans.Any())
                {
                    //record in history excel sheet
                    HistoryMethods.WriteHistoryToExcel(this.localPath, selectedLoans);
                    MessageBox.Show("the record successfily add to history in excel file", "record saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnShow.Enabled = true;
                    txtSearch.Enabled = true;
                    comboSearch.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
