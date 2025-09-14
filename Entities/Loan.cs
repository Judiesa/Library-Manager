using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Loan
    {
        public int LoanId
        { get; set; }
        public int MemberId
        { get; set; }
        public int BookId
        { get; set; }
        public string IssueDate
        { get; set; }
        public string ReturnDate
        { get; set; }
        public string Status
        { get; set; }
        public string[] IssueBookInfo
        { get; set; }
        public static List<Member> MemberList
        { get; set; }
        public static List<Book> LibraryBooks
        { get; set; }
        public Loan(int issueBookId, int memberId = 0, int bookId = 0, string issueDate = "", string returnDate = "", string status = "")
        {
            IssueBookInfo = new string[]
            {
                Convert.ToString(issueBookId),
                Convert.ToString(memberId),
             Convert.ToString(bookId),
             issueDate,
             returnDate,
             status
            };
            this.LoanId = issueBookId;
            this.MemberId = memberId;
            this.BookId = bookId;
            this.IssueDate = issueDate;
            this.ReturnDate = returnDate;
            this.Status = status;
        }
        public Loan()
        {

        }
    }
}
