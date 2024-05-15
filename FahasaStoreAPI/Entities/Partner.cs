using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Partner
    {
        public Partner()
        {
            BooksPartners = new HashSet<BooksPartner>();
        }

        public int PartnerId { get; set; }
        public int? PartnerTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;

        public virtual PartnerType? PartnerType { get; set; }
        public virtual ICollection<BooksPartner> BooksPartners { get; set; }
    }
}
