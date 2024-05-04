using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class SocialMediaLink
    {
        public int LinkId { get; set; }
        public string Platform { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Link { get; set; } = null!;
    }
}
