namespace FahasaStoreAPI.Models.BasicModels
{
    public class WebsiteBasic
    {
        public int WebsiteId { get; set; }
        public string Name { get; set; } = null!;
        public string? LogoUrl { get; set; }
        public string? IconUrl { get; set; }
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
