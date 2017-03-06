﻿using Library.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Interfaces
{
    public interface IUserBookRepository
    {
        IEnumerable<UserBook> GetUsersBooks();
    }
}
