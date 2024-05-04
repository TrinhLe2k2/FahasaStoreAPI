using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Menu
    {
        public int MenuId { get; set; }
        public string Name { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
