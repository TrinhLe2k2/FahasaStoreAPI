using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Category
    {
        public Category()
        {
            Subcategories = new HashSet<Subcategory>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? PublicId { get; set; }
        public string ImageUrl { get; set; } = null!;

        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}
