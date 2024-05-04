namespace FahasaStoreAPI.Models.FormModels
{
    public class SocialMediaLinkForm
    {
        public int LinkId { get; set; }
        public string Platform { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Link { get; set; } = null!;
    }
}
