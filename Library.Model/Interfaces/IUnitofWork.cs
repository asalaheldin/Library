using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Interfaces
{
    public interface IUnitofWork
    {
        IBookRepository BookRepository { get; set; }
        IHistoryRepository HistoryRepository { get; set; }
        IUserBookRepository UserBookRepository { get; set; }
        IUserRepository UserRepository { get; set; }
    }
}
