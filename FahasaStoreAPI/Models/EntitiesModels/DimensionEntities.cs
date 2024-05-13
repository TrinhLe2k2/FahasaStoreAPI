using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class DimensionEntities
    {
        public DimensionEntities()
        {
            Books = new HashSet<BookBasic>();
        }

        public int DimensionId { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Unit { get; set; } = null!;

        public virtual ICollection<BookBasic> Books { get; set; }
    }
}
