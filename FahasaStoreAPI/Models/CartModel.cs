namespace FahasaStoreAPI.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}
