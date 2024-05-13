using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class OrderStatusEntities
    {
        public int OrderStatusId { get; set; }
        public int? OrderId { get; set; }
        public int? StatusId { get; set; }
        public DateTime OrderStatusDate { get; set; }

        public virtual OrderBasic? Order { get; set; }
        public virtual StatusBasic? Status { get; set; }
    }
}
