using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
