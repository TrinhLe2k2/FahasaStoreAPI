namespace FahasaStoreAPI.Models.FormModels
{
    public class BookForm
    {
        public int BookId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? PartnerId { get; set; }
        public int? AuthorId { get; set; }
        public int? CoverTypeId { get; set; }
        public int? DimensionId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal OriginalPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public double DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public double? Weight { get; set; }
        public int? PageCount { get; set; }
    }
}
