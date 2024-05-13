using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class StatusEntities
    {
        public StatusEntities()
        {
            OrderStatuses = new HashSet<OrderStatusBasic>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        public virtual ICollection<OrderStatusBasic> OrderStatuses { get; set; }
    }
}
