namespace FahasaStoreAPI.Models.FormModels
{
    public class FlashSaleBookForm
    {
        public int FlashSaleBookId { get; set; }
        public int? FlashSaleId { get; set; }
        public int? BookId { get; set; }
        public double DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public int Sold { get; set; }
    }
}
