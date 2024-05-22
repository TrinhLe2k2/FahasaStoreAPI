using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Platform
    {
        public int PlatformId { get; set; }
        public string PlatformName { get; set; } = null!;
        public string? PublicId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Link { get; set; } = null!;
    }
}
