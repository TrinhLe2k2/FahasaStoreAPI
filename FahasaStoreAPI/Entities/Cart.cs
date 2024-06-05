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

        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual AspNetUser User { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
