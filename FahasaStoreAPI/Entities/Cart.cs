using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int CartId { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
