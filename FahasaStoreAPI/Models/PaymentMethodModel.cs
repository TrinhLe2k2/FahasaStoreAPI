namespace FahasaStoreAPI.Models
{
    public class PaymentMethodModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
