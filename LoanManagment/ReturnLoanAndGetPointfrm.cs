using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;


namespace Project
{
    public partial class ReturnLoanAndGetPointfrm : Form
    {
        string localPath;

        public ReturnLoanAndGetPointfrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
                    btnSearch.Enabled = true;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Enabled = false;
                comboSearch.Enabled = false;
                //check the validation 
                if (txtSearch.Text == null || txtSearch.Text == "")
                {
                    MessageBox.Show("please fill the search fileds correctly", "textBox field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSearch.Clear();
                    comboSearch.Enabled = true;
                    txtSearch.Enabled = true;
                    return;
                }
                if (comboSearch.Text == null || comboSearch.Text == "none" || comboSearch.Text == "")
                {
                    MessageBox.Show("please fill the search fileds correctly", "status field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboSearch.Text = "none";
                    txtSearch.Enabled = true;
                    comboSearch.Enabled = true;
                    return;
                }
                //list of loan excel sheet
                List<Loan> loansList = LoanMethods.ReadLoansFromExcel(this.localPath);
                //filter the book objects based on wanted item
                switch (comboSearch.Text)
                {
                    //if comboBox selected was loanid
                    case "Book Name":
                        //check validation
                        for (int i = 0; i < txtSearch.Text.Length; i++)
                        {
                            if (!char.IsLetterOrDigit(txtSearch.Text[i]))
                            {
                                MessageBox.Show("please fill the search fileds correctly", "status field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                comboSearch.Text = "none";
                                txtSearch.Enabled = true;
                                comboSearch.Enabled = true;
                                return;
                            }
                        }
                        var libraryBooks = BookMethods.ReadBookFromExcel(this.localPath)
                            .Where(x => x.BookName == txtSearch.Text)
                            .Where(x => x.Status == "borrowed")
                            .ToArray();
                        //check if the book object is null or not
                        if (libraryBooks == null)
                        {
                            MessageBox.Show("the entered book name is not exist in choosen file", "invalid book name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtSearch.Clear();
                        }
                        else
                        {
                            //filter the loans list with entered name
                            var loanList = LoanMethods.ReadLoansFromExcel(this.localPath);
                            List<Loan> filteredLoansByBookName = new List<Loan>();
                            for (int i = 0; i < loanList.Count; i++)
                            {
                                for (int j = 0; j < libraryBooks.Count(); j++)
                                {
                                    if (loanList[i].BookId == libraryBooks[j].BookId)
                                    {
                                        filteredLoansByBookName.Add(loanList[i]);
                                    }
                                }
                            }
                            if (!filteredLoansByBookName.Any())
                            {
                                MessageBox.Show("no match found", "no match found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtSearch.Enabled = true;
                                comboSearch.Enabled = true;
                            }
                            else
                            {
                                //clear datagrid
                                dataGridView1.Rows.Clear();
                                //show the result in datagrid
                                foreach (var loan in filteredLoansByBookName)
                                {
                                    dataGridView1.Rows.Add(loan.LoanId, loan.MemberId, loan.BookId, loan.IssueDate, loan.ReturnDate, loan.Status);
                                }
                                MessageBox.Show("filter based on book name applied, now you can select from table and return the selected loan", "filter applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dataGridView1.Enabled = true;
                                btnReturn.Enabled = true;
                            }
                        }
                        break;
                    case "Book Author Name":
                        //check validation
                        for (int i = 0; i < txtSearch.Text.Length; i++)
                        {
                            if (!char.IsLetter(txtSearch.Text[i]))
                            {
                                MessageBox.Show("please fill the search fileds correctly", "status field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                comboSearch.Text = "none";
                                txtSearch.Enabled = true;
                                comboSearch.Enabled = true;
                                txtSearch.Clear();
                                return;
                            }
                        }
                        var bookLibrary = BookMethods.ReadBookFromExcel(this.localPath)
                            .Where(x => x.Author == txtSearch.Text)
                            .Where(x => x.Status == "borrowed")
                            .ToArray();
                        //check if the book object is null or not
                        if (bookLibrary == null)
                        {
                            MessageBox.Show("the entered book author name is not exist in choosen file", "invalid book author name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            comboSearch.Text = "none";
                            txtSearch.Enabled = true;
                            comboSearch.Enabled = true;
                            txtSearch.Clear();
                            return;
                        }
                        else
                        {
                            //filter the loans list with entered name
                            var loanList = LoanMethods.ReadLoansFromExcel(this.localPath);
                            List<Loan> filteredLoansByBookAuthorName = new List<Loan>();
                            for (int i = 0; i < loanList.Count; i++)
                            {
                                for (int j = 0; j < bookLibrary.Count(); j++)
                                {
                                    if (loanList[i].BookId == bookLibrary[j].BookId && loanList[i].Status == "borrowed")
                                    {
                                        filteredLoansByBookAuthorName.Add(loanList[i]);
                                    }
                                }
                            }
                            if (!filteredLoansByBookAuthorName.Any())
                            {
                                MessageBox.Show("no match found", "no match found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                comboSearch.Text = "none";
                                txtSearch.Enabled = true;
                                comboSearch.Enabled = true;
                                txtSearch.Clear();
                                return;
                            }
                            else
                            {
                                //clear datagrid
                                dataGridView1.Rows.Clear();
                                //show the result in datagrid
                                foreach (var loan in filteredLoansByBookAuthorName)
                                {
                                    dataGridView1.Rows.Add(loan.LoanId, loan.MemberId, loan.BookId, loan.IssueDate, loan.ReturnDate, loan.Status);
                                }
                                MessageBox.Show("filter based on book author name applied, now you can select from table and return the selected loan", "filter applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dataGridView1.Enabled = true;
                                btnReturn.Enabled = true;
                            }
                        }
                        break;
                    case "Member LastName":
                        //check validation
                        for (int i = 0; i < txtSearch.Text.Length; i++)
                        {
                            if (!char.IsLetter(txtSearch.Text[i]))
                            {
                                MessageBox.Show("please fill the search fileds correctly", "status field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                comboSearch.Text = "none";
                                txtSearch.Enabled = true;
                                comboSearch.Enabled = true;
                                txtSearch.Clear();
                                return;
                            }
                        }

                        var memberList = LoanMethods.LibraryMembers(this.localPath)
                            .Where(x => x.LastName == txtSearch.Text)
                            .ToArray();
                        //check if the book object is null or not
                        if (memberList == null)
                        {
                            MessageBox.Show("the entered member last name is not exist in choosen file", "invalid member last name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            comboSearch.Text = "none";
                            txtSearch.Enabled = true;
                            comboSearch.Enabled = true;
                            txtSearch.Clear();
                        }
                        else
                        {
                            //filter the loans list with entered name
                            var loanList = LoanMethods.ReadLoansFromExcel(this.localPath);
                            List<Loan> filteredLoansByLastName = new List<Loan>();
                            for (int i = 0; i < loanList.Count; i++)
                            {
                                for (int j = 0; j < memberList.Count(); j++)
                                {
                                    if (loanList[i].MemberId == memberList[j].MemberId && loanList[i].Status == "borrowed")
                                    {
                                        filteredLoansByLastName.Add(loanList[i]);
                                    }
                                }
                            }
                            if (!filteredLoansByLastName.Any())
                            {
                                MessageBox.Show("no match found", "no match found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                comboSearch.Text = "none";
                                txtSearch.Enabled = true;
                                comboSearch.Enabled = true;
                                txtSearch.Clear();
                            }
                            else
                            {
                                //clear datagrid
                                dataGridView1.Rows.Clear();
                                //show the result in datagrid
                                foreach (var loan in filteredLoansByLastName)
                                {
                                    dataGridView1.Rows.Add(loan.LoanId, loan.MemberId, loan.BookId, loan.IssueDate, loan.ReturnDate, loan.Status);
                                }
                                MessageBox.Show("filter based on member last name applied, now you can select from table and return the selected loan", "filter applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dataGridView1.Enabled = true;
                                btnReturn.Enabled = true;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboSearch.Text = "none";
                txtSearch.Enabled = true;
                comboSearch.Enabled = true;
                txtSearch.Clear();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            //return the book to library, change book status and save the changes in excel
            try
            {
                //change point of member if return date being on exact date
                List<Member> members = UserMethods.ReadMembersFromExcel(this.localPath);
                //change book status of books that selected
                List<Book> returnedBooks = BookMethods.ReadBookFromExcel(this.localPath);
                bool[] selectedLoans = new bool[dataGridView1.Rows.Count];
                // move on rows in dataGridView
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    int currentRow = r.Index;
                    DataGridViewRow newDataGrid = dataGridView1.Rows[currentRow];
                    //date of today
                    DateTime today = DateTime.Today;
                    // convert the return date string to dataTime type
                    DateTime returnDate = Convert.ToDateTime(Convert.ToString(r.Cells[4].Value));
                    //check that which loans want return issue and it is valid
                    if (Convert.ToBoolean(r.Cells[6].Value) == true && Convert.ToString(r.Cells[5].Value) == "borrowed")
                    {
                        selectedLoans[currentRow] = true;
                        // has delay in return issue
                        if (today > returnDate)
                        {
                            newDataGrid.Cells[5].Value = "returned";
                            newDataGrid.Cells[4].Value = today.ToString();
                        }
                        //return on time(no delay)
                        else if (today <= returnDate)
                        {
                            newDataGrid.Cells[5].Value = "returned";
                            newDataGrid.Cells[4].Value = today.ToString();
                            foreach (var member in members)
                            {
                                if (Convert.ToInt32(r.Cells[1].Value) == member.MemberId)
                                {
                                    //member returned book on time and get point
                                    member.MemberPoint += 50;
                                    break;
                                }
                            }
                        }
                        //change book status in excel
                        foreach (var book in returnedBooks)
                        {
                            if (Convert.ToInt32(r.Cells[2].Value) == book.BookId)
                            {
                                book.Status = "available";
                                break;
                            }
                        }
                    }
                }
                bool shouldSaveDataGrid = false;
                foreach (var loans in selectedLoans)
                {
                    //if even one of the loans changed the excel file should be updated and saved
                    if (loans == true)
                    {
                        shouldSaveDataGrid = true;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                //save the changes of return date and status to excel file
                //save the datagrid to an list of loans
                if (shouldSaveDataGrid == true)
                {
                    List<Loan> changedListLoans = new List<Loan>();
                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        Loan rLoan = new Loan(Convert.ToInt32(r.Cells[0].Value), Convert.ToInt32(r.Cells[1].Value), Convert.ToInt32(r.Cells[2].Value), Convert.ToString(r.Cells[3].Value), Convert.ToString(r.Cells[4].Value), Convert.ToString(r.Cells[5].Value));
                        changedListLoans.Add(rLoan);
                    }
                    UserMethods.ApplyMemberChanges(this.localPath, members);
                    BookMethods.ApplyBookChanges(this.localPath, returnedBooks);
                    LoanMethods.ApplyLoanChanges(this.localPath, changedListLoans);
                    MessageBox.Show("returned loans and books saved successfully", "changes applied ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnChooseFile.Enabled = true;
                }
                else
                {
                    if (MessageBox.Show("do you want return any book?", "no loan selected", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.Close();
                    }
                    else
                    {
                        btnSearch.Enabled = true;
                        comboSearch.Enabled = true;
                        txtSearch.Enabled = true;
                        txtSearch.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSearch.Enabled = true;
                comboSearch.Enabled = true;
                txtSearch.Enabled = true;
                txtSearch.Clear();
            }
        }

    }
}
