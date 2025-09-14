using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;


namespace Project
{
    public class LoanMethods
    {
        static int rows;
        static int borrowSheet = 3;

        //count the rows of excel file that are filled up
        public static int CountRows(string path)
        {
            rows = 1;
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[borrowSheet];
            Excel.Range range = WSh.UsedRange;
            int rowExcel = range.Rows.Count + 1;
            if (rowExcel == 1 || rowExcel == 0)
                rowExcel = 3;
            try
            {
                for (int i = 2; i <= rowExcel; i++)
                {
                    if (WSh.Cells[i, 1].value == null)
                    {
                        continue;
                    }
                    if (10000000 == (int)WSh.Cells[i, 1].value)
                    {
                        rows++;
                    }
                    rows++;
                }
                Wb.Close();
                xslFile.Quit();
                return rows;
            }
            catch
            {
                Wb.Close();
                xslFile.Quit();
                return rows;
            }
        }

        //check the count of borrowed books by a member in present time
        public static bool CanBorrowedBooks(string path, Member requesteddBook)
        {
            bool canLoan = true;
            int countOfLoans = 0;
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[borrowSheet];
            for (int i = 2; i <= rowExcel; i++)
            {
                if (requesteddBook.MemberId == (int)WSh.Cells[i, 2].value && WSh.Cells[i, 6].value == "borrowed")
                {
                    countOfLoans++;
                }
            }
            if (countOfLoans < 3)
            {
                canLoan = true;
            }
            else if (countOfLoans >= 3 && requesteddBook.Status == "VIP")
            {
                canLoan = true;
            }
            else
            {
                canLoan = false;
            }
            Wb.Close();
            xslFile.Quit();
            return canLoan;
        }

        //change the requested book status to opposite present status
        public static void ChangeBookStatus(string path, Book requesteddBook)
        {
            int rowExcel = BookMethods.CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[borrowSheet - 1];
            for (int i = 2; i <= rowExcel; i++)
            {
                if (requesteddBook.BookId == (int)WSh.Cells[i, 1].value)
                {
                    if (WSh.Cells[i, 5].value == "available")
                    {
                        WSh.Cells[i, 5].value = "borrowed";
                        break;
                    }
                    else
                    {
                        WSh.Cells[i, 5].value = "available";
                        break;
                    }
                }
            }
            Wb.SaveAs(path);
            Wb.Close();
            xslFile.Quit();
        }

        //find the id of the last loan in excel file and get an id to the new borrowed book*
        public static int FindTheLastId(string path)
        {
            int lastID = 0;
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[borrowSheet];
            for (int i = 2; i <= rowExcel; i++)
            {
                if (lastID < (int)WSh.Cells[i, 1].value)
                    lastID = (int)WSh.Cells[i, 1].value;
            }
            Wb.Close();
            xslFile.Quit();
            return lastID;
        }

        //add the info of the new borrow to the sheet borrow excel file*
        public static void AddLoanToExcel(string path, Loan loan)
        {
            int rowExcel = CountRows(path) + 1;
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[borrowSheet];
            for (int i = 0; i < loan.IssueBookInfo.Length; i++)
            {
                WSh.Cells[rowExcel, i + 1].value = loan.IssueBookInfo[i];
            }
            Wb.SaveAs(path);
            Wb.Close();
            xslFile.Quit();
        }

        //change the details of borrow with new given datas in excel file*
        public static void ApplyLoanChanges(string path, List<Loan> changedLoans)
        {
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[borrowSheet];
            for (int i = 2; i <= rowExcel; i++)
            {
                foreach (var loans in changedLoans)
                {
                    if (loans.LoanId == (int)WSh.Cells[i, 1].value)
                    {
                        WSh.Cells[i, 1].value = loans.LoanId;
                        WSh.Cells[i, 2].value = loans.MemberId;
                        WSh.Cells[i, 3].value = loans.BookId;
                        WSh.Cells[i, 4].value = loans.IssueDate;
                        WSh.Cells[i, 5].value = loans.ReturnDate;
                        WSh.Cells[i, 6].value = loans.Status;
                    }
                }
            }
            Wb.SaveAs(path);
            Wb.Close();
            xslFile.Quit();
        }

