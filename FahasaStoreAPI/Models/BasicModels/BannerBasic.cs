namespace FahasaStoreAPI.Models.BasicModels
{
    public class BannerBasic
    {
        public int BannerId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
