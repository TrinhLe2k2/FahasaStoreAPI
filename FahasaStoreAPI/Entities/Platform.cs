using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Platform
    {
        public int Id { get; set; }
        public string PlatformName { get; set; } = null!;
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public string Link { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}
