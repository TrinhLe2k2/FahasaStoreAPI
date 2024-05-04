using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class HelpContent
    {
        public int HelpContentId { get; set; }
        public int? HelpId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        public virtual Help? Help { get; set; }
    }
}
