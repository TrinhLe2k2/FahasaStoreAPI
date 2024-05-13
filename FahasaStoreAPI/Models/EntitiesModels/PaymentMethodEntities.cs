using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class PaymentMethodEntities
    {
        public PaymentMethodEntities()
        {
            Orders = new HashSet<OrderBasic>();
            Payments = new HashSet<PaymentBasic>();
        }

        public int PaymentMethodId { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<OrderBasic> Orders { get; set; }
        public virtual ICollection<PaymentBasic> Payments { get; set; }
    }
}
