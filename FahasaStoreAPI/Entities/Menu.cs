using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
