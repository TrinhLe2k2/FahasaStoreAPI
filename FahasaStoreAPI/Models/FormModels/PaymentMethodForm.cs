namespace FahasaStoreAPI.Models.FormModels
{
    public class PaymentMethodForm
    {
        public int PaymentMethodId { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public bool Active { get; set; }
    }
}
