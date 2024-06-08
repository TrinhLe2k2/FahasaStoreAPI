using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
            OrderStatuses = new HashSet<OrderStatus>();
        }

        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int? VoucherId { get; set; }
        public int AddressId { get; set; }
        public int PaymentMethodId { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual PaymentMethod PaymentMethod { get; set; } = null!;
        public virtual AspNetUser User { get; set; } = null!;
        public virtual Voucher? Voucher { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderStatus> OrderStatuses { get; set; }
    }
}
