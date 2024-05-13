﻿using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Partner
    {
        public Partner()
        {
            Books = new HashSet<Book>();
        }

        public int PartnerId { get; set; }
        public int? PartnerTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ImageUrl { get; set; }

        public virtual PartnerType? PartnerType { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
