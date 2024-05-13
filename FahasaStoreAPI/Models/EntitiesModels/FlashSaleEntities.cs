using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class FlashSaleEntities
    {
        public FlashSaleEntities()
        {
            FlashSaleBooks = new HashSet<FlashSaleBookBasic>();
        }

        public int FlashSaleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<FlashSaleBookBasic> FlashSaleBooks { get; set; }
    }
}
