using Library.Data.Models;
using Library.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Repositories
{
    public class UserBookRepository : Repository<UserBook>, IUserBookRepository
    {
        public UserBookRepository(string connectionString) 
            : base(connectionString)
        {
        }
        public IEnumerable<UserBook> GetUsersBooks()
        {
            List<UserBook> result = new List<UserBook>();
            using (var command = new SqlCommand(@"UsersBooks"))
            {
                var userBooks = ExecuteStoredProc(command);

                foreach (var userBook in userBooks)
                {
                    var exist = result.FirstOrDefault(x => x.User.Id == userBook.User.Id);
                    if (exist != null)
                    {
                        exist.Books.AddRange(userBook.Books);
                    }
                    else
                    {
                        result.Add(userBook);
                    }
                }
                return result;
            }
        }

        public override UserBook PopulateRecord(SqlDataReader reader)
        {
            List<BookData> booksData = new List<BookData>();
            booksData.Add(new BookData
            {
                Id = reader.GetInt32(5),
                BookName = reader.GetString(6),
                BorrowDate = reader.GetDateTime(7),
                EndBorrowDate = reader.GetDateTime(8),
            });
            return new UserBook
            {
                User = new User
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    IsActive = reader.GetBoolean(4),
                },
                Books = booksData
            };
        }
    }
}
