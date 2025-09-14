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
    public partial class ExtensionReturnDatefrm : Form
    {
        string localPath;
        public ExtensionReturnDatefrm()
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
                        dataGridView1.Rows.Add(loan[i].LoanId, loan[i].MemberId, loan[i].BookId, loan[i].IssueDate, loan[i].ReturnDate, loan[i].Status);
                    }
                    btnShow.Enabled = true;
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

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                btnShow.Enabled = false;
                comboSearch.Enabled = false;
                btnChooseFile.Enabled = false;
                //check that the combobox have a valid data
                if (comboSearch.Text == "enter a memberID")
                {
                    MessageBox.Show("please enter a memberID", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnShow.Enabled = true;
                    comboSearch.Enabled = true;
                    comboSearch.Text = "enter a memberID";
                    return;
                }
                else if (comboSearch.Text != "show all loans")
                {
                    //search on the excel sheet loan for member id
                    var listLoansBasedOnMemberId = LoanMethods.ReadLoansFromExcel(this.localPath)
                        .Where(x => x.MemberId == int.Parse(comboSearch.Text))
                        .Select(x => x);
                    //check that the entered memberid in excel sheet loan, exist or not
                    //member id exsit
                    if (listLoansBasedOnMemberId.Any())
                    {
                        //clear the dataGrid for new search result
                        dataGridView1.Rows.Clear();
                        //show the result in datagrid
                        foreach (var loans in listLoansBasedOnMemberId)
                        {
                            dataGridView1.Rows.Add(loans.LoanId, loans.MemberId, loans.BookId, loans.IssueDate, loans.ReturnDate, loans.Status);
                        }
                        MessageBox.Show("now you can select loans to extension of the return date and options of extension are in the right of the page", "guide", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnExtension.Enabled = true;
                        rdb10day.Enabled = true;
                        rdbWeek.Enabled = true;
                        rdbMonth.Enabled = true;
                        dataGridView1.Enabled = true;
                    }
                    //member id does not exist
                    else
                    {
                        MessageBox.Show("the entered member ID is not recorded in choosen file. Or this member did not borrow any book", "invalid memberID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnShow.Enabled = true;
                        comboSearch.Enabled = true;
                        comboSearch.Text = "enter a memberID";
                    }
                }
                //no filter and search by number id
                else
                {
                    MessageBox.Show("now you can select loans to extension of the return date and options of extension are in the right of the page", "guide", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnExtension.Enabled = true;
                    rdb10day.Enabled = true;
                    rdbWeek.Enabled = true;
                    rdbMonth.Enabled = true;
                    dataGridView1.Enabled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnShow.Enabled = true;
                comboSearch.Enabled = true;
                comboSearch.Text = "enter a memberID";
            }
        }

        private void btnExtension_Click(object sender, EventArgs e)
        {
            try
            {
                //this array is for checking the dataGridView changes
                bool[] selectedLoans = new bool[dataGridView1.Rows.Count];
                // move on rows in dataGridView
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    int currentRow = r.Index;
                    DataGridViewRow newDataGrid = dataGridView1.Rows[currentRow];
                    // convert the return date string to dataTime type
                    DateTime dt = Convert.ToDateTime(Convert.ToString(r.Cells[4].Value));
                    //check that which loans want change return date and it is valid
                    if (Convert.ToBoolean(r.Cells[6].Value) == true && Convert.ToString(r.Cells[5].Value) == "borrowed")
                    {
                        selectedLoans[currentRow] = true;
                        //check that which extension option is selected
                        //week
                        if (rdbWeek.Checked == true)
                        {
                            dt = dt.AddDays(7);
                            newDataGrid.Cells[4].Value = dt.ToString();
                        }
                        //10 days
                        else if (rdb10day.Checked == true)
                        {
                            dt = dt.AddDays(10);
                            newDataGrid.Cells[4].Value = dt.ToString();
                        }
                        //month
                        else if (rdbMonth.Checked == true)
                        {
                            dt = dt.AddDays(30);
                            newDataGrid.Cells[4].Value = dt.ToString();
                        }
                        //no option selected
                        else
                        {
                            MessageBox.Show("you shoud check one option to renew the return date", "no selected renew option", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                //check that any date has changed or not
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
                //save the changes of return date to excel file
                //save the datagrid to an list of loans
                if (shouldSaveDataGrid == true)
                {
                    List<Loan> changedListLoans = new List<Loan>();
                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        Loan rLoan = new Loan(Convert.ToInt32(r.Cells[0].Value), Convert.ToInt32(r.Cells[1].Value), Convert.ToInt32(r.Cells[2].Value), Convert.ToString(r.Cells[3].Value), Convert.ToString(r.Cells[4].Value), Convert.ToString(r.Cells[5].Value));
                        changedListLoans.Add(rLoan);
                    }
                    LoanMethods.ApplyLoanChanges(this.localPath, changedListLoans);
                    MessageBox.Show("renew of return date changed and saved successfully", "changes applied ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnChooseFile.Enabled = true;
                }
                else
                {
                    if (MessageBox.Show("do you want change the return dates", "not selected renew option", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnShow.Enabled = true;
                comboSearch.Enabled = true;
                comboSearch.Text = "enter a number";
            }
        }
    }
}