using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class HelpContentEntities
    {
        public int HelpContentId { get; set; }
        public int? HelpId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        public virtual HelpBasic? Help { get; set; }
    }
}
