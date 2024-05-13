using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class AddressEntities
    {
        public AddressEntities()
        {
            Orders = new HashSet<OrderBasic>();
        }

        public int AddressId { get; set; }
        public int? UserId { get; set; }
        public string ReceiverName { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Ward { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public bool DefaultAddress { get; set; }
        public bool Active { get; set; }

        public virtual UserBasic? User { get; set; }
        public virtual ICollection<OrderBasic> Orders { get; set; }
    }
}
