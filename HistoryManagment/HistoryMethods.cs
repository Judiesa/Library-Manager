using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using SRI = System.Runtime.InteropServices;

namespace Project
{
    public class HistoryMethods
    {
        static int rows;
        static int historySheet = 4;

        //count the rows of excel file that are filled up
        public static int CountRows(string path)
        {
            rows = 1;
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[historySheet];
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
                    else if (10000000 == (int)WSh.Cells[i, 1].value)
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

        //read from excel the whole filled sheet and save into a list
        public static List<History> ReadHistoryFromExcel(string path)
        {
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[historySheet];
            List<History> historyExcelFile = new List<History>();
            for (int i = 2; i <= rowExcel; i++)
            {
                if (WSh.Cells[i, 1].value != null)
                {
                    History history = new History((int)WSh.Cells[i, 1].value, (int)WSh.Cells[i, 2].value, (int)WSh.Cells[i, 3].value, (int)WSh.Cells[i, 4].value, Convert.ToString(WSh.Cells[i, 5].value), Convert.ToString(WSh.Cells[i, 6].value), WSh.Cells[i, 7].value);
                    historyExcelFile.Add(history);
                }
                else
                {
                    continue;
                }
            }
            Wb.Close();
            xslFile.Quit();
            return historyExcelFile;
        }

        //write in excel a new record of history
        public static void WriteHistoryToExcel(string path, List<Loan> newLoansList)
        {
            //save the excel datas
            var lastHistoryList = HistoryMethods.ReadHistoryFromExcel(path);
            //find new history id
            int newHistoryId = 1;
            if (lastHistoryList.Any())
            {
                newHistoryId = lastHistoryList
                   .Max(x => x.HistoryId) + 1;
            }
            //the fainal list for saving in excel
            var finalList = ManageDuplicatedLoans(lastHistoryList, newLoansList);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[historySheet];
            for (int i = 0; i < finalList.Count; i++)
            {
                WSh.Cells[newHistoryId + i+1, 1].value = newHistoryId + i;
                WSh.Cells[newHistoryId + i+1, 2].value = Convert.ToInt32(finalList[i].LoanId);
                WSh.Cells[newHistoryId + i+1, 3].value = finalList[i].MemberId;
                WSh.Cells[newHistoryId + i+1, 4].value = finalList[i].BookId;
                WSh.Cells[newHistoryId + i+1, 5].value = finalList[i].IssueDate;
                WSh.Cells[newHistoryId + i+1, 6].value = finalList[i].ReturnDate;
                WSh.Cells[newHistoryId + i+1, 7].value = finalList[i].Status;
            }
            Wb.SaveAs(path);
            Wb.Close();
            xslFile.Quit();
        }

        //find duplicated histories in excel
        public static List<Loan> ManageDuplicatedLoans(List<History> lastHistoryExcel, List<Loan> newSendedHistory)
        {
                //new list for save to excel
            List<Loan> historyResult = new List<Loan>();
            //check that excel is empty or not
            if (lastHistoryExcel.Any())
            {
                //move on two list and find choose the
                foreach (var item in newSendedHistory)
                {
                    if (!lastHistoryExcel.Select(x=>x.LoanId).Contains(item.LoanId))
                        historyResult.Add(item);
                }
                return historyResult;
            }
            else
            {
                return newSendedHistory;
            }
        }
    }
}
