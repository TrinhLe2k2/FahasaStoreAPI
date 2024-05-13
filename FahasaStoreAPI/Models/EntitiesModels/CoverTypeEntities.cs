using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class CoverTypeEntities
    {
        public CoverTypeEntities()
        {
            Books = new HashSet<BookBasic>();
        }

        public int CoverTypeId { get; set; }
        public string TypeName { get; set; } = null!;

        public virtual ICollection<BookBasic> Books { get; set; }
    }
}
