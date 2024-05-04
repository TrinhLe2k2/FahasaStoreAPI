using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int? OrderId { get; set; }
        public int? PaymentMethodId { get; set; }
        public string? Description { get; set; }

        public virtual Order? Order { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
    }
}
