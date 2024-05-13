using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class FlashSaleBookEntities
    {
        public int FlashSaleBookId { get; set; }
        public int? FlashSaleId { get; set; }
        public int? BookId { get; set; }
        public double DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public int Sold { get; set; }

        public virtual BookBasic? Book { get; set; }
        public virtual FlashSaleBasic? FlashSale { get; set; }
    }
}
