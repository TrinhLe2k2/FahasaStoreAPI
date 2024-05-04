namespace FahasaStoreAPI.Models.FormModels
{
    public class OrderForm
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? VoucherId { get; set; }
        public int? AddressId { get; set; }
        public int? PaymentMethodId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Description { get; set; }
    }
}
