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

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Partner> Partners { get; set; }
    }
}
