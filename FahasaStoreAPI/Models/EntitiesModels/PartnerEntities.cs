using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class PartnerEntities
    {
        public PartnerEntities()
        {
            Books = new HashSet<BookBasic>();
        }

        public int PartnerId { get; set; }
        public int? PartnerTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ImageUrl { get; set; }

        public virtual PartnerTypeBasic? PartnerType { get; set; }
        public virtual ICollection<BookBasic> Books { get; set; }
    }
}
