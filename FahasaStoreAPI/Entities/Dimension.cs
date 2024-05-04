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

        public int DimensionId { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Unit { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
