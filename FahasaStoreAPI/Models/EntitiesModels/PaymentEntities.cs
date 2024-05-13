using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class PaymentEntities
    {
        public int PaymentId { get; set; }
        public int? OrderId { get; set; }
        public int? PaymentMethodId { get; set; }
        public string? Description { get; set; }

        public virtual OrderBasic? Order { get; set; }
        public virtual PaymentMethodBasic? PaymentMethod { get; set; }
    }
}
