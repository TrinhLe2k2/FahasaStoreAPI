namespace FahasaStoreAPI.Models.BasicModels
{
    public class SocialMediaLinkBasic
    {
        public int LinkId { get; set; }
        public string Platform { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Link { get; set; } = null!;
    }
}
