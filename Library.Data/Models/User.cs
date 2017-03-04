using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get {
                return FirstName + " " + LastName;
            } }
        [EmailAddress]
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
