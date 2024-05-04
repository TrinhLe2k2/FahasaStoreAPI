using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class PartnerType
    {
        public PartnerType()
        {
            Partners = new HashSet<Partner>();
        }

        public int PartnerTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Partner> Partners { get; set; }
    }
}
