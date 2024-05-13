using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class RoleEntities
    {
        public RoleEntities()
        {
            Users = new HashSet<UserBasic>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<UserBasic> Users { get; set; }
    }
}