        //save the excel sheet member in member library list for checking the condition*
        public static List<Member> LibraryMembers(string path)
        {
            int rowExcel = BookMethods.CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[borrowSheet - 2];
            List<Member> libraryMemb = new List<Member>();
            for (int i = 2; i <= rowExcel; i++)
            {
                if (WSh.Cells[i, 1].value == null)
                {
                    continue;
                }
                else
                {
                    Member member = new Member((int)WSh.Cells[i, 1].value, WSh.Cells[i, 2].value, WSh.Cells[i, 3].value, WSh.Cells[i, 4].value, (double)WSh.Cells[i, 5].value);
                    libraryMemb.Add(member);
                }
            }
            Wb.Close();
            xslFile.Quit();
            return libraryMemb;
        }

        //save the excel sheet loan in loan list for checking the condition*
        public static List<Loan> ReadLoansFromExcel(string path)
        {
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[borrowSheet];
            List<Loan> loanExcelFile = new List<Loan>();
            for (int i = 2; i <= rowExcel; i++)
            {

                if (WSh.Cells[i, 1].value != null)
                {
                    Loan loan = new Loan((int)WSh.Cells[i, 1].value, (int)WSh.Cells[i, 2].value, (int)WSh.Cells[i, 3].value, Convert.ToString(WSh.Cells[i, 4].value), Convert.ToString(WSh.Cells[i, 5].value), WSh.Cells[i, 6].value);
                    loanExcelFile.Add(loan);
                }
                else
                {
                    continue;
                }
            }
            Wb.Close();
            xslFile.Quit();
            return loanExcelFile;
        }

        //    //write in loans list excel
        //    public static int WriteLoansToExcel(string path, List<Loan> sentedLoans)
        //    {
        //        //search for book id
        //        var lastLoanExcel = LoanMethods.ReadLoansFromExcel(path);
        //        //find new book id
        //        int newLoanId = 1;
        //        if (!lastLoanExcel.Any())
        //        {
        //            newLoanId = lastLoanExcel
        //               .Max(x => x.BookId) + 1;
        //        }
        //        //the fainal list for saving in excel
        //        var finalList = ManageDuplicatedLoans(lastLoanExcel, sentedLoans);
        //        int rowExcel = newLoanId;
        //        Excel.Application xslFile = new Excel.Application();
        //        Excel.Workbook Wb = xslFile.Workbooks.Add(path);
        //        Excel.Worksheet WSh = Wb.Worksheets[borrowSheet];
        //        for (int i = 0; i < finalList.Count; i++)
        //        {
        //            WSh.Cells[rowExcel + i, 1].value = newLoanId + i;
        //            WSh.Cells[rowExcel + i, 2].value = finalList[i].MemberId;
        //            WSh.Cells[rowExcel + i, 3].value = finalList[i].BookId;
        //            WSh.Cells[rowExcel + i, 4].value = finalList[i].IssueDate;
        //            WSh.Cells[rowExcel + i, 4].value = finalList[i].ReturnDate;
        //            WSh.Cells[rowExcel + i, 5].value = finalList[i].Status;
        //        }
        //        Wb.SaveAs(path);
        //        Wb.Close();
        //        xslFile.Quit();
        //        return newLoanId;
        //    }

        //    //find duplicated books in excel
        //    private static List<Loan> ManageDuplicatedLoans(List<Loan> lastLoanExcel, List<Loan> newSendedLoan)
        //    {
        //        //new list for save to excel
        //        List<Loan> loanResult = new List<Loan>();
        //        //check that excel is empty or not
        //        if (lastLoanExcel.Any())
        //        {
        //            //move on two list and find choose the
        //            foreach (var sended in newSendedLoan)
        //            {
        //                //new loan
        //                if (!lastLoanExcel.Select(x => x.BookId).Contains(sended.BookId))
        //                    loanResult.Add(sended);
        //                //changes loan
        //                else
        //                {
        //                    foreach (var cell in lastLoanExcel)
        //                    {
        //                        if (cell.LoanId == sended.LoanId)
        //                        {
        //                            cell.MemberId = sended.MemberId;
        //                            cell.BookId = sended.BookId;
        //                            cell.IssueDate = sended.IssueDate;
        //                            cell.ReturnDate = sended.ReturnDate;
        //                            cell.Status = sended.Status;
        //                        }
        //                    }
        //                }
        //            }
        //            return loanResult;
        //        }
        //        else
        //        {
        //            return loanResult;
        //        }
        //    }

        //}
    }
}