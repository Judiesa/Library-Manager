using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Book
    {
        public int BookId
        { get; set; }
        public string BookName
        { get; set; }
        public string Author
        { get; set; }
        public string Genre
        { get; set; }
        public string Status
        { get; set; }
        public string[] BookInfo
        { get; set; }
        public Book(int bookId, string bookName , string author, string genre, string status )
        {
            BookInfo = new string[]
            {
             Convert.ToString(bookId),
             bookName,
             author,
             genre,
             status
            };
            this.BookId = bookId;
            this.BookName = bookName;
            this.Author = author;
            this.Genre = genre;
            this.Status = status;
        }
        public Book()
        {
        }
        public Book(int bookId)
        {
            this.BookId = bookId;
        }
    }
}
