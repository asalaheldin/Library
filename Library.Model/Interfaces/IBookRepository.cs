using Library.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> GetAvailableBooks();
        IEnumerable<Book> GetUserBooks(int id);
        int BorrowBook(Borrow model);
    }
}
