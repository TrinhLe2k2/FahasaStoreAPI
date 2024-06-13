using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Dimension
    {
        public Dimension()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Unit { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
