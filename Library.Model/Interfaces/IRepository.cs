using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T PopulateRecord(SqlDataReader reader);
        IEnumerable<T> GetRecords(SqlCommand command);
        T GetRecord(SqlCommand command);
        IEnumerable<T> ExecuteStoredProc(SqlCommand command);
        
    }
}
