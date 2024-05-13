using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class CategoryEntities
    {
        public CategoryEntities()
        {
            Subcategories = new HashSet<SubcategoryBasic>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;

        public virtual ICollection<SubcategoryBasic> Subcategories { get; set; }
    }
}
