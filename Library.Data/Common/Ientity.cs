using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Common
{
    public interface IEntity
    {
        int Id { get; set; }
        bool IsActive { get; set; }
        int DisplayOrder { get; set; }
        DateTime CreateDate { get; set; }
        DateTime UpdateDate { get; set; }
    }
}
