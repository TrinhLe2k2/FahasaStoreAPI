using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Website
    {
        public int WebsiteId { get; set; }
        public string Name { get; set; } = null!;
        public string LogoUrl { get; set; } = null!;
        public string IconUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
