using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class CoverType
    {
        public CoverType()
        {
            Books = new HashSet<Book>();
        }

        public int CoverTypeId { get; set; }
        public string TypeName { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
