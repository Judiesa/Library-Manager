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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
      

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DetermineStatusAndDeleteMemberfrm deletefrm = new DetermineStatusAndDeleteMemberfrm();
            deletefrm.ShowDialog();
        }

        private void viewMembershipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetermineStatusAndDeleteMemberfrm deletefrm = new DetermineStatusAndDeleteMemberfrm();
            deletefrm.ShowDialog();
        }

        private void addNewMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMemberfrm addmembfrm = new AddMemberfrm();
            addmembfrm.ShowDialog();
        }

        private void viewEditMembersInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditMemberfrm editfrm = new EditMemberfrm();
            editfrm.ShowDialog();
        }

        private void addNewBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBookfrm addbkfrm = new AddBookfrm();
            addbkfrm.ShowDialog();
        }

        private void deleteBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteBookfrm deletebkfrm = new DeleteBookfrm();
            deletebkfrm.ShowDialog();
        }

        private void viewAndEditBookInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditBookfrm editbkfrm = new EditBookfrm();
            editbkfrm.ShowDialog();
        }

        private void findBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindBookfrm findbkfrm = new FindBookfrm();
            findbkfrm.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Exit", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void addNewBookIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddIssueBookfrm addIssue = new AddIssueBookfrm();
            addIssue.ShowDialog();
        }

        private void extensionOfReturnDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtensionReturnDatefrm extension = new ExtensionReturnDatefrm();
            extension.ShowDialog(); 
        }

        private void reportOfReturnDatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportOfReturnDatefrm returnDatefrm = new ReportOfReturnDatefrm();
            returnDatefrm.ShowDialog();
        }

        private void managmentOfPenaltiesForDelayReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenaltiesDelayReturnfrm penalties = new PenaltiesDelayReturnfrm();
            penalties.ShowDialog();
        }

        private void recordHistoryLoansAndReturnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecordOfHistoryLoansAndReturnsfrm recordOfHistoryLoans = new RecordOfHistoryLoansAndReturnsfrm();
            recordOfHistoryLoans.ShowDialog();
        }

        private void reportOfHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportOfHistory report = new ReportOfHistory();
            report.ShowDialog();
        }

        private void returnAnIssuedBookAndGetPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReturnLoanAndGetPointfrm returnLoan = new ReturnLoanAndGetPointfrm();
            returnLoan.ShowDialog();
        }
    }
}
