namespace FahasaStoreAPI.Models.BasicModels
{
    public class PartnerBasic
    {
        public int PartnerId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
