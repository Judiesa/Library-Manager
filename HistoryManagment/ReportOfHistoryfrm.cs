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
    public partial class ReportOfHistory : Form
    {
        string localPath;
        public ReportOfHistory()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            try
            {
                //select the excel file and save the path of the excel file
                openFileDialog1.Title = "Choose Excel file";
                openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.localPath = openFileDialog1.FileName;
                }
                if (this.localPath != null || this.localPath != "")
                {
                    rdbReturnedLoans.Enabled = true;
                    rdbHoldLoans.Enabled = true;
                    rdbBookName.Enabled = true;
                    rdbBookAuthor.Enabled = true;
                    rdbmemberLast.Enabled = true;
                    btnShow.Enabled = true;
                }
                else
                {
                    MessageBox.Show("please choose a file", "file not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rdbReturnedLoans.Enabled = false;
                    rdbHoldLoans.Enabled = false;
                    rdbBookName.Enabled = false;
                    rdbBookAuthor.Enabled = false;
                    rdbmemberLast.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nplease choose the file again", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rdbReturnedLoans.Enabled = false;
                rdbHoldLoans.Enabled = false;
                rdbBookName.Enabled = false;
                rdbBookAuthor.Enabled = false;
                rdbmemberLast.Enabled = false;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                rdbReturnedLoans.Enabled = false;
                rdbHoldLoans.Enabled = false;
                rdbBookName.Enabled = false;
                rdbBookAuthor.Enabled = false;
                rdbmemberLast.Enabled = false;
                //get report from excel history sheet based on selected option
                //based on returned loand
                if (rdbReturnedLoans.Checked)
                {
                    //get a list from loans and filter the loans that are returned books
                    var filteredByReturnLoans = HistoryMethods.ReadHistoryFromExcel(this.localPath)
                    .Where(x => x.Status == "returned");
                    //show the result in datagrid
                    foreach (var loan in filteredByReturnLoans)
                    {
                        dataGridView1.Rows.Add(loan.HistoryId, loan.LoanId, loan.MemberId, loan.BookId, loan.IssueDate, loan.ReturnDate, loan.Status);
                    }
                    if (dataGridView1.RowCount != 0)
                    {
                        MessageBox.Show("report of history based on loans that are returned books", "report of history", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rdbReturnedLoans.Enabled = true;
                        rdbHoldLoans.Enabled = true;
                        rdbBookName.Enabled = true;
                        rdbBookAuthor.Enabled = true;
                        rdbmemberLast.Enabled = true;
                    }
                    if (dataGridView1.RowCount == 0)
                    {
                        MessageBox.Show("the history contains no recorded based on loans that are returned books \ntry another option", "report of history", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rdbReturnedLoans.Enabled = true;
                        rdbHoldLoans.Enabled = true;
                        rdbBookName.Enabled = true;
                        rdbBookAuthor.Enabled = true;
                        rdbmemberLast.Enabled = true;
                    }
                }
                //based on hold loand
                else if (rdbHoldLoans.Checked)
                {
                    //get a list from loans and filter the loans that are holded books
                    var filteredByHoldLoans = HistoryMethods.ReadHistoryFromExcel(this.localPath)
                    .Where(x => x.Status == "borrowed");
                    //show the result in datagrid
                    foreach (var loan in filteredByHoldLoans)
                    {
                        dataGridView1.Rows.Add(loan.HistoryId, loan.LoanId, loan.MemberId, loan.BookId, loan.IssueDate, loan.ReturnDate, loan.Status);
                    }
                    if (dataGridView1.RowCount != 0)
                    {
                        MessageBox.Show("report of history based on loans that are holded books", "report of history", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rdbReturnedLoans.Enabled = true;
                        rdbHoldLoans.Enabled = true;
                        rdbBookName.Enabled = true;
                        rdbBookAuthor.Enabled = true;
                        rdbmemberLast.Enabled = true;
                    }
                    if (dataGridView1.RowCount == 0)
                    {
                        MessageBox.Show("the history contains no recorded based on loans that are returned books \ntry another option", "report of history", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rdbReturnedLoans.Enabled = true;
                        rdbHoldLoans.Enabled = true;
                        rdbBookName.Enabled = true;
                        rdbBookAuthor.Enabled = true;
                        rdbmemberLast.Enabled = true;
                    }
                }
                //based on book name
                else if (rdbBookName.Checked)
                {
                    //check the boxes be valid
                    if (txtBookName.Text == null || txtBookName.Text == "")
                    {
                        MessageBox.Show("please fill the search fileds correctly", "invalid textBox field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBookName.Clear();
                        txtBookName.Enabled = true;
                        return;
                    }
                    for (int i = 0; i < txtBookName.Text.Length; i++)
                    {
                        if (!char.IsLetterOrDigit(txtBookName.Text[i]))
                        {
                            MessageBox.Show("please fill the search fileds correctly", "invalid textBox field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtBookName.Clear();
                            txtBookName.Enabled = true;
                            return;
                        }
                    }
                    //filter the loan objects based on wanted item 
                    var libraryBooksFilteredByBookName = BookMethods.ReadBookFromExcel(this.localPath)
                            .Where(x => x.BookName == txtBookName.Text).ToArray();
                    //if book name does not exist in library
                    if (libraryBooksFilteredByBookName == null)
                    {
                        MessageBox.Show("the entered book Name does not exist in book library" + "\nplease try again", "invalid book name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBookName.Clear();
                        txtBookName.Enabled = false;
                        rdbReturnedLoans.Enabled = true;
                        rdbHoldLoans.Enabled = true;
                        rdbBookName.Enabled = true;
                        rdbBookAuthor.Enabled = true;
                        rdbmemberLast.Enabled = true;
                    }
                    //if book exsit
                    else
                    {
                        //find loans that match with this book id
                        var listLoans = HistoryMethods.ReadHistoryFromExcel(this.localPath);
                        List<History> filteredLoansByBookName = new List<History>();
                        for (int i = 0; i < listLoans.Count(); i++)
                        {
                            for (int j = 0; j < libraryBooksFilteredByBookName.Count(); j++)
                            {
                                if (listLoans[i].BookId == libraryBooksFilteredByBookName[j].BookId)
                                {
                                    filteredLoansByBookName.Add(listLoans[i]);
                                }
                            }
                        }
                        // if the book has never borrowed
                        if (!filteredLoansByBookName.Any())
                        {
                            MessageBox.Show("the entered Book Name has never been borrowed yet" + "\nyou can not record books with no history in loans", "book has not been borrowed yet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtBookName.Clear();
                            txtBookName.Enabled = false;
                            rdbReturnedLoans.Enabled = true;
                            rdbHoldLoans.Enabled = true;
                            rdbBookName.Enabled = true;
                            rdbBookAuthor.Enabled = true;
                            rdbmemberLast.Enabled = true;
                        }
                        else
                        {
                            //show the datas
                            foreach (var loan in filteredLoansByBookName)
                            {
                                dataGridView1.Rows.Add(loan.HistoryId, loan.LoanId, loan.MemberId, loan.BookId, loan.IssueDate, loan.ReturnDate, loan.Status);
                            }
                            if (dataGridView1.RowCount != 0)
                            {
                                MessageBox.Show("report of history based on entered book name", "report of history", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                rdbReturnedLoans.Enabled = true;
                                rdbHoldLoans.Enabled = true;
                                rdbBookName.Enabled = true;
                                rdbBookAuthor.Enabled = true;
                                rdbmemberLast.Enabled = true;
                            }
                            if (dataGridView1.RowCount == 0)
                            {
                                MessageBox.Show("the history contains no recorded based on this nook name \ntry another option", "report of history", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                rdbReturnedLoans.Enabled = true;
                                rdbHoldLoans.Enabled = true;
                                rdbBookName.Enabled = true;
                                rdbBookAuthor.Enabled = true;
                                rdbmemberLast.Enabled = true;
                            }
                        }
                    }
                }
                //based on author book name
                else if (rdbBookAuthor.Checked)
                {
                    //check the boxes be valid
                    if (txtAuthorName.Text == null || txtAuthorName.Text == "")
                    {
                        MessageBox.Show("please fill the search fileds correctly", "invalid textBox field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAuthorName.Clear();
                        txtAuthorName.Enabled = true;
                        return;
                    }
                    for (int i = 0; i < txtAuthorName.Text.Length; i++)
                    {
                        if (!char.IsLetter(txtAuthorName.Text[i]))
                        {
                            MessageBox.Show("please fill the search fileds correctly", "invalid textBox field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtAuthorName.Clear();
                            txtAuthorName.Enabled = true;
                            return;
                        }
                    }
                    //filter the library books by the name of author name that is entered 
                    var libraryBooksFilteredByAuthorName = BookMethods.ReadBookFromExcel(this.localPath)
                            .Where(x => x.Author == txtAuthorName.Text).ToArray();
                    //if author name was not exist in library
                    if (!libraryBooksFilteredByAuthorName.Any())
                    {
                        MessageBox.Show("the entered book author name does not exist in book library" + "\nplease try again", "invalid book name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAuthorName.Clear();
                        txtAuthorName.Enabled = false;
                        rdbReturnedLoans.Enabled = true;
                        rdbHoldLoans.Enabled = true;
                        rdbBookName.Enabled = true;
                        rdbBookAuthor.Enabled = true;
                        rdbmemberLast.Enabled = true;
                    }
                    else
                    {
                        //find loans that match with this member id
                        var listLoans = HistoryMethods.ReadHistoryFromExcel(this.localPath);
                        List<History> filteredLoansByMemberLastName = new List<History>();
                        for (int i = 0; i < listLoans.Count(); i++)
                        {
                            for (int j = 0; j < libraryBooksFilteredByAuthorName.Count(); j++)
                            {
                                if (listLoans[i].BookId == libraryBooksFilteredByAuthorName[j].BookId)
                                {
                                    filteredLoansByMemberLastName.Add(listLoans[i]);
                                }
                            }
                        }
                        // if the book has never borrowed
                        if (!filteredLoansByMemberLastName.Any())
                        {
                            MessageBox.Show("the entered Book Author Name is not exist in loan list this book has never been borrowed yet" + "\nyou can not record books with no history in loans", "book has not been borrowed yet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtBookName.Clear();
                            txtAuthorName.Enabled = false;
                            rdbReturnedLoans.Enabled = true;
                            rdbHoldLoans.Enabled = true;
                            rdbBookName.Enabled = true;
                            rdbBookAuthor.Enabled = true;
                            rdbmemberLast.Enabled = true;
                        }
                        else
                        {
                            //show the datas
                            foreach (var loan in filteredLoansByMemberLastName)
                            {
                                dataGridView1.Rows.Add(loan.LoanId, loan.MemberId, loan.BookId, loan.IssueDate, loan.ReturnDate, loan.Status);
                            }
                            if (dataGridView1.RowCount != 0)
                            {
                                MessageBox.Show("report of history based on entered book author name", "report of history", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                rdbReturnedLoans.Enabled = true;
                                rdbHoldLoans.Enabled = true;
                                rdbBookName.Enabled = true;
                                rdbBookAuthor.Enabled = true;
                                rdbmemberLast.Enabled = true;
                            }
                            if (dataGridView1.RowCount == 0)
                            {
                                MessageBox.Show("the history contains no recorded based on entered book author name \ntry another option", "report of history", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                rdbReturnedLoans.Enabled = true;
                                rdbHoldLoans.Enabled = true;
                                rdbBookName.Enabled = true;
                                rdbBookAuthor.Enabled = true;
                                rdbmemberLast.Enabled = true;
                            }
                        }
                    }
                }
                //based on member lastName
                else if (rdbmemberLast.Checked)
                {
                    //check the boxes be valid
                    if (txtLastName.Text == null || txtLastName.Text == "")
                    {
                        MessageBox.Show("please fill the search fileds correctly", "invalid textBox field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtLastName.Clear();
                        txtLastName.Enabled = true;
                        return;
                    }
                    for (int i = 0; i < txtLastName.Text.Length; i++)
                    {
                        if (!char.IsLetter(txtLastName.Text[i]))
                        {
                            MessageBox.Show("please fill the search fileds correctly", "invalid textBox field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtLastName.Clear();
                            txtLastName.Enabled = true;
                            return;
                        }
                    }
                    //filter the member list by the last name of the member that is entered 
                    var memberListFilteredByLastName = LoanMethods.LibraryMembers(this.localPath)
                        .Where(x => x.LastName == txtLastName.Text).ToArray();
                    //check that the entered last name exist or not
                    if (!memberListFilteredByLastName.Any())
                    {
                        MessageBox.Show("the entered member last name does not exist in member list" + "\nplease try again", "invalid book name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtLastName.Clear();
                        txtLastName.Enabled = false;
                        rdbReturnedLoans.Enabled = true;
                        rdbHoldLoans.Enabled = true;
                        rdbBookName.Enabled = true;
                        rdbBookAuthor.Enabled = true;
                        rdbmemberLast.Enabled = true;
                    }
                    else
                    {
                        //find loans that match with member id
                        var listLoans = HistoryMethods.ReadHistoryFromExcel(this.localPath);
                        List<History> filteredLoansByLastName = new List<History>();
                        for (int i = 0; i < listLoans.Count(); i++)
                        {
                            for (int j = 0; j < memberListFilteredByLastName.Count(); j++)
                            {
                                if (listLoans[i].MemberId == memberListFilteredByLastName[j].MemberId)
                                {
                                    filteredLoansByLastName.Add(listLoans[i]);
                                }
                            }
                        }
                        // if the book has never borrowed
                        if (!filteredLoansByLastName.Any())
                        {
                            MessageBox.Show("the entered member LastName is not exist in loan list this member has never been borrowed any book" + "\nyou can not record members with no history in loans", "member has never been borrowed book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtBookName.Clear();
                            txtAuthorName.Enabled = false;
                            rdbReturnedLoans.Enabled = true;
                            rdbHoldLoans.Enabled = true;
                            rdbBookName.Enabled = true;
                            rdbBookAuthor.Enabled = true;
                            rdbmemberLast.Enabled = true;
                        }
                        else
                        {
                            //show the datas
                            foreach (var loan in filteredLoansByLastName)
                            {
                                dataGridView1.Rows.Add(loan.HistoryId, loan.LoanId, loan.MemberId, loan.BookId, loan.IssueDate, loan.ReturnDate, loan.Status);
                            }
                            if (dataGridView1.RowCount != 0)
                            {
                                MessageBox.Show("report of history based on entered member last name", "report of history", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                rdbReturnedLoans.Enabled = true;
                                rdbHoldLoans.Enabled = true;
                                rdbBookName.Enabled = true;
                                rdbBookAuthor.Enabled = true;
                                rdbmemberLast.Enabled = true;
                            }
                            if (dataGridView1.RowCount == 0)
                            {
                                MessageBox.Show("the history contains no recorded based on enteredmember last name \ntry another option", "report of history", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                rdbReturnedLoans.Enabled = true;
                                rdbHoldLoans.Enabled = true;
                                rdbBookName.Enabled = true;
                                rdbBookAuthor.Enabled = true;
                                rdbmemberLast.Enabled = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nplease choose the file again", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rdbReturnedLoans.Enabled = false;
                rdbHoldLoans.Enabled = false;
                rdbBookName.Enabled = false;
                rdbBookAuthor.Enabled = false;
                rdbmemberLast.Enabled = false;
                txtBookName.Enabled = false;
                txtAuthorName.Enabled = false;
                txtLastName.Enabled = false;
                txtBookName.Clear();
                txtAuthorName.Clear();
                txtLastName.Clear();
            }
        }

        private void rdbBookName_CheckedChanged(object sender, EventArgs e)
        {
            txtBookName.Enabled = true;
            txtAuthorName.Enabled = false;
            txtLastName.Enabled = false;
            txtAuthorName.Clear();
            txtLastName.Clear();
        }

        private void rdbBookAuthor_CheckedChanged(object sender, EventArgs e)
        {
            txtAuthorName.Enabled = true;
            txtBookName.Enabled = false;
            txtLastName.Enabled = false;
            txtBookName.Clear();
            txtLastName.Clear();
        }

        private void rdbmemberLast_CheckedChanged(object sender, EventArgs e)
        {
            txtLastName.Enabled = true;
            txtBookName.Enabled = false;
            txtAuthorName.Enabled = false;
            txtBookName.Clear();
            txtAuthorName.Clear();
        }
    }

}


