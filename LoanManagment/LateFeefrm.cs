using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Project
{
    public partial class PenaltiesDelayReturnfrm : Form
    {
        string localPath;
        public PenaltiesDelayReturnfrm()
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
                    //show the excel file in form by loan objects
                    List<Loan> loans = LoanMethods.ReadLoansFromExcel(this.localPath);
                    //an array for delay return column in dataGridView
                    int[] delayReturnLoans = new int[loans.Count];
                    //today date
                    DateTime today = DateTime.Today;
                    //compar the return date and today date
                    for (int i = 0; i < loans.Count; i++)
                    {
                        //check that the book is returned or not
                        if (loans[i].Status == "borrowed")
                        {
                            //change the return date type to dateTime type
                            DateTime dt = Convert.ToDateTime(loans[i].ReturnDate);
                            //the return date has not come yet or is today
                            if (dt >= today)
                            {
                                delayReturnLoans[i] = 0;
                            }
                            //the return date passed
                            else if (dt < today)
                            {
                                delayReturnLoans[i] = (today - dt).Days;
                            }
                        }
                        else
                        {
                            delayReturnLoans[i] = 0;
                        }
                    }
                    //show the excel sheet loan in dataGridView only the borrowed items
                    for (int i = 0; i < loans.Count; i++)
                    {
                        dataGridView1.Rows.Add(loans[i].LoanId, loans[i].MemberId, loans[i].BookId, loans[i].IssueDate, loans[i].ReturnDate, loans[i].Status, delayReturnLoans[i]);
                    }
                    btnFind.Enabled = true;
                    comboSearch.Enabled = true;
                    txtSearch.Enabled = true;
                }
                else
                {
                    MessageBox.Show("please choose a file", "file not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nplease choosethe file again", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //filter the datagridview by entered items
            try
            {
                txtSearch.Enabled = false;
                comboSearch.Enabled = false;
                //check the boxes be valid
                if (comboSearch.Text != "show all loans")
                {
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

                    //list of loans from excel file
                    List<Loan> loansList = LoanMethods.ReadLoansFromExcel(this.localPath);
                    //filter the book objects based on wanted item
                    switch (comboSearch.Text)
                    {
                        //if comboBox selected was loanid
                        case "loanID":
                            var listFilteredByLoanID = loansList
                                .Where(x => x.LoanId == int.Parse(txtSearch.Text))
                                .FirstOrDefault();
                            //check if the loan object is null or not
                            if (listFilteredByLoanID == null)
                            {
                                MessageBox.Show("the entered loan ID is not exist in choosen file", "invalid loanId", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtSearch.Clear();
                            }
                            else
                            {
                                //the last column in dataGridView
                                int numDelay = 0;
                                //today date
                                DateTime today = DateTime.Today;
                                //compar the return date and today date
                                //check that the book is returned or not
                                if (listFilteredByLoanID.Status == "borrowed")
                                {
                                    //change the return date type to dateTime type
                                    DateTime dt = Convert.ToDateTime(listFilteredByLoanID.ReturnDate);
                                    //the return date has not come yet or is today
                                    if (dt >= today)
                                    {
                                        numDelay = 0;
                                    }
                                    //the return date passed
                                    else if (dt < today)
                                    {
                                        numDelay = (today - dt).Days;
                                    }
                                }
                                //clear the dataGrid for new search result
                                dataGridView1.Rows.Clear();
                                //show the result in datagrid
                                dataGridView1.Rows.Add(listFilteredByLoanID.LoanId, listFilteredByLoanID.MemberId, listFilteredByLoanID.BookId, listFilteredByLoanID.IssueDate, listFilteredByLoanID.ReturnDate, listFilteredByLoanID.Status, numDelay);
                                dataGridView1.Enabled = true;
                                MessageBox.Show("now you can click on \"Calculate Late Fee\" buttons to calculate the fines for each loan", "guide", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        //if comboBox selected was memberid
                        case "memberID":
                            var listFilteredByMemberId = loansList
                                .Where(x => x.MemberId == int.Parse(txtSearch.Text))
                                .Select(x => x)
                                .ToList();
                            //check if the loan object is null or not
                            if (listFilteredByMemberId == null)
                            {
                                MessageBox.Show("the entered member ID is not exist in choosen file", "invalid memberID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtSearch.Clear();
                            }
                            else
                            {
                                //an array for delay return column in dataGridView
                                int[] numDelay = new int[listFilteredByMemberId.Count];
                                //today date
                                DateTime today = DateTime.Today;
                                //compar the return date and today date
                                for (int i = 0; i < listFilteredByMemberId.Count; i++)
                                {
                                    //check that the book is returned or not
                                    if (listFilteredByMemberId[i].Status == "borrowed")
                                    {
                                        //change the return date type to dateTime type
                                        DateTime dt = Convert.ToDateTime(listFilteredByMemberId[i].ReturnDate);
                                        //the return date has not come yet or is today
                                        if (dt >= today)
                                        {
                                            numDelay[i] = 0;
                                        }
                                        //the return date passed
                                        else if (dt < today)
                                        {
                                            numDelay[i] = (today - dt).Days;
                                        }
                                    }
                                    else
                                    {
                                        numDelay[i] = 0;
                                    }
                                }
                                //clear the dataGrid for new search result
                                dataGridView1.Rows.Clear();
                                //show the result in datagrid
                                for (int i = 0; i < listFilteredByMemberId.Count; i++)
                                {

                                    dataGridView1.Rows.Add(listFilteredByMemberId[i].LoanId, listFilteredByMemberId[i].MemberId, listFilteredByMemberId[i].BookId, listFilteredByMemberId[i].IssueDate, listFilteredByMemberId[i].ReturnDate, listFilteredByMemberId[i].Status, numDelay[i]);
                                }
                                dataGridView1.Enabled = true;
                                MessageBox.Show("now you can click on \"Calculate Late Fee\" buttons to calculate the  fines for each loan", "guide", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "bookID":
                            var listFilteredByBookId = loansList
                                .Where(x => x.BookId == int.Parse(txtSearch.Text))
                                .LastOrDefault();
                            //check if the loan object is null or not
                            if (listFilteredByBookId == null)
                            {
                                MessageBox.Show("the entered book ID is not exist in choosen file", "invalid bookID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtSearch.Clear();
                            }
                            else
                            {
                                //the last column in dataGridView
                                int numDelay = 0;
                                //today date
                                DateTime today = DateTime.Today;
                                //compar the return date and today date
                                //check that the book is returned or not
                                if (listFilteredByBookId.Status == "borrowed")
                                {
                                    //change the return date type to dateTime type
                                    DateTime dt = Convert.ToDateTime(listFilteredByBookId.ReturnDate);
                                    //the return date has not come yet or is today
                                    if (dt >= today)
                                    {
                                        numDelay = 0;
                                    }
                                    //the return date passed
                                    else if (dt < today)
                                    {
                                        numDelay = (today - dt).Days;
                                    }
                                }
                                //clear the dataGrid for new search result
                                dataGridView1.Rows.Clear();
                                dataGridView1.Rows.Add(listFilteredByBookId.LoanId, listFilteredByBookId.MemberId, listFilteredByBookId.BookId, listFilteredByBookId.IssueDate, listFilteredByBookId.ReturnDate, listFilteredByBookId.Status, numDelay);
                                dataGridView1.Enabled = true;
                                MessageBox.Show("now you can click on \"Calculate Late Fee\" buttons to calculate the fines for each loan", "guide", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                    }
                    //check that any book object has found with wanted items or not
                    if (dataGridView1.Rows.Count == 0)
                        MessageBox.Show("no match found", "no match found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSearch.Enabled = true;
                    comboSearch.Enabled = true;
                }
                else if (comboSearch.Text == "show all loans")
                {
                    dataGridView1.Enabled = true;
                    MessageBox.Show("now you can click on \"Calculate Late Fee\" buttons to calculate the fines for each loan", "guide", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("the ID should contains just numbers, please fill the field correctly", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblPenalty.Text = "";
                lblBill.Visible = false;
                lblPenalty.Visible = false;
                if (dataGridView1.Columns[e.ColumnIndex].Name == "CalculatePenalty")
                {
                    int rowdataGrid = dataGridView1.Rows[e.RowIndex].Index;
                    int delay = Convert.ToInt32(dataGridView1.Rows[rowdataGrid].Cells[6].Value);
                    //for each one day delay the penalty is 500 toman
                    int bill = delay * 500;
                    lblPenalty.Text = Convert.ToString(bill);
                    lblBill.Visible = true;
                    lblPenalty.Visible = true;
                    MessageBox.Show("fine bill for the member with ID " + Convert.ToString(dataGridView1.Rows[rowdataGrid].Cells[1].Value) + " for book with ID " + Convert.ToString(dataGridView1.Rows[rowdataGrid].Cells[2].Value) + " is " + Convert.ToString(bill) + " toman.", "fine bill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
