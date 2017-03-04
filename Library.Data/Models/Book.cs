using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Models
{
    public class Book : Base
    {
        public string Name { get; set; }
        public List<Author> Authors { get; set; }

        public string AuthorNamesCommaSeparated {
            get {
                StringBuilder names = new StringBuilder("");
                foreach (var author in Authors)
                    names.Append(author.FullName + ", ");

                names.Remove(names.Length - 2, 2);
                return names.ToString();
            }
            
        }
    }
}
