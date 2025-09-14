using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Project
{
    public class UserMethods
    {
        static int rows;
        static int memberSheet = 1;

        //count the rows of excel file that are filled up
        public static int CountRows(string path)
        {
            rows = 1;
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[memberSheet];
            Excel.Range range = WSh.UsedRange;
            int rowExcel = range.Rows.Count;
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

        //check that the given book id is available in excel file or not*
        public static bool ExistMemberId(string path, Member selectedMember)
        {
            bool existId = false;
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[memberSheet];
            for (int i = 2; i <= rowExcel; i++)
            {
                if (selectedMember.MemberId == (int)WSh.Cells[i, 1].value)
                {
                    existId = true;
                    break;
                }
            }
            Wb.Close();
            xslFile.Quit();
            return existId;
        }

        //find the id of the last member in excel file and get an id to the new member*
        public static int FindTheLastId(string path)
        {
            int lastID = 0;
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[memberSheet];
            for (int i = 2; i <= rowExcel; i++)
            {
                if (lastID < (int)WSh.Cells[i, 1].value)
                    lastID = (int)WSh.Cells[i, 1].value;
            }
            Wb.Close();
            xslFile.Quit();
            return lastID;
        }

        //delete a member from excel file*
        public static void DeleteMember(string path, Member deletedMember)
        {
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[memberSheet];
            Excel.Range range = WSh.UsedRange;
            int columnExcel = range.Columns.Count;
            for (int i = 2; i <= rowExcel; i++)
            {
                if (deletedMember.MemberId <= (int)WSh.Cells[i, 1].value && i != rowExcel)
                {
                    for (int j = 1; j <= columnExcel; j++)
                    {
                        WSh.Cells[i, j].value = WSh.Cells[i + 1, j].value;
                    }
                }
                if (i == rowExcel)
                {
                    for (int j = 1; j <= columnExcel; j++)
                    {
                        WSh.Cells[i, j].value = "  ";
                    }
                }
            }
            Wb.SaveAs(path);
            Wb.Close();
            xslFile.Quit();
        }

        //determining the status of the member 
        public static string StatusOfMember(string path, Member statusMember)
        {
            string result = " ";
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[memberSheet];
            Excel.Range range = WSh.UsedRange;
            int columnExcel = range.Columns.Count;
            for (int i = 2; i <= rowExcel; i++)
            {
                if (statusMember.MemberId == (int)WSh.Cells[i, 1].value)
                {
                    result = WSh.Cells[i, 4].value;
                }
            }
            Wb.Close();
            xslFile.Quit();
            return result;
        }

        //add the info of the new member to the sheet member excel file*
        public static void AddMemberToExcel(string path, Member member)
        {
            int rowExcel = CountRows(path) + 1;
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[memberSheet];
            for (int i = 0; i < member.MemberInfo.Length; i++)
            {
                WSh.Cells[rowExcel, i + 1].value = member.MemberInfo[i];
            }
            Wb.SaveAs(path);
            Wb.Close();
            xslFile.Quit();
        }

        //find the info of the Member in excel file with the given id*
        public static Member GetMemberInfo(string path, int id)
        {
            int rowExcel = CountRows(path);
            Member member = new Member();
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[memberSheet];
            for (int i = 2; i <= rowExcel; i++)
            {
                if (id == (int)WSh.Cells[i, 1].value)
                {
                    member.MemberId = (int)WSh.Cells[i, 1].value;
                    member.FirstName = WSh.Cells[i, 2].value;
                    member.LastName = WSh.Cells[i, 3].value;
                    member.Status = WSh.Cells[i, 4].value;
                    member.MemberPoint = WSh.Cells[i, 5].value;
                }
            }
            Wb.Close();
            xslFile.Quit();
            return member;
        }

        //change the details of a member with new given datas in excel file
        public static void ApplyMemberChanges(string path, Member changedmember)
        {
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[memberSheet];
            for (int i = 2; i <= rowExcel; i++)
            {
                if (changedmember.MemberId == (int)WSh.Cells[i, 1].value)
                {
                    for (int j = 0; j < changedmember.MemberInfo.Length; j++)
                    {
                        WSh.Cells[i, j + 1].value = changedmember.MemberInfo[j];
                    }
                }
            }
            Wb.SaveAs(path);
            Wb.Close();
            xslFile.Quit();
        }

        //change the details of a member list with new given datas in excel file
        public static void ApplyMemberChanges(string path, List<Member> changedMembers)
        {
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[memberSheet];
            for (int i = 2; i <= rowExcel; i++)
            {
                foreach (var member in changedMembers)
                {
                    if (member.MemberId == (int)WSh.Cells[i, 1].value)
                    {
                        WSh.Cells[i, 1].value = member.MemberId;
                        WSh.Cells[i, 2].value = member.FirstName;
                        WSh.Cells[i, 3].value = member.LastName;
                        WSh.Cells[i, 4].value = member.Status;
                        WSh.Cells[i, 5].value = member.MemberPoint;
                    }
                }
            }
            Wb.SaveAs(path);
            Wb.Close();
            xslFile.Quit();
        }

        //save the excel sheet loan in loan list for checking the condition*
        public static List<Member> ReadMembersFromExcel(string path)
        {
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[memberSheet];
            List<Member> memberExcelFile = new List<Member>();
            for (int i = 2; i <= rowExcel; i++)
            {

                if (WSh.Cells[i, 1].value != null)
                {
                    Member member = new Member((int)WSh.Cells[i, 1].value, WSh.Cells[i, 2].value, WSh.Cells[i, 3].value, WSh.Cells[i, 4].value, WSh.Cells[i, 5].value);
                    memberExcelFile.Add(member);
                }
                else
                {
                    continue;
                }
            }
            Wb.Close();
            xslFile.Quit();
            return memberExcelFile;
        }
    }
}
