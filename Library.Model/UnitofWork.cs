using Library.Model.Interfaces;
using Library.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class UnitofWork : IUnitofWork
    {
        private const string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=LibraryBD; Integrated Security=SSPI;MultipleActiveResultSets=True;";
        public UnitofWork()
        {
        }
        // Add all the repository handles here

        IBookRepository bookRepository = null;
        IHistoryRepository historyRepository = null;
        IUserBookRepository userBookRepository = null;
        IUserRepository userRepository = null;

        public IBookRepository BookRepository
        {
            get { return bookRepository ?? (bookRepository = new BookRepository(connectionString)); }
            set { bookRepository = value; }
        }
        public IHistoryRepository HistoryRepository
        {
            get { return historyRepository ?? (historyRepository = new HistoryRepository(connectionString)); }
            set { historyRepository = value; }
        }
        public IUserBookRepository UserBookRepository
        {
            get { return userBookRepository ?? (userBookRepository = new UserBookRepository(connectionString)); }
            set { userBookRepository = value; }
        }
        public IUserRepository UserRepository
        {
            get { return userRepository ?? (userRepository = new UserRepository(connectionString)); }
            set { userRepository = value; }
        }
    }
}
