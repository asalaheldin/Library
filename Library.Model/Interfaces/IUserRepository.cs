using Library.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Interfaces
{
    public interface IUserRepository
    {
        int Register(User user);
        User Login(string email);
    }
}
