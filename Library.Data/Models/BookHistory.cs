﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Models
{
    public class BookHistory
    {
        public string BookName { get; set; }
        public string FullName { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime  EndBorrowDate { get; set; }
        
    }
}
