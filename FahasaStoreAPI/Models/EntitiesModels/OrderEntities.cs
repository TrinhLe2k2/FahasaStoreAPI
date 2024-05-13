using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class OrderEntities
    {
        public OrderEntities()
        {
            OrderItems = new HashSet<OrderItemBasic>();
            OrderStatuses = new HashSet<OrderStatusBasic>();
            Payments = new HashSet<PaymentBasic>();
        }

        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? VoucherId { get; set; }
        public int? AddressId { get; set; }
        public int? PaymentMethodId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Description { get; set; }

        public virtual AddressEntities? Address { get; set; }
        public virtual PaymentMethodBasic? PaymentMethod { get; set; }
        public virtual UserBasic? User { get; set; }
        public virtual VoucherBasic? Voucher { get; set; }
        public virtual ICollection<OrderItemBasic> OrderItems { get; set; }
        public virtual ICollection<OrderStatusBasic> OrderStatuses { get; set; }
        public virtual ICollection<PaymentBasic> Payments { get; set; }
    }
}
