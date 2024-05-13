using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class PartnerTypeEntities
    {
        public PartnerTypeEntities()
        {
            Partners = new HashSet<PartnerBasic>();
        }

        public int PartnerTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<PartnerBasic> Partners { get; set; }
    }
}
