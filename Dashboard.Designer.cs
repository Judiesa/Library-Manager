
namespace Project
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.booksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAndEditBookInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findBooksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.membersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewMemberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewEditMembersInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMembershipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.loanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewBookIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportOfReturnDatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extensionOfReturnDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managmentOfPenaltiesForDelayReturnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.returnAnIssuedBookAndGetPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordHistoryLoansAndReturnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportOfHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.BurlyWood;
            this.menuStrip1.Font = new System.Drawing.Font("Rage Italic", 25F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.booksToolStripMenuItem,
            this.membersToolStripMenuItem,
            this.loanToolStripMenuItem,
            this.historyToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(900, 51);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // booksToolStripMenuItem
            // 
            this.booksToolStripMenuItem.BackColor = System.Drawing.Color.Tan;
            this.booksToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.booksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewBookToolStripMenuItem,
            this.deleteBookToolStripMenuItem,
            this.viewAndEditBookInfoToolStripMenuItem,
            this.findBooksToolStripMenuItem});
            this.booksToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.booksToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.booksToolStripMenuItem.Name = "booksToolStripMenuItem";
            this.booksToolStripMenuItem.Size = new System.Drawing.Size(103, 47);
            this.booksToolStripMenuItem.Text = "Books";
            // 
            // addNewBookToolStripMenuItem
            // 
            this.addNewBookToolStripMenuItem.BackColor = System.Drawing.Color.Tan;
            this.addNewBookToolStripMenuItem.Name = "addNewBookToolStripMenuItem";
            this.addNewBookToolStripMenuItem.Size = new System.Drawing.Size(406, 48);
            this.addNewBookToolStripMenuItem.Text = "Add New Book";
            this.addNewBookToolStripMenuItem.Click += new System.EventHandler(this.addNewBookToolStripMenuItem_Click);
            // 
            // deleteBookToolStripMenuItem
            // 
            this.deleteBookToolStripMenuItem.BackColor = System.Drawing.Color.Tan;
            this.deleteBookToolStripMenuItem.Name = "deleteBookToolStripMenuItem";
            this.deleteBookToolStripMenuItem.Size = new System.Drawing.Size(406, 48);
            this.deleteBookToolStripMenuItem.Text = "Delete Book";
            this.deleteBookToolStripMenuItem.Click += new System.EventHandler(this.deleteBookToolStripMenuItem_Click);
            // 
            // viewAndEditBookInfoToolStripMenuItem
            // 
            this.viewAndEditBookInfoToolStripMenuItem.BackColor = System.Drawing.Color.Tan;
            this.viewAndEditBookInfoToolStripMenuItem.Name = "viewAndEditBookInfoToolStripMenuItem";
            this.viewAndEditBookInfoToolStripMenuItem.Size = new System.Drawing.Size(406, 48);
            this.viewAndEditBookInfoToolStripMenuItem.Text = "View and Edit Book Info";
            this.viewAndEditBookInfoToolStripMenuItem.Click += new System.EventHandler(this.viewAndEditBookInfoToolStripMenuItem_Click);
            // 
            // findBooksToolStripMenuItem
            // 
            this.findBooksToolStripMenuItem.BackColor = System.Drawing.Color.Tan;
            this.findBooksToolStripMenuItem.Name = "findBooksToolStripMenuItem";
            this.findBooksToolStripMenuItem.Size = new System.Drawing.Size(406, 48);
            this.findBooksToolStripMenuItem.Text = "Find Books";
            this.findBooksToolStripMenuItem.Click += new System.EventHandler(this.findBooksToolStripMenuItem_Click);
            // 
            // membersToolStripMenuItem
            // 
            this.membersToolStripMenuItem.AutoSize = false;
            this.membersToolStripMenuItem.BackColor = System.Drawing.Color.Peru;
            this.membersToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.membersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewMemberToolStripMenuItem,
            this.viewEditMembersInfoToolStripMenuItem,
            this.viewMembershipToolStripMenuItem,
            this.toolStripMenuItem2});
            this.membersToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.membersToolStripMenuItem.Name = "membersToolStripMenuItem";
            this.membersToolStripMenuItem.Size = new System.Drawing.Size(112, 40);
            this.membersToolStripMenuItem.Text = "Members";
            // 
            // addNewMemberToolStripMenuItem
            // 
            this.addNewMemberToolStripMenuItem.BackColor = System.Drawing.Color.Peru;
            this.addNewMemberToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.addNewMemberToolStripMenuItem.Name = "addNewMemberToolStripMenuItem";
            this.addNewMemberToolStripMenuItem.Size = new System.Drawing.Size(452, 48);
            this.addNewMemberToolStripMenuItem.Text = "Add New Member";
            this.addNewMemberToolStripMenuItem.Click += new System.EventHandler(this.addNewMemberToolStripMenuItem_Click);
            // 
            // viewEditMembersInfoToolStripMenuItem
            // 
            this.viewEditMembersInfoToolStripMenuItem.BackColor = System.Drawing.Color.Peru;
            this.viewEditMembersInfoToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.viewEditMembersInfoToolStripMenuItem.Name = "viewEditMembersInfoToolStripMenuItem";
            this.viewEditMembersInfoToolStripMenuItem.Size = new System.Drawing.Size(452, 48);
            this.viewEditMembersInfoToolStripMenuItem.Text = "View and Edit Members Info";
            this.viewEditMembersInfoToolStripMenuItem.Click += new System.EventHandler(this.viewEditMembersInfoToolStripMenuItem_Click);
            // 
            // viewMembershipToolStripMenuItem
            // 
            this.viewMembershipToolStripMenuItem.BackColor = System.Drawing.Color.Peru;
            this.viewMembershipToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.viewMembershipToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.viewMembershipToolStripMenuItem.Name = "viewMembershipToolStripMenuItem";
            this.viewMembershipToolStripMenuItem.Size = new System.Drawing.Size(452, 48);
            this.viewMembershipToolStripMenuItem.Text = "View Membership Of Member";
            this.viewMembershipToolStripMenuItem.Click += new System.EventHandler(this.viewMembershipToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.Color.Peru;
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(452, 48);
            this.toolStripMenuItem2.Text = "Delete Member";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // loanToolStripMenuItem
            // 
            this.loanToolStripMenuItem.BackColor = System.Drawing.Color.Tan;
            this.loanToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewBookIssueToolStripMenuItem,
            this.reportOfReturnDatesToolStripMenuItem,
            this.extensionOfReturnDateToolStripMenuItem,
            this.managmentOfPenaltiesForDelayReturnToolStripMenuItem,
            this.returnAnIssuedBookAndGetPointToolStripMenuItem});
            this.loanToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.loanToolStripMenuItem.Name = "loanToolStripMenuItem";
            this.loanToolStripMenuItem.Size = new System.Drawing.Size(171, 47);
            this.loanToolStripMenuItem.Text = "Issue Books";
            // 
            // addNewBookIssueToolStripMenuItem
            // 
            this.addNewBookIssueToolStripMenuItem.BackColor = System.Drawing.Color.Tan;
            this.addNewBookIssueToolStripMenuItem.Name = "addNewBookIssueToolStripMenuItem";
            this.addNewBookIssueToolStripMenuItem.Size = new System.Drawing.Size(578, 48);
            this.addNewBookIssueToolStripMenuItem.Text = "Add New Issue Book";
            this.addNewBookIssueToolStripMenuItem.Click += new System.EventHandler(this.addNewBookIssueToolStripMenuItem_Click);
            // 
            // reportOfReturnDatesToolStripMenuItem
            // 
            this.reportOfReturnDatesToolStripMenuItem.BackColor = System.Drawing.Color.Tan;
            this.reportOfReturnDatesToolStripMenuItem.Name = "reportOfReturnDatesToolStripMenuItem";
            this.reportOfReturnDatesToolStripMenuItem.Size = new System.Drawing.Size(578, 48);
            this.reportOfReturnDatesToolStripMenuItem.Text = "Report Of Return Dates";
            this.reportOfReturnDatesToolStripMenuItem.Click += new System.EventHandler(this.reportOfReturnDatesToolStripMenuItem_Click);
            // 
            // extensionOfReturnDateToolStripMenuItem
            // 
            this.extensionOfReturnDateToolStripMenuItem.BackColor = System.Drawing.Color.Tan;
            this.extensionOfReturnDateToolStripMenuItem.Name = "extensionOfReturnDateToolStripMenuItem";
            this.extensionOfReturnDateToolStripMenuItem.Size = new System.Drawing.Size(578, 48);
            this.extensionOfReturnDateToolStripMenuItem.Text = "Renew The Return Date";
            this.extensionOfReturnDateToolStripMenuItem.Click += new System.EventHandler(this.extensionOfReturnDateToolStripMenuItem_Click);
            // 
            // managmentOfPenaltiesForDelayReturnToolStripMenuItem
            // 
            this.managmentOfPenaltiesForDelayReturnToolStripMenuItem.BackColor = System.Drawing.Color.Tan;
            this.managmentOfPenaltiesForDelayReturnToolStripMenuItem.Name = "managmentOfPenaltiesForDelayReturnToolStripMenuItem";
            this.managmentOfPenaltiesForDelayReturnToolStripMenuItem.Size = new System.Drawing.Size(578, 48);
            this.managmentOfPenaltiesForDelayReturnToolStripMenuItem.Text = "Managment Of Late Fee";
            this.managmentOfPenaltiesForDelayReturnToolStripMenuItem.Click += new System.EventHandler(this.managmentOfPenaltiesForDelayReturnToolStripMenuItem_Click);
            // 
            // returnAnIssuedBookAndGetPointToolStripMenuItem
            // 
            this.returnAnIssuedBookAndGetPointToolStripMenuItem.BackColor = System.Drawing.Color.Tan;
            this.returnAnIssuedBookAndGetPointToolStripMenuItem.Name = "returnAnIssuedBookAndGetPointToolStripMenuItem";
            this.returnAnIssuedBookAndGetPointToolStripMenuItem.Size = new System.Drawing.Size(578, 48);
            this.returnAnIssuedBookAndGetPointToolStripMenuItem.Text = "Return An Issued Book And Get Point";
            this.returnAnIssuedBookAndGetPointToolStripMenuItem.Click += new System.EventHandler(this.returnAnIssuedBookAndGetPointToolStripMenuItem_Click);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.BackColor = System.Drawing.Color.Peru;
            this.historyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recordHistoryLoansAndReturnsToolStripMenuItem,
            this.reportOfHistoryToolStripMenuItem});
            this.historyToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(114, 47);
            this.historyToolStripMenuItem.Text = "history";
            // 
            // recordHistoryLoansAndReturnsToolStripMenuItem
            // 
            this.recordHistoryLoansAndReturnsToolStripMenuItem.BackColor = System.Drawing.Color.Peru;
            this.recordHistoryLoansAndReturnsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.recordHistoryLoansAndReturnsToolStripMenuItem.Name = "recordHistoryLoansAndReturnsToolStripMenuItem";
            this.recordHistoryLoansAndReturnsToolStripMenuItem.Size = new System.Drawing.Size(537, 48);
            this.recordHistoryLoansAndReturnsToolStripMenuItem.Text = "Record History Loans And Returns";
            this.recordHistoryLoansAndReturnsToolStripMenuItem.Click += new System.EventHandler(this.recordHistoryLoansAndReturnsToolStripMenuItem_Click);
            // 
            // reportOfHistoryToolStripMenuItem
            // 
            this.reportOfHistoryToolStripMenuItem.BackColor = System.Drawing.Color.Peru;
            this.reportOfHistoryToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.reportOfHistoryToolStripMenuItem.Name = "reportOfHistoryToolStripMenuItem";
            this.reportOfHistoryToolStripMenuItem.Size = new System.Drawing.Size(537, 48);
            this.reportOfHistoryToolStripMenuItem.Text = "Report Of History";
            this.reportOfHistoryToolStripMenuItem.Click += new System.EventHandler(this.reportOfHistoryToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(82, 47);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem booksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewBookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem membersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewMemberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewEditMembersInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMembershipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteBookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewAndEditBookInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findBooksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewBookIssueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportOfReturnDatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extensionOfReturnDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem managmentOfPenaltiesForDelayReturnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem returnAnIssuedBookAndGetPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordHistoryLoansAndReturnsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportOfHistoryToolStripMenuItem;
    }
}

