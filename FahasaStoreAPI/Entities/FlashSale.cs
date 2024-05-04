using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class FlashSale
    {
        public FlashSale()
        {
            FlashSaleBooks = new HashSet<FlashSaleBook>();
        }

        public int FlashSaleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<FlashSaleBook> FlashSaleBooks { get; set; }
    }
}
