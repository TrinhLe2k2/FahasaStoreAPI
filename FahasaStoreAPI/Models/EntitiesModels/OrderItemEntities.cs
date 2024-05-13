using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class OrderItemEntities
    {
        public int OrderItemId { get; set; }
        public int? OrderId { get; set; }
        public int? BookId { get; set; }
        public int Quantity { get; set; }

        public virtual BookBasic? Book { get; set; }
        public virtual OrderBasic? Order { get; set; }
    }
}
