using Library.Data.Models;
using Library.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Repositories
{
    class HistoryRepository : Repository<BookHistory>, IHistoryRepository
    {
        public HistoryRepository(string connectionString) 
            : base(connectionString)
        {
        }

        public IEnumerable<BookHistory> GetBookHistory(int id)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new SqlCommand(@"BookHistory"))
            {
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                return ExecuteStoredProc(command);
            }
        }

        public override BookHistory PopulateRecord(SqlDataReader reader)
        {
            return new BookHistory
            {
                BookName = reader.GetString(0),
                FullName = reader.GetString(1),
                BorrowDate = reader.GetDateTime(2),
                EndBorrowDate = reader.GetDateTime(3),
            };
        }
    }
}
