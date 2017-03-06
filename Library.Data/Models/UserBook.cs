using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Models
{
    public class UserBook
    {
        public User User { get; set; }
        public List<BookData> Books { get; set;} 
    }

    public class BookData {
        public int Id { get; set; }
        public string  BookName { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime EndBorrowDate { get; set; }
    }
    
}
