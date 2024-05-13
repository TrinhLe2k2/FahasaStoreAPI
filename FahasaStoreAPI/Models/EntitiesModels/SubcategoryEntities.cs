using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class SubcategoryEntities
    {
        public SubcategoryEntities()
        {
            Books = new HashSet<BookBasic>();
        }

        public int SubcategoryId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;

        public virtual CategoryBasic? Category { get; set; }
        public virtual ICollection<BookBasic> Books { get; set; }
    }
}
