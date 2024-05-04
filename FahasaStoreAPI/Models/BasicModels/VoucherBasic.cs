namespace FahasaStoreAPI.Models.BasicModels
{
    public class VoucherBasic
    {
        public int VoucherId { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public double DiscountPercent { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal MinOrderAmount { get; set; }
        public decimal MaxDiscountAmount { get; set; }
        public int UsageLimit { get; set; }
    }
}
