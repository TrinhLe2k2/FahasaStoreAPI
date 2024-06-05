namespace FahasaStoreAPI.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int VoucherId { get; set; }
        public int AddressId { get; set; }
        public int PaymentMethodId { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
