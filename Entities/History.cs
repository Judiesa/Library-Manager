using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class History:Loan
    {
        public int HistoryId
        { get; set; }
        public static List<Loan> MemberList
        { get; set; }
        public History(int historyId, int issueBookId, int memberId, int bookId, string issueDate, string returnDate, string status) : base(issueBookId, memberId, bookId, issueDate, returnDate, status)
        {
            this.HistoryId = historyId;
        }
    }
}
