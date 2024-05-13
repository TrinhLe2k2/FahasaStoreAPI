using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class CartItemEntities
    {
        public int CartItemId { get; set; }
        public int? CartId { get; set; }
        public int? BookId { get; set; }
        public int Quantity { get; set; }

        public virtual BookBasic? Book { get; set; }
        public virtual CartBasic? Cart { get; set; }
    }
}
