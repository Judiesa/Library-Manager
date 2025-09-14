using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using SIO = System.IO;

namespace Project
{
    public class BookMethods
    {
        static int rows;
        static int bookSheet = 2;

        //count the rows of excel file that are filled up
        public static int CountRows(string path)
        {
            rows = 1;
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[bookSheet];
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

        //check that the given book id is available in excel file or not*
        public static bool ExistBookId(string path, Book selectedBook)
        {
            bool existId = false;
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[bookSheet];
            for (int i = 2; i <= rowExcel; i++)
            {
                if (selectedBook.BookId == (int)WSh.Cells[i, 1].value)
                {
                    existId = true;
                    break;
                }
            }
            Wb.Close();
            xslFile.Quit();
            return existId;
        }

        //find the id of the last book in excel file and get an id to the new book*
        public static int FindTheLastId(string path)
        {
            int lastID = 0;
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[bookSheet];
            for (int i = 2; i <= rowExcel; i++)
            {
                if (lastID < (int)WSh.Cells[i, 1].value)
                    lastID = (int)WSh.Cells[i, 1].value;
            }
            Wb.Close();
            xslFile.Quit();
            return lastID;
        }

        //delete a book from excel file*
        public static void DeleteBook(string path, Book deletedBook)
        {
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[bookSheet];
            Excel.Range range = WSh.UsedRange;
            int columnExcel = range.Columns.Count;
            for (int i = 2; i <= rowExcel; i++)
            {
                if (deletedBook.BookId <= (int)WSh.Cells[i, 1].value && i != rowExcel)
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

        //add the info of the new book to the sheet book excel file*
        public static void AddBookToExcel(string path, Book book)
        {
            int rowExcel = CountRows(path) + 1;
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[bookSheet];
            for (int i = 0; i < book.BookInfo.Length; i++)
            {
                WSh.Cells[rowExcel, i + 1].value = book.BookInfo[i];
            }
            Wb.SaveAs(path);
            Wb.Close();
            xslFile.Quit();
        }

        //find the info of the book in excel file with the given id*
        public static Book GetBookInfo(string path, int id)
        {
            int rowExcel = CountRows(path);
            Book book = new Book();
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[bookSheet];
            for (int i = 2; i <= rowExcel; i++)
            {
                if (id == (int)WSh.Cells[i, 1].value)
                {
                    book.BookId = (int)WSh.Cells[i, 1].value;
                    book.BookName = WSh.Cells[i, 2].value;
                    book.Author = WSh.Cells[i, 3].value;
                    book.Genre = WSh.Cells[i, 4].value;
                    book.Status = WSh.Cells[i, 5].value;
                }
            }
            Wb.Close();
            xslFile.Quit();
            return book;
        }

        //change the details of book list with new given datas in excel file
        public static void ApplyBookChanges(string path, List<Book> changedBook)
        {
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[bookSheet];
            for (int i = 2; i <= rowExcel; i++)
            {
                foreach (var books in changedBook)
                {
                    if (books.BookId == (int)WSh.Cells[i, 1].value)
                    {
                        WSh.Cells[i, 1].value = books.BookId;
                        WSh.Cells[i, 2].value = books.BookName;
                        WSh.Cells[i, 3].value = books.Author;
                        WSh.Cells[i, 4].value = books.Genre;
                        WSh.Cells[i, 5].value = books.Status;
                    }
                }
            }
            Wb.SaveAs(path);
            Wb.Close();
            xslFile.Quit();
        }

        //change the details of a book with new given datas in excel file 
        public static void ApplyBookChanges(string path, Book books)
        {
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[bookSheet];
            for (int i = 2; i <= rowExcel; i++)
            {
                if (books.BookId == (int)WSh.Cells[i, 1].value)
                {
                    WSh.Cells[i, 1].value = books.BookId;
                    WSh.Cells[i, 2].value = books.BookName;
                    WSh.Cells[i, 3].value = books.Author;
                    WSh.Cells[i, 4].value = books.Genre;
                    WSh.Cells[i, 5].value = books.Status;
                }
            }
            Wb.SaveAs(path);
            Wb.Close();
            xslFile.Quit();
        }

        //save the excel sheet books in book list for show in the form*
        public static List<Book> ShowExcelBooks(string path)
        {
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[bookSheet];
            List<Book> excelBooks = new List<Book>();
            for (int i = 2; i <= rowExcel; i++)
            {
                if (WSh.Cells[i, 1].value == null)
                {
                    continue;
                }
                else
                {
                    Book book = new Book((int)WSh.Cells[i, 1].value, WSh.Cells[i, 2].value, WSh.Cells[i, 3].value, WSh.Cells[i, 4].value, WSh.Cells[i, 5].value);
                    excelBooks.Add(book);
                }
            }
            Wb.Close();
            xslFile.Quit();
            return excelBooks;
        }

        //filter the sheet excel books by given info*
        public static List<Book> FilterExcelBooks(string path, string selectedItem, string itemColumn)
        {
            int selectedColumn = 1;
            //determining the number of the column that shoud be search by name of book property
            switch (itemColumn)
            {
                // column 2
                case "Book Name":
                    selectedColumn = 2;
                    break;
                // column 3
                case "Author Name":
                    selectedColumn = 3;
                    break;
                // column 4
                case "Book Genre":
                    selectedColumn = 4;
                    break;
            }
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[bookSheet];
            Excel.Range range = WSh.UsedRange;
            int columnExcel = range.Columns.Count;
            List<Book> filteredExcel = new List<Book>();
            for (int i = 2; i <= rowExcel; i++)
            {
                if (WSh.Cells[i, selectedColumn].value == selectedItem)
                {
                    Book book = new Book((int)WSh.Cells[i, 1].value, WSh.Cells[i, 2].value, WSh.Cells[i, 3].value, WSh.Cells[i, 4].value, WSh.Cells[i, 5].value);
                    filteredExcel.Add(book);
                }
                else
                {
                    continue;
                }
            }
            Wb.Close();
            xslFile.Quit();
            return filteredExcel;
        }

        //read the books that saved in excel*
        public static List<Book> ReadBookFromExcel(string path)
        {
            int rowExcel = CountRows(path);
            Excel.Application xslFile = new Excel.Application();
            Excel.Workbook Wb = xslFile.Workbooks.Add(path);
            Excel.Worksheet WSh = Wb.Worksheets[bookSheet];
            List<Book> libraryBook = new List<Book>();
            for (int i = 2; i <= rowExcel; i++)
            {
                if (WSh.Cells[i, 1].value == null)
                {
                    continue;
                }
                else
                {
                    Book book = new Book((int)WSh.Cells[i, 1].value, WSh.Cells[i, 2].value, WSh.Cells[i, 3].value, WSh.Cells[i, 4].value, WSh.Cells[i, 5].value);
                    libraryBook.Add(book);
                }
            }
            Wb.Close();
            xslFile.Quit();
            return libraryBook;
        }
        ////write in excel
        //public static int WriteBookToExcel(string path, List<Book> sentedBooks)
        //{
        //    //search for book id
        //    var lastBookExcel = BookMethods.ReadBookFromExcel(path);
        //    //find new book id
        //    int newBookId = 1;
        //    if (lastBookExcel.Any())
        //    {
        //        newBookId = lastBookExcel
        //           .Max(x => x.BookId) + 1;
        //    }
        //    //the fainal list for saving in excel
        //    var finalList = ManageDuplicatedBooks(lastBookExcel, sentedBooks);
        //    //
        //    foreach (var item in finalList)
        //    {
        //        if(lastBookExcel.Contains(item.BookId)));

        //    }
        //    i/*nt rowExcel =*/
        //        int as = finalList.Select(x => x.BookId).ToList().f

        //    Excel.Application xslFile = new Excel.Application();
        //    Excel.Workbook Wb = xslFile.Workbooks.Add(path);
        //    Excel.Worksheet WSh = Wb.Worksheets[bookSheet];
        //    for (int i = 0; i < finalList.Count; i++)
        //    {
        //        WSh.Cells[rowExcel + i, 1].value = newBookId + i;
        //        WSh.Cells[rowExcel + i, 2].value = finalList[i].BookName;
        //        WSh.Cells[rowExcel + i, 3].value = finalList[i].Author;
        //        WSh.Cells[rowExcel + i, 4].value = finalList[i].Genre;
        //        WSh.Cells[rowExcel + i, 5].value = finalList[i].Status;
        //    }
        //    Wb.SaveAs(path);
        //    Wb.Close();
        //    xslFile.Quit();
        //    return newBookId;
        //}

        ////find duplicated books in excel
        //private static List<Book> ManageDuplicatedBooks(List<Book> lastBookExcel, List<Book> newSendedBook)
        //{
        //    //new list for save to excel
        //    List<Book> bookResult = new List<Book>();
        //    //check that excel is empty or not
        //    if (lastBookExcel.Any())
        //    {
        //        //move on two list and find choose the
        //        foreach (var sended in newSendedBook)
        //        {
        //            //new book
        //            if (!lastBookExcel.Select(x => x.BookId).Contains(sended.BookId))
        //                bookResult.Add(sended);
        //            //changes book
        //            else
        //            {
        //                foreach (var cell in lastBookExcel)
        //                {
        //                    if (cell.BookId == sended.BookId)
        //                    {
        //                        cell.BookName = sended.BookName;
        //                        cell.Author = sended.Author;
        //                        cell.Genre = sended.Genre;
        //                        cell.Status = sended.Status;
        //                        bookResult.Add(sended);
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //        return bookResult;
        //    }
        //    else
        //    {
        //        return newSendedBook;
        //    }
        //}

        //read the excel
    }
}
