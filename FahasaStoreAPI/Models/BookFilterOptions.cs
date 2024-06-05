namespace FahasaStoreAPI.Models
{
    public class BookFilterOptions
    {
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? AuthorId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? PartnerId { get; set; }
        public string CoverType { get; set; } = string.Empty;
        public bool FlashSale { get; set; }
        public string SortBy { get; set; } = string.Empty;
    }
}
