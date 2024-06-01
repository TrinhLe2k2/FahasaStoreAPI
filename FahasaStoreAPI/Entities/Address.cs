using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Address
    {
        public Address()
        {
            Orders = new HashSet<Order>();
        }

        public int AddressId { get; set; }
        public string? UserId { get; set; }
        public string ReceiverName { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Ward { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public bool DefaultAddress { get; set; }

        public virtual AspNetUser? User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
