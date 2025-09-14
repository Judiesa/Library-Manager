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
    public partial class AddIssueBookfrm : Form
    {
        string localPath;
        public AddIssueBookfrm()
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
                txtBookId.Clear();
                txtMemberId.Clear();
                dateTimeIssue.ResetText();
                dateTimeReturn.ResetText();
                //select the excel file and save the path of the excel file
                openFileDialog1.Title = "Choose Excel file";
                openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.localPath = openFileDialog1.FileName;
                }
                if (this.localPath != null || this.localPath != "")
                {
                    btnAddIssue.Enabled = true;
                    txtMemberId.Enabled = true;
                    txtBookId.Enabled = true;
                    dateTimeIssue.Enabled = true;
                }
                else
                {
                    MessageBox.Show("please choose a file", "file not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBookId.Clear();
                    txtMemberId.Clear();
                    dateTimeIssue.ResetText();
                    dateTimeReturn.ResetText();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nplease choose the file again", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookId.Clear();
                txtMemberId.Clear();
                dateTimeIssue.ResetText();
                dateTimeReturn.ResetText();
            }
        }

        private void btnAddIssue_Click(object sender, EventArgs e)
        {
            try
            {
                txtBookId.Enabled = false;
                txtMemberId.Enabled = false;
                dateTimeIssue.Enabled = false;
                //find the id for recording the new loan in excel file
                int loanId = LoanMethods.FindTheLastId(this.localPath) + 1;
                //set the return date
                DateTime dt = dateTimeIssue.Value;
                dateTimeReturn.Value = dt.AddDays(10);
                //make an object of class loan
                Loan loan = new Loan(loanId, int.Parse(txtMemberId.Text), int.Parse(txtBookId.Text), dateTimeIssue.Text, dateTimeReturn.Text, comboStatus.Text);
                //check that the fields have valid value
                for (int i = 1; i < loan.IssueBookInfo.Length - 3; i++)
                {
                    for (int j = 0; j < loan.IssueBookInfo[i].Length; j++)
                    {
                        if (!Char.IsDigit(loan.IssueBookInfo[i][j]))
                        {
                            MessageBox.Show("invalid data" + "\nplease try again and fill the fields correctly", "invalid data field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtBookId.Clear();
                            txtMemberId.Clear();
                            return;
                        }
                    }
                }
                Book askedBook = new Book(loan.BookId);
                //check that the member id that is entered is exist or not
                var requestedMemberInfo = LoanMethods.LibraryMembers(this.localPath)
                    .Where(x => x.MemberId == loan.MemberId)
                    .FirstOrDefault();
                //member id is not exist
                if (requestedMemberInfo == null)
                {
                    MessageBox.Show("the entered member ID is not exist in choosen file", "invalid member ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMemberId.Clear();
                }
                //member id exist
                else
                {
                    //check that the book id that is entered is exist or not
                    var requestedBookInfo = BookMethods.ReadBookFromExcel(this.localPath)
                        .Where(x => x.BookId == loan.BookId)
                        .FirstOrDefault();
                    //book id not exist
                    if (requestedBookInfo == null)
                    {
                        MessageBox.Show("the entered book ID is not exist in choosen file", "invalid member ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBookId.Clear();
                    }
                    //book id exist
                    else
                    {
                        //check the book status is available or is borrowed
                        //is borrowed
                        if (requestedBookInfo.Status == "borrowed")
                        {
                            MessageBox.Show("sorry, this book has been borrowed", "not available book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtBookId.Clear();
                        }
                        //book is available
                        else
                        {
                            //check that the member can borrow a book or not
                            //a member can issued 3 book
                            if (LoanMethods.CanBorrowedBooks(this.localPath, requestedMemberInfo))
                            {
                                //change the status of the asked book to borrowed
                                txtIssueId.Text = Convert.ToString(loan.LoanId);
                                LoanMethods.ChangeBookStatus(this.localPath, askedBook);
                                //add new loan to excel file
                                LoanMethods.AddLoanToExcel(this.localPath, loan);
                                MessageBox.Show("new issue book Info successfully added to Excel" + "\nyour loan ID is " + loanId, "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            //can not borrow more books
                            else
                            {
                                MessageBox.Show("muximum number of book has been issued" + "\nyou cannot issue more books", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookId.Clear();
                txtMemberId.Clear();
            }
        }
    }
}
