using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class BooksPartner
    {
        public int BooksPartnersId { get; set; }
        public int? BookId { get; set; }
        public int? PartnerId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Book? Book { get; set; }
        public virtual Partner? Partner { get; set; }
    }
}
