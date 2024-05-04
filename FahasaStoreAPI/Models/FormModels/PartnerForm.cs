﻿namespace FahasaStoreAPI.Models.FormModels
{
    public class PartnerForm
    {
        public int PartnerId { get; set; }
        public int? PartnerTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
