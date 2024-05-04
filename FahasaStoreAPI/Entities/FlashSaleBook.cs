using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class FlashSaleBook
    {
        public int FlashSaleBookId { get; set; }
        public int? FlashSaleId { get; set; }
        public int? BookId { get; set; }
        public double DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public int Sold { get; set; }

        public virtual Book? Book { get; set; }
        public virtual FlashSale? FlashSale { get; set; }
    }
}
