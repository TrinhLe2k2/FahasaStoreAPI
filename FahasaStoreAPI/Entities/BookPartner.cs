using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class BookPartner
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int PartnerId { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual Partner Partner { get; set; } = null!;
    }
}
