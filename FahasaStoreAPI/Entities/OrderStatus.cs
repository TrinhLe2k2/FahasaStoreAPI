using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public int? OrderId { get; set; }
        public int? StatusId { get; set; }
        public DateTime OrderStatusDate { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Status? Status { get; set; }
    }
}
