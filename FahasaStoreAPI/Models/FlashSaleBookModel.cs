namespace FahasaStoreAPI.Models
{
    public class FlashSaleBookModel
    {
        public int Id { get; set; }
        public int FlashSaleId { get; set; }
        public int BookId { get; set; }
        public int DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
