using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Status
    {
        public Status()
        {
            OrderStatuses = new HashSet<OrderStatus>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        public virtual ICollection<OrderStatus> OrderStatuses { get; set; }
    }
}
