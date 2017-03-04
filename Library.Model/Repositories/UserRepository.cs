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
    public class UserRepository : Repository<User>, IUserRepository
    {
        string _connectionString;
        public UserRepository(string connectionString)
            :base(connectionString)
        {
            _connectionString = connectionString;
        }

        public User Login(string email)
        {
            using (var command = new SqlCommand(@"Exec LoginUser @Email"))
            {
                command.Parameters.Add(new ObjectParameter("Email", email));
                return ExecuteStoredProc(command).FirstOrDefault();
            }
        }

        public int Register(User user)
        {
            int userId = -1;
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("RegisterUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = user.Email;
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = user.FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = user.LastName;
                    command.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                    connection.Open();
                    command.ExecuteNonQuery();

                    userId = Convert.ToInt32(command.Parameters["@NewId"].Value);
                    connection.Close();
                }
            }
            return userId;
        }
        public override User PopulateRecord(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3),
                IsActive = reader.GetBoolean(4)
            };
        }
    }
}
