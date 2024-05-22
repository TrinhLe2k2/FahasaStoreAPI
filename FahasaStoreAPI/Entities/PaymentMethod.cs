using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Orders = new HashSet<Order>();
        }

        public int PaymentMethodId { get; set; }
        public string Name { get; set; } = null!;
        public string? PublicId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool Active { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
