namespace FahasaStoreAPI.Models.FormModels
{
    public class PaymentForm
    {
        public int PaymentId { get; set; }
        public int? OrderId { get; set; }
        public int? PaymentMethodId { get; set; }
        public string? Description { get; set; }
    }
}
