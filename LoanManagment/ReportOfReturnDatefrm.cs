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
    public partial class ReportOfReturnDatefrm : Form
    {
        string localPath;
        public ReportOfReturnDatefrm()
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
                    List<Loan> loan = LoanMethods.ReadLoansFromExcel(this.localPath);
                    for (int i = 0; i < loan.Count; i++)
                    {
                        if (loan[i].Status == "borrowed") 
                            dataGridView1.Rows.Add(loan[i].LoanId, loan[i].MemberId, loan[i].BookId, loan[i].IssueDate, loan[i].ReturnDate, loan[i].Status);
                    }
                    btnReportDate.Enabled = true;
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

        private void btnReportDate_Click(object sender, EventArgs e)
        {
            //filter the excel result based on wanted item
            try
            {
                txtSearch.Enabled = false;
                comboSearch.Enabled = false;
                if (comboSearch.Text != "show all")
                {
                    //check the boxes be valid
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
                        case "loanID":
                            //cjeck validation
                            var listFilteredByLoanID = loansList
                                .Where(x => x.LoanId == int.Parse(txtSearch.Text))
                                .Where(x => x.Status == "borrowed")
                                .FirstOrDefault();
                            //check if the loan object is null or not
                            if (listFilteredByLoanID == null)
                            {
                                MessageBox.Show("the entered loan ID is not exist in choosen file", "invalid loanId", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtSearch.Clear();
                                comboSearch.Text = "none";
                                txtSearch.Enabled = true;
                                comboSearch.Enabled = true;
                            }
                            else
                            {
                                //clear the dataGrid for new search result
                                dataGridView1.Rows.Clear();
                                //show the result in datagrid
                                dataGridView1.Rows.Add(listFilteredByLoanID.LoanId, listFilteredByLoanID.MemberId, listFilteredByLoanID.BookId, listFilteredByLoanID.IssueDate, listFilteredByLoanID.ReturnDate, listFilteredByLoanID.Status);
                                MessageBox.Show("the return date of loanID :" + listFilteredByLoanID.LoanId + " is\n" + listFilteredByLoanID.ReturnDate, "return date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dataGridView1.Enabled = true;
                                MessageBox.Show("now you can calculate remind days by clicking on buttons in table", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        //if comboBox selected was memberid
                        case "memberID":
                            var listFilteredByMemberId = loansList
                                .Where(x => x.MemberId == int.Parse(txtSearch.Text))
                                .Where(x => x.Status == "borrowed")
                                .Select(x => x);
                            //check if the loan object is null or not
                            if (listFilteredByMemberId == null)
                            {
                                MessageBox.Show("the entered member ID is not exist in choosen file", "invalid memberID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtSearch.Clear();
                                comboSearch.Text = "none";
                                txtSearch.Enabled = true;
                                comboSearch.Enabled = true;
                            }
                            else
                            {
                                //clear the dataGrid for new search result
                                dataGridView1.Rows.Clear();
                                //show the result in datagrid
                                foreach (var loan in listFilteredByMemberId)
                                {
                                    dataGridView1.Rows.Add(loan.LoanId, loan.MemberId, loan.BookId, loan.IssueDate, loan.ReturnDate, loan.Status);
                                }
                                dataGridView1.Enabled = true;
                                MessageBox.Show("now you can calculate remind days by clicking on buttons in table", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "bookID":
                            var listFilteredByBookId = loansList
                                .Where(x => x.BookId == int.Parse(txtSearch.Text))
                                .Where(x => x.Status == "borrowed")
                                .LastOrDefault();
                            //check if the loan object is null or not
                            if (listFilteredByBookId == null)
                            {
                                MessageBox.Show("the entered book ID is not exist in choosen file", "invalid bookID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtSearch.Clear();
                                comboSearch.Text = "none";
                                txtSearch.Enabled = true;
                                comboSearch.Enabled = true;
                            }
                            else
                            {
                                //clear the dataGrid for new search result
                                dataGridView1.Rows.Clear();
                                dataGridView1.Rows.Add(listFilteredByBookId.LoanId, listFilteredByBookId.MemberId, listFilteredByBookId.BookId, listFilteredByBookId.IssueDate, listFilteredByBookId.ReturnDate, listFilteredByBookId.Status);
                                MessageBox.Show("the return date of loanID :" + listFilteredByBookId.LoanId + " is\n" + listFilteredByBookId.ReturnDate, "return date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dataGridView1.Enabled = true;
                                MessageBox.Show("now you can calculate remind days by clicking on buttons in table", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                    }
                    //check that any book object has found with wanted items or not
                    if (dataGridView1.Rows.Count == 0)
                    {
                        MessageBox.Show("no match found", "no match found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSearch.Enabled = true;
                        comboSearch.Enabled = true;
                        btnReportDate.Enabled = true;
                    }
                }
                else if (comboSearch.Text == "show all") 
                {
                    MessageBox.Show("now you can calculate remind days by clicking on buttons in table", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.Enabled = true;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("the ID should contains just numbers, please fill the field correctly", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearch.Clear();
                comboSearch.Text = "none";
                txtSearch.Enabled = true;
                comboSearch.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearch.Clear();
                comboSearch.Text = "none";
                txtSearch.Enabled = true;
                comboSearch.Enabled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblCalculate.Text = "";
                lblCalculate.Visible = false;
                lblDays.Visible = false;
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Calculate")
                {
                    int rowdataGrid = dataGridView1.Rows[e.RowIndex].Index;
                    if (dataGridView1.Rows[rowdataGrid].Cells[5].Value.ToString() == "borrowed")
                    {
                        DateTime returnDate = Convert.ToDateTime(dataGridView1.Rows[rowdataGrid].Cells[4].Value);
                        //today date
                        DateTime today = DateTime.Today;
                        lblCalculate.Text = Convert.ToString((returnDate - today).Days);
                        lblCalculate.Visible = true;
                        lblDays.Visible = true;
                        MessageBox.Show("remind lending period loan ID " + Convert.ToString(dataGridView1.Rows[rowdataGrid].Cells[1].Value) + " for book with ID " + Convert.ToString(dataGridView1.Rows[rowdataGrid].Cells[2].Value) + " is " + Convert.ToString((returnDate - today).Days) + " days.", "return date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
