using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class CartEntities
    {
        public CartEntities()
        {
            CartItems = new HashSet<CartItemBasic>();
        }

        public int CartId { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual UserBasic? User { get; set; }
        public virtual ICollection<CartItemBasic> CartItems { get; set; }
    }
}
