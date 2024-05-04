using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Help
    {
        public Help()
        {
            HelpContents = new HashSet<HelpContent>();
        }

        public int HelpId { get; set; }
        public string Topic { get; set; } = null!;

        public virtual ICollection<HelpContent> HelpContents { get; set; }
    }
}
