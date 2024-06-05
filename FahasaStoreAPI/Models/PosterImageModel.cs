namespace FahasaStoreAPI.Models
{
    public class PosterImageModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public bool ImageDefault { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
