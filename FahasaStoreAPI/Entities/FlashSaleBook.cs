using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class FlashSaleBook
    {
        public int Id { get; set; }
        public int FlashSaleId { get; set; }
        public int BookId { get; set; }
        public int DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual FlashSale FlashSale { get; set; } = null!;
    }
}
