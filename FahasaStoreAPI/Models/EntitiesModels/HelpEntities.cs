using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class HelpEntities
    {
        public HelpEntities()
        {
            HelpContents = new HashSet<HelpContentBasic>();
        }

        public int HelpId { get; set; }
        public string Topic { get; set; } = null!;

        public virtual ICollection<HelpContentBasic> HelpContents { get; set; }
    }
}
