namespace FahasaStoreAPI.Models
{
    public class BookFilterOptions
    {
        public string SortBy { get; set; } = string.Empty;
        public string Search { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? AuthorId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? PartnerId { get; set; }
        public int? PartnerTypeId { get; set; }
        public int? CoverTypeId { get; set; }
        public bool FlashSale { get; set; }
    }
}
