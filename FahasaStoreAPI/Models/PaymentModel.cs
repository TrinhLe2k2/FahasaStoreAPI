namespace FahasaStoreAPI.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
