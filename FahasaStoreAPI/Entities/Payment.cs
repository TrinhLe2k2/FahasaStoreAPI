using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int? OrderId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Order? Order { get; set; }
    }
}
