namespace FahasaStoreAPI.Models
{
    public class FavouriteModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int BookId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
