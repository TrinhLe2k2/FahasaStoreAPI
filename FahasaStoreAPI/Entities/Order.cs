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
            Payments = new HashSet<Payment>();
        }

        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? VoucherId { get; set; }
        public int? AddressId { get; set; }
        public int? PaymentMethodId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Description { get; set; }

        public virtual Address? Address { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual User? User { get; set; }
        public virtual Voucher? Voucher { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderStatus> OrderStatuses { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
