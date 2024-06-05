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

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}
