using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Banner
    {
        public int BannerId { get; set; }
        public string? PublicId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
