namespace FahasaStoreAPI.Models.BasicModels
{
    public class PaymentMethodBasic
    {
        public int PaymentMethodId { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public bool Active { get; set; }
    }
}
