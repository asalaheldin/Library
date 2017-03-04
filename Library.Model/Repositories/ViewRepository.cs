using Library.Data.Models;
using Library.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Repositories
{
    class ViewRepository : Repository<BookHistory>, IViewRepository
    {
        public ViewRepository(string connectionString) 
            : base(connectionString)
        {
        }

        public IEnumerable<BookHistory> GetBookHistory(int id)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new SqlCommand(
                @"select  (u.FirstName + ' ' + u.LastName) FullName, ub.BorrowDate, CAST(DATEADD(day,ub.BorrowDays,ub.BorrowDate) as DATE) EndBorrowDate from Book b 
                            inner join UserBook ub
                            on (b.Id = ub.BookId)
                            inner join [User] u
                            on (u.Id = ub.UserId)
                            where b.Id = @id
                            "))
            {
                command.Parameters.Add(new ObjectParameter("id", id));
                return GetRecords(command);
            }
        }

        public override BookHistory PopulateRecord(SqlDataReader reader)
        {
            return new BookHistory
            {
                FullName = reader.GetString(0),
                BorrowDate = reader.GetDateTime(1),
                EndBorrowDate = reader.GetDateTime(1),
            };
        }
    }
}
